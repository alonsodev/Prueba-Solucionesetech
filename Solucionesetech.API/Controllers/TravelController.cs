using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Solucionesetech.Command.Travel.Application.Interfaces;
using Solucionesetech.CrossCutting;
using Solucionesetech.Dtos.Common.Response;
using Solucionesetech.Dtos.Travel.Request;
using Solucionesetech.Dtos.Travel.Response;

namespace Solucionesetech.API.Controllers
{
    [Produces("application/json")]
    [Consumes("application/json")]
    [ApiExplorerSettings(GroupName = "Travel")]
    [Route("Travel")]
    [ApiController]

    public class TravelController : ControllerBase
    {
        private readonly ITravelApplicationService _TravelApplicationService;
        public TravelController(ITravelApplicationService TravelApplicationService)
        {
            _TravelApplicationService = TravelApplicationService;
        }

        [HttpGet]

        public async Task<IActionResult> GetAll()
        {
            var response = await _TravelApplicationService.GetAll();
            return Ok(response);
        }

        [HttpPost]
        [Route("paginated")]
        public async Task<IActionResult> SearchPaginated([FromBody] SearchPaginatedTravelRequestDto filters)
        {

            SearchPaginatedResponseDto<SearchTravelResponseDto> response = await _TravelApplicationService.SearchPaginated(filters);
            return Ok(response);
        }

        [HttpGet]
        // [Authorize(Permission.ManagementUsers)]
        [Route("{TravelId}")]


        public IActionResult Get(int TravelId)
        {
            var response = _TravelApplicationService.GetById(TravelId);
            return Ok(response);
        }
        [HttpPost]

        public async Task<IActionResult> Add([FromBody] AddTravelRequestDto requestDto)
        {
            
            await _TravelApplicationService.Add(requestDto);
            var response = new
            {
                status = 1,
            };
            return StatusCode(StatusCodes.Status201Created, response);
        }

        [HttpPut]

        public IActionResult Update([FromBody] UpdateTravelRequestDto updaterTravelRequestDto)
        {
            
            _TravelApplicationService.Update(updaterTravelRequestDto);
            var response = new { status = 1 };
            return Ok(response);
        }

       

        [HttpDelete]
        // [Authorize(Permission.ManagementUsers)]
        [Route("{TravelId}")]

        public async Task<IActionResult> Delete(int TravelId)
        {
            await _TravelApplicationService.Delete(TravelId);
            return Ok();
        }
    }
}
