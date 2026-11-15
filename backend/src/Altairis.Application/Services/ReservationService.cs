using Altairis.Application.DTOs;
using Altairis.Application.DTOs.Filters;
using Altairis.Application.Helpers;
using Altairis.Application.Helpers.MappingExtensions;
using Altairis.Application.Interfaces.Repositories;
using Altairis.Application.Interfaces.Services;
using Altairis.Domain.Entities;
using Altairis.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Net;
using System.Text;
using static System.Net.WebRequestMethods;

namespace Altairis.Application.Services
{
    public class ReservationService : IReservationService
    {
        private readonly IBaseRepository<Reservation> _repository;
        private readonly IHotelService _hotelService;

        public ReservationService(
            IBaseRepository<Reservation> repository, 
            IHotelService hotelService
        )
        {
            _repository = repository;
            _hotelService = hotelService;
        }

        public async Task<SingleApiResponse<ReservationDTO>> GetByIdAsync(int id)
        {
            try
            {
                var reservation = await _repository.GetByIdAsync(id);

                return (reservation is not null)
                    ? new SingleApiResponse<ReservationDTO> { Data = reservation?.ToDto() }
                    : new SingleApiResponse<ReservationDTO>
                        {
                            StatusCode = HttpStatusCode.NotFound,
                            Message = "Registro no encontrado."
                        };
            }
            catch (Exception ex)
            {
                return new SingleApiResponse<ReservationDTO>()
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Success = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<ListApiResponse<ReservationDTO>> GetAllAsync(ReservationFiltersDTO? filter = null)
        {
            try
            {
                IReadOnlyList<Reservation>? reservations;

                if (filter is not null && !filter.AreAllPropertiesNull())
                {
                    Expression<Func<Reservation, bool>> predicate = (reservation) =>
                    (
                        (string.IsNullOrEmpty(filter.Reference) || reservation.Reference.ToUpper().Contains(filter.Reference.ToUpper().Trim()))
                        && (string.IsNullOrEmpty(filter.RoomTypeCode) || (reservation.RoomType.Code.ToUpper().Contains(filter.RoomTypeCode.ToUpper().Trim())))
                        && (string.IsNullOrEmpty(filter.GuestName) || reservation.GuestName.ToUpper().Contains(filter.GuestName.ToUpper().Trim()))
                        && (filter.Status == null || reservation.Status == filter.Status)
                        && (filter.CheckInDate == null || reservation.CheckInDate == filter.CheckInDate)
                        && (filter.CheckOutDate == null || reservation.CheckOutDate <= filter.CheckOutDate)
                    );

                    reservations = await _repository.GetByConditionAsync(predicate);
                }
                else
                {
                    reservations = await _repository.GetAllAsync();
                }

                return (reservations is not null && reservations.Any())
                    ? new ListApiResponse<ReservationDTO> { Items = reservations?.Select(h => h.ToDto())?.ToList() }
                    : new ListApiResponse<ReservationDTO>
                    {
                        StatusCode = HttpStatusCode.NotFound,
                        Message = "No se hallaron registros.",
                    };
            }
            catch (Exception ex)
            {
                return new ListApiResponse<ReservationDTO>()
                {
                    StatusCode = HttpStatusCode.OK,
                    Success = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<PagedListApiResponse<ReservationDTO>> GetAllPagedAsync(PaginationFilters pagination, ReservationFiltersDTO? filter)
        {
            try
            {
                IReadOnlyList<Reservation>? reservations;

                if (filter is not null && !filter.AreAllPropertiesNull())
                {
                    Expression<Func<Reservation, bool>> predicate = (reservation) =>
                    (
                        (string.IsNullOrEmpty(filter.Reference) || reservation.Reference.ToUpper().Contains(filter.Reference.ToUpper().Trim()))
                        && (string.IsNullOrEmpty(filter.RoomTypeCode) || (reservation.RoomType.Code.ToUpper().Contains(filter.RoomTypeCode.ToUpper().Trim())))
                        && (string.IsNullOrEmpty(filter.GuestName) || reservation.GuestName.ToUpper().Contains(filter.GuestName.ToUpper().Trim()))
                        && (filter.Status == null || reservation.Status == filter.Status)
                        && (filter.CheckInDate == null || reservation.CheckInDate >= filter.CheckInDate)
                        && (filter.CheckOutDate == null || reservation.CheckOutDate <= filter.CheckOutDate)
                    );

                    reservations = await _repository.GetByConditionOrderedByAsync(predicate, x => x.CreationDate, true, ignoreAutoIncludes: false);
                }
                else
                {
                    reservations = await _repository.GetAllOrderedByAsync(x => x.CreationDate, true, ignoreAutoIncludes: false);
                }

                if (reservations is not null && reservations.Any())
                {
                    var pagedReservations = await ListToDto(reservations?.Skip(pagination.Skip)?.Take(pagination.Take)?.Select(x => x)?.ToList()!);
    
                    return new PagedListApiResponse<ReservationDTO>
                    {
                        Items = pagedReservations,
                        TotalCount = reservations?.Count ?? 0,
                        PageNumber = pagination.PageNumber,
                        PageSize = pagination.PageSize,
                    };
                }
                else {
                    return new PagedListApiResponse<ReservationDTO>
                    {
                        StatusCode = HttpStatusCode.NotFound,
                        Message = "No se hallaron registros.",
                    };
                }
            }
            catch (Exception ex)
            {
                return new PagedListApiResponse<ReservationDTO>()
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Success = false,
                    Message = ex.Message,
                };
            }
        }

        private async Task<List<ReservationDTO>> ListToDto(List<Reservation> pagedReservations)
        {
            var hotels = (await _hotelService.GetAllAsync()).Items;

            var reservations = pagedReservations.Select(reservation =>
            {
                var dto = reservation.ToDto();
                dto.Hotel = hotels?.FirstOrDefault(x => x?.Id == dto.RoomType?.HotelId);
                
                return dto;

            }).ToList();

            return reservations;

        }
    }
}
