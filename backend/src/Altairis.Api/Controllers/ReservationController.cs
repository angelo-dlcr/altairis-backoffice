using Altairis.Application.DTOs;
using Altairis.Application.DTOs.Filters;
using Altairis.Application.Helpers;
using Altairis.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Altairis.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly IReservationService _service;

        public ReservationController(IReservationService service)
        {
            _service = service;
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromQuery] int id)
        {
            var response = await _service.GetByIdAsync(id);

            return response.Success
                ? response.StatusCode == HttpStatusCode.OK
                    ? Ok(response)
                    : NotFound(response)

                : BadRequest(response);
        }

        [HttpGet]
        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _service.GetAllAsync());


        [HttpGet(nameof(GetAllPaged))]
        public async Task<IActionResult> GetAllPaged(
            [FromQuery] PaginationFilters pagination, 
            [FromQuery] ReservationFiltersDTO? filter) 
        => Ok(await _service.GetAllPagedAsync(pagination, filter));

    }
}
