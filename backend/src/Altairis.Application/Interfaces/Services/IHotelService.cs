using Altairis.Application.DTOs;
using Altairis.Application.DTOs.Filters;
using Altairis.Application.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Altairis.Application.Interfaces.Services
{
    public interface IHotelService
    {
        Task<SingleApiResponse<HotelDTO>> GetByIdAsync(int id);
        Task<ListApiResponse<HotelDTO>> GetAllAsync();
        Task<PagedListApiResponse<HotelDTO>> GetAllPagedAsync(PaginationFilters pagination, HotelFiltersDTO? filter = null);
    }
}
