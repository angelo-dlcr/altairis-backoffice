using Altairis.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Altairis.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StatisticsController : ControllerBase
    {
        private readonly IStatisticsService _service;

        public StatisticsController(IStatisticsService service)
        {
            _service = service;
        }

        [HttpGet(nameof(GetStatistics))]
        public async Task<IActionResult> GetStatistics()
            => Ok(await _service.GetStatisticsAsync());
    }
}
