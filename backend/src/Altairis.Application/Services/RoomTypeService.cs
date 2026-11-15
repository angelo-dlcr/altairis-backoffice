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
    public class RoomTypeService : IRoomTypeService
    {
        private readonly IBaseRepository<RoomType> _repository;

        public RoomTypeService(IBaseRepository<RoomType> repository)
        {
            _repository = repository;
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

        public async Task<PagedListApiResponse<RoomTypeDTO>> GetAllPagedAsync(PaginationFilters pagination)
        {
            try
            {
                var roomTypes = await _repository.GetAllAsync();

                return (roomTypes is not null && roomTypes.Any())
                    ? new PagedListApiResponse<RoomTypeDTO>
                        {
                            Items = roomTypes?.Skip(pagination.Skip).Take(pagination.Take).Select(h => h.ToDto())?.ToList(),
                            TotalCount = roomTypes?.Count ?? 0,
                            PageNumber = pagination.PageNumber,
                            PageSize = pagination.PageSize,
                        }
                    : new PagedListApiResponse<RoomTypeDTO>
                        {
                            StatusCode = HttpStatusCode.NotFound,
                            Message = "No se hallaron registros.",
                        };
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
    }
}
