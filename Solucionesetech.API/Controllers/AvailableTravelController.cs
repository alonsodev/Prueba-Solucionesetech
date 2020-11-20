using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Solucionesetech.Command.AvailableTravel.Application.Interfaces;
using Solucionesetech.CrossCutting;
using Solucionesetech.Dtos.Common.Response;
using Solucionesetech.Dtos.AvailableTravel.Request;
using Solucionesetech.Dtos.AvailableTravel.Response;

namespace Solucionesetech.API.Controllers
{
    [Produces("application/json")]
    [Consumes("application/json")]
    [ApiExplorerSettings(GroupName = "AvailableTravel")]
    [Route("AvailableTravel")]
    [ApiController]

    public class AvailableTravelController : ControllerBase
    {
        private readonly IAvailableTravelApplicationService _AvailableTravelApplicationService;
        public AvailableTravelController(IAvailableTravelApplicationService AvailableTravelApplicationService)
        {
            _AvailableTravelApplicationService = AvailableTravelApplicationService;
        }

        [HttpGet]

        public async Task<IActionResult> GetAll()
        {
            var response = await _AvailableTravelApplicationService.GetAll();
            return Ok(response);
        }

        [HttpPost]
        [Route("paginated")]
        public async Task<IActionResult> SearchPaginated([FromBody] SearchPaginatedAvailableTravelRequestDto filters)
        {

            SearchPaginatedResponseDto<SearchAvailableTravelResponseDto> response = await _AvailableTravelApplicationService.SearchPaginated(filters);
            return Ok(response);
        }

        [HttpGet]
        // [Authorize(Permission.ManagementUsers)]
        [Route("{AvailableTravelId}")]


        public IActionResult Get(int AvailableTravelId)
        {
            var response = _AvailableTravelApplicationService.GetById(AvailableTravelId);
            return Ok(response);
        }
        [HttpPost]

        public async Task<IActionResult> Add([FromBody] AddAvailableTravelRequestDto requestDto)
        {
            
            await _AvailableTravelApplicationService.Add(requestDto);
            var response = new
            {
                status = 1,
            };
            return StatusCode(StatusCodes.Status201Created, response);
        }

        [HttpPut]

        public IActionResult Update([FromBody] UpdateAvailableTravelRequestDto updaterAvailableTravelRequestDto)
        {
            
            _AvailableTravelApplicationService.Update(updaterAvailableTravelRequestDto);
            var response = new { status = 1 };
            return Ok(response);
        }

       

        [HttpDelete]
        // [Authorize(Permission.ManagementUsers)]
        [Route("{AvailableTravelId}")]

        public async Task<IActionResult> Delete(int AvailableTravelId)
        {
            await _AvailableTravelApplicationService.Delete(AvailableTravelId);
            return Ok();
        }
    }
}
