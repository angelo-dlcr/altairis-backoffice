using Altairis.Application.DTOs;
using Altairis.Application.Interfaces.Repositories;
using Altairis.Application.Interfaces.Services;
using Altairis.Application.Helpers.MappingExtensions;
using Altairis.Domain.Entities;
using System;
using System.Net;
using System.Collections.Generic;
using System.Text;
using Altairis.Application.Helpers;

namespace Altairis.Application.Services
{
    public class ReservationService : IReservationService
    {
        private readonly IBaseRepository<Reservation> _repository;

        public ReservationService(IBaseRepository<Reservation> repository)
        {
            _repository = repository;
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

        public async Task<ListApiResponse<ReservationDTO>> GetAllAsync()
        {
            try
            {
                var reservations = await _repository.GetAllAsync();

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

        public async Task<PagedListApiResponse<ReservationDTO>> GetAllPagedAsync(PaginationFilters pagination)
        {
            try
            {
                var reservations = await _repository.GetAllAsync();

                return (reservations is not null && reservations.Any())
                    ? new PagedListApiResponse<ReservationDTO>
                        {
                            Items = reservations?.Skip(pagination.Skip).Take(pagination.Take).Select(h => h.ToDto())?.ToList(),
                            TotalCount = reservations?.Count ?? 0,
                            PageNumber = pagination.PageNumber,
                            PageSize = pagination.PageSize,
                        }
                    : new PagedListApiResponse<ReservationDTO>
                        {
                            StatusCode = HttpStatusCode.NotFound,
                            Message = "No se hallaron registros.",
                        };
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
    }
}
