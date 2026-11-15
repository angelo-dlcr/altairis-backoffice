using Altairis.Application.DTOs;
using Altairis.Application.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Altairis.Application.Interfaces.Services
{
    public interface IReservationService
    {
        Task<SingleApiResponse<ReservationDTO>> GetByIdAsync(int id);
        Task<ListApiResponse<ReservationDTO>> GetAllAsync();
        Task<PagedListApiResponse<ReservationDTO>> GetAllPagedAsync(PaginationFilters pagination);
    }
}
