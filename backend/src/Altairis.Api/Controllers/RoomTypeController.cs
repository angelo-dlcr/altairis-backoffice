using Altairis.Application.DTOs;
using Altairis.Application.DTOs.Filters;
using Altairis.Application.Helpers;
using Altairis.Application.Interfaces.Services;
using Azure;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Altairis.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomTypeController : ControllerBase
    {
        private readonly IRoomTypeService _service;

        public RoomTypeController(IRoomTypeService service)
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
        public async Task<IActionResult> GetAll() => Ok(await _service.GetAllAsync());


        [HttpGet(nameof(GetAllByHotelId))]
        public async Task<IActionResult> GetAllByHotelId([FromQuery] int hotelId)
        {
            var result = await _service.GetAllByHotelIdAsync(hotelId);

            switch (result.StatusCode)
            {
                case (HttpStatusCode.NotFound):
                        return NotFound(result);

                case (HttpStatusCode.InternalServerError):
                        return BadRequest(result);

                default: return Ok(result);
            }
        }


        [HttpGet(nameof(GetAllPaged))]
        public async Task<IActionResult> GetAllPaged([FromQuery] PaginationFilters pagination, [FromQuery] RoomTypeFiltersDTO? filter = null) 
            => Ok(await _service.GetAllPagedAsync(pagination, filter));


        [HttpPost(nameof(Create))]
        public async Task<IActionResult> Create([FromBody] RoomTypeDTO roomType)
        {
            var response = await _service.AddAsync(roomType);

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
                        return NotFound(response);

                case (HttpStatusCode.BadRequest):
                        return BadRequest(response);

                case (HttpStatusCode.InternalServerError):
                        return Conflict(response);

                default: return Ok(response);
            }
        }

    }
}
