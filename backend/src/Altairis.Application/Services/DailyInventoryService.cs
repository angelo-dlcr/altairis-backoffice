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
    public class DailyInventoryService : IDailyInventoryService
    {
        private readonly IBaseRepository<DailyInventory> _repository;

        public DailyInventoryService(IBaseRepository<DailyInventory> repository)
        {
            _repository = repository;
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

        public async Task<PagedListApiResponse<DailyInventoryDTO>> GetAllPagedAsync(PaginationFilters pagination)
        {
            try
            {
                var dailyInventorys = await _repository.GetAllAsync();

                return (dailyInventorys is not null && dailyInventorys.Any())
                    ? new PagedListApiResponse<DailyInventoryDTO>
                        {
                            Items = dailyInventorys?.Skip(pagination.Skip).Take(pagination.Take).Select(h => h.ToDto())?.ToList(),
                            TotalCount = dailyInventorys?.Count ?? 0,
                            PageNumber = pagination.PageNumber,
                            PageSize = pagination.PageSize,
                        }
                    : new PagedListApiResponse<DailyInventoryDTO>
                        {
                            StatusCode = HttpStatusCode.NotFound,
                            Message = "No se hallaron registros.",
                        };
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
    }
}
