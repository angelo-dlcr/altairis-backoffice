using Altairis.Application.DTOs;
using Altairis.Application.DTOs.Filters;
using Altairis.Application.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Altairis.Application.Interfaces.Services
{
    public interface IRoomTypeService
    {
        Task<SingleApiResponse<RoomTypeDTO>> GetByIdAsync(int id);
        Task<ListApiResponse<RoomTypeDTO>> GetAllAsync();
        Task<PagedListApiResponse<RoomTypeDTO>> GetAllPagedAsync(PaginationFilters pagination, RoomTypeFiltersDTO? filter = null);

        Task<SingleApiResponse<bool>> AddAsync(RoomTypeDTO dto);
        Task<SingleApiResponse<bool>> ChangeStatusAsync(int id, bool activate);
    }
}
