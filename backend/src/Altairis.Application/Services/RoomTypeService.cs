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

namespace Altairis.Application.Services
{
    public class RoomTypeService : IRoomTypeService
    {
        private readonly IBaseRepository<RoomType> _repository;
        private readonly IHotelService _hotelService;

        public RoomTypeService(IBaseRepository<RoomType> repository, IHotelService hotelService)
        {
            _repository = repository;
            _hotelService = hotelService;
        }

        public async Task<SingleApiResponse<RoomTypeDTO>> GetByIdAsync(int id)
        {
            try
            {
                var roomType = await _repository.GetByIdAsync(id);

                return (roomType is not null)
                    ? new SingleApiResponse<RoomTypeDTO> { Data = roomType?.ToDto() }
                    : new SingleApiResponse<RoomTypeDTO>
                        {
                            StatusCode = HttpStatusCode.NotFound,
                            Message = "Registro no encontrado."
                        };
            }
            catch (Exception ex)
            {
                return new SingleApiResponse<RoomTypeDTO>()
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Success = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<ListApiResponse<RoomTypeDTO>> GetAllByHotelIdAsync(int hotelId)
        {
            try
            {
                var roomTypes = await _repository.GetByConditionOrderedByAsync(x => x.HotelId == hotelId, x => x.Code, ignoreAutoIncludes: true);

                return (roomTypes is not null && roomTypes.Any())
                    ? new ListApiResponse<RoomTypeDTO> 
                    { 
                        Items = roomTypes?.Select(h => h.ToDto())?.ToList()!
                    }
                    : new ListApiResponse<RoomTypeDTO>
                    {
                        StatusCode = HttpStatusCode.NotFound,
                        Message = "No se hallaron registros.",
                        Success = false
                    };
            }
            catch (Exception ex)
            {
                return new ListApiResponse<RoomTypeDTO>()
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Success = false,
                    Message = ex.InnerException?.Message ?? ex.Message,
                };
            }
        }

        public async Task<ListApiResponse<RoomTypeDTO>> GetAllAsync()
        {
            try
            {
                var roomTypes = await _repository.GetAllAsync();

                return (roomTypes is not null && roomTypes.Any())
                    ? new ListApiResponse<RoomTypeDTO> { Items = roomTypes?.Select(h => h.ToDto())?.ToList() }
                    : new ListApiResponse<RoomTypeDTO>
                    {
                        StatusCode = HttpStatusCode.NotFound,
                        Message = "No se hallaron registros.",
                    };
            }
            catch (Exception ex)
            {
                return new ListApiResponse<RoomTypeDTO>()
                {
                    StatusCode = HttpStatusCode.OK,
                    Success = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<PagedListApiResponse<RoomTypeDTO>> GetAllPagedAsync(PaginationFilters pagination, RoomTypeFiltersDTO? filter = null)
        {
            try
            {
                IReadOnlyList<RoomType>? roomTypes;

                if (filter is not null && !filter.AreAllPropertiesNull())
                {
                    Expression<Func<RoomType, bool>> predicate = (roomType) =>
                    (
                        (string.IsNullOrEmpty(filter.NameOrCode) ||
                            (roomType.Name.ToUpper().Contains(filter.NameOrCode.ToUpper().Trim())
                          || roomType.Code.ToUpper().Contains(filter.NameOrCode.ToUpper().Trim()))
                        )
                        && (filter.IsActive == null || roomType.IsActive == filter.IsActive)
                        && (filter.StartDate == null || roomType.CreationDate >= filter.StartDate)
                        && (filter.EndDate == null || roomType.CreationDate <= filter.EndDate.Value.AddDays(1).AddSeconds(-1))
                    );

                    roomTypes = await _repository.GetByConditionOrderedByAsync(predicate, x => x.CreationDate, true, ignoreAutoIncludes: false);
                }
                else
                {
                    roomTypes = await _repository.GetAllOrderedByAsync(x => x.CreationDate, true, ignoreAutoIncludes: false);
                }

                if (roomTypes is not null && roomTypes.Any())
                {
                    var pagedRoomTypes = await ListToDto(roomTypes?.Skip(pagination.Skip).Take(pagination.Take).Select(h => h)?.ToList()!);

                    return new PagedListApiResponse<RoomTypeDTO>
                    {
                        Items = pagedRoomTypes,
                        TotalCount = roomTypes?.Count ?? 0,
                        PageNumber = pagination.PageNumber,
                        PageSize = pagination.PageSize,
                    };
                }
                else
                {
                    return new PagedListApiResponse<RoomTypeDTO>
                    {
                        StatusCode = HttpStatusCode.NotFound,
                        Message = "No se hallaron registros.",
                    };
                }
            }
            catch (Exception ex)
            {
                return new PagedListApiResponse<RoomTypeDTO>()
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Success = false,
                    Message = ex.Message,
                };
            }
        }

        private async Task<List<RoomTypeDTO>> ListToDto(IReadOnlyList<RoomType> pagedRoomTypes)
        {
            var hotels = (await _hotelService.GetAllAsync()).Items;

            var roomTypes = pagedRoomTypes.Select(inventory =>
            {
                var dto = inventory.ToDto();
                dto.HotelName = hotels?.FirstOrDefault(x => x?.Id == dto.HotelId)?.Name;
                dto.HotelCode = hotels?.FirstOrDefault(x => x?.Id == dto.HotelId)?.Code;

                return dto;

            }).ToList();

            return roomTypes;
        }

        public async Task<SingleApiResponse<bool>> AddAsync(RoomTypeDTO dto)
        {
            try
            {
                var roomType = dto.ToEntityCreate();

                var success = await _repository.AddAsync(roomType);

                return success
                    ? new SingleApiResponse<bool>
                    {
                        Data = true,
                        StatusCode = HttpStatusCode.OK,
                        Message = "Registro creado exitosamente."
                    }
                    : new SingleApiResponse<bool>
                    {
                        Success = false,
                        Message = "No se pudo guardar el registro",
                        StatusCode = HttpStatusCode.BadRequest
                    };
            }
            catch (Exception ex)
            {
                return new SingleApiResponse<bool>
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Success = false,
                    Message = ex.InnerException?.Message ?? ex.Message
                };
            }
        }

        public async Task<SingleApiResponse<bool>> ChangeStatusAsync(int id, bool activate)
        {
            try
            {
                var roomType = await _repository.GetByIdAsync(id);

                if (roomType is not null)
                {
                    roomType?.IsActive = activate;
                    var success = _repository.Update(roomType!);

                    return success
                        ? new SingleApiResponse<bool>
                        {
                            Data = true,
                            StatusCode = HttpStatusCode.OK,
                            Message = "Registro actualizado exitosamente."
                        }
                        : new SingleApiResponse<bool>
                        {
                            Success = false,
                            Message = "No se pudo guardar el registro",
                            StatusCode = HttpStatusCode.BadRequest
                        };
                }
                else
                {
                    return new SingleApiResponse<bool>
                    {
                        Success = false,
                        Message = "El registro indicado no existe.",
                        StatusCode = HttpStatusCode.NotFound
                    };
                }
            }
            catch (Exception ex)
            {
                return new SingleApiResponse<bool>
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Success = false,
                    Message = ex.InnerException?.Message ?? ex.Message
                };
            }
        }
    }
}
