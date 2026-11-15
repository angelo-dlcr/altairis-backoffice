using Altairis.Application.DTOs;
using Altairis.Application.Helpers;
using Altairis.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Altairis.Application.Interfaces.Services
{
    public interface IDailyInventoryService
    {
        Task<SingleApiResponse<DailyInventoryDTO>> GetByIdAsync(int id);
        Task<ListApiResponse<DailyInventoryDTO>> GetAllAsync();
        Task<PagedListApiResponse<DailyInventoryDTO>> GetAllPagedAsync(PaginationFilters pagination);
    }
}
