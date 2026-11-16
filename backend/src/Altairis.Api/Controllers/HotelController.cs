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
    public class HotelController : ControllerBase
    {
        private readonly IHotelService _service;

        public HotelController(IHotelService service)
        {
            _service = service;
        }

        [HttpGet(nameof(GetById))]
        public async Task<IActionResult> GetById([FromQuery] int id)
        {
            var response = await _service.GetByIdAsync(id);

            return response.Success
                ? response.StatusCode == HttpStatusCode.OK
                    ? Ok(response)
                    : NotFound(response)

                : BadRequest(response);
        }

        [HttpGet(nameof(GetAll))]
        public async Task<IActionResult> GetAll() 
            => Ok(await _service.GetAllAsync());


        [HttpGet(nameof(GetAllPaged))]
        public async Task<IActionResult> GetAllPaged(
            [FromQuery] PaginationFilters pagination, 
            [FromQuery] HotelFiltersDTO? filter = null) 
        => Ok(await _service.GetAllPagedAsync(pagination, filter));


        [HttpPost(nameof(Create))]
        public async Task<IActionResult> Create([FromBody] HotelDTO hotel)
        {
            var response = await _service.AddAsync(hotel);

            return response.Success
                ? Ok(response)
                : BadRequest(response);
        }

        [HttpPut(nameof(Update))]
        public async Task<IActionResult> Update([FromBody] HotelDTO hotel)
        {
            var response = await _service.AddAsync(hotel);

            return response.Success
                ? Ok(response)
                : BadRequest(response);
        }

        [HttpPut("ChangeStatus/{id:int}")]
        public async Task<IActionResult> ChangeStatus([FromRoute] int id, [FromBody] bool activate)
        {
            var response = await _service.ChangeStatusAsync(id, activate);

            switch (response.StatusCode)
            {
                case (HttpStatusCode.NotFound):
                    {
                        return NotFound(response);
                    }
                case (HttpStatusCode.BadRequest):
                    {
                        return BadRequest(response);
                    }
                case (HttpStatusCode.InternalServerError):
                    {
                        return Conflict(response);
                    }
                default:
                    {
                        return Ok(response);
                    }
            }
        }

    }
}
