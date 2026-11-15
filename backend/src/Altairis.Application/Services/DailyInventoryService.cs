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
    public class DailyInventoryService : IDailyInventoryService
    {
        private readonly IBaseRepository<DailyInventory> _repository;
        private readonly IReservationService _reservationService;
        private readonly IHotelService _hotelService;

        public DailyInventoryService(
            IBaseRepository<DailyInventory> repository,
            IReservationService reservationService,
            IHotelService hotelService
        )
        {
            _repository = repository;
            _reservationService = reservationService;
            _hotelService = hotelService;
        }

        public async Task<SingleApiResponse<DailyInventoryDTO>> GetByIdAsync(int id)
        {
            try
            {
                var dailyInventory = await _repository.GetByIdAsync(id);

                return (dailyInventory is not null)
                    ? new SingleApiResponse<DailyInventoryDTO> { Data = dailyInventory?.ToDto() }
                    : new SingleApiResponse<DailyInventoryDTO>
                    {
                        StatusCode = HttpStatusCode.NotFound,
                        Message = "Registro no encontrado."
                    };
            }
            catch (Exception ex)
            {
                return new SingleApiResponse<DailyInventoryDTO>()
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Success = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<ListApiResponse<DailyInventoryDTO>> GetAllAsync()
        {
            try
            {
                var dailyInventorys = await _repository.GetAllAsync();

                return (dailyInventorys is not null && dailyInventorys.Any())
                    ? new ListApiResponse<DailyInventoryDTO> { Items = dailyInventorys?.Select(h => h.ToDto())?.ToList() }
                    : new ListApiResponse<DailyInventoryDTO>
                    {
                        StatusCode = HttpStatusCode.NotFound,
                        Message = "No se hallaron registros.",
                    };
            }
            catch (Exception ex)
            {
                return new ListApiResponse<DailyInventoryDTO>()
                {
                    StatusCode = HttpStatusCode.OK,
                    Success = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<PagedListApiResponse<DailyInventoryDTO>> GetAllPagedAsync(PaginationFilters pagination, DailyInventoryFiltersDTO? filter = null)
        {
            try
            {
                IReadOnlyList<DailyInventory>? inventories;

                if (filter is not null && !filter.AreAllPropertiesNull())
                {
                    Expression<Func<DailyInventory, bool>> predicate = (inventory) =>
                    (
                        ((string.IsNullOrEmpty(filter.RoomTypeCode) || inventory.RoomType.Code.ToUpper().Contains(filter.RoomTypeCode.ToUpper().Trim())))
                        && (filter.StartDate == null || inventory.StayDate >= DateOnly.FromDateTime(filter.StartDate.Value.AddDays(1).AddSeconds(-1)))
                        && (filter.EndDate == null || inventory.StayDate <= DateOnly.FromDateTime(filter.EndDate.Value.AddDays(1).AddSeconds(-1)))
                    );

                    inventories = await _repository.GetByConditionOrderedByAsync(predicate, x => x.StayDate, true, ignoreAutoIncludes: false);
                }
                else
                {
                    inventories = await _repository.GetAllOrderedByAsync(x => x.StayDate, true, ignoreAutoIncludes: false);
                }


                if (inventories is not null && inventories.Any())
                {
                    var pagedInventories = await ListToDto(inventories.Skip(pagination.Skip)?.Take(pagination.Take)?.Select(x => x)?.ToList()!);
                    
                    return new PagedListApiResponse<DailyInventoryDTO>
                    {
                        Items = pagedInventories,
                        TotalCount = inventories?.Count ?? 0,
                        PageNumber = pagination.PageNumber,
                        PageSize = pagination.PageSize,
                    };
                }
                else
                {
                    return new PagedListApiResponse<DailyInventoryDTO>
                    {
                        StatusCode = HttpStatusCode.NotFound,
                        Message = "No se hallaron registros.",
                    };
                }
            }
            catch (Exception ex)
            {
                return new PagedListApiResponse<DailyInventoryDTO>()
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Success = false,
                    Message = ex.Message,
                };
            }
        }

        private async Task<List<DailyInventoryDTO>> ListToDto(IReadOnlyList<DailyInventory> pagedInventories)
        {
            var reservations = (await _reservationService.GetAllAsync(new ReservationFiltersDTO { Status = ReservationStatus.Confirmed })).Items;

            var hotels = (await _hotelService.GetAllAsync()).Items;

            var inventories = pagedInventories.Select(inventory =>
            {
                var reservedUnits = reservations?
                    .Where(reservation =>
                        reservation?.RoomTypeId == inventory.RoomTypeId &&
                        reservation.CheckInDate <= inventory.StayDate &&
                        reservation.CheckOutDate > inventory.StayDate)
                    .Sum(reservation => reservation?.RoomsCount) ?? 0;

                var dto = inventory.ToDto(reservedUnits);
                dto.HotelId = inventory.RoomType.HotelId;
                dto.RoomTypeCode = inventory.RoomType.Code;
                dto.RoomTypeName = inventory.RoomType.Name;

                return dto;

            }).ToList();

            foreach (var inventory in inventories)
            {
                var hotel = hotels?.Where(x => x.Id == inventory.HotelId).FirstOrDefault();

                if(hotel != null)
                {
                    inventory.HotelCode = hotel.Code;
                    inventory.HotelName = hotel.Name;
                }
            }

            return inventories;
        }
    }
}