using Altairis.Application.DTOs;
using Altairis.Application.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Altairis.Application.Interfaces.Services
{
    public interface IStatisticsService
    {
        Task<SingleApiResponse<StatisticsDTO>> GetStatisticsAsync();
    }
}
