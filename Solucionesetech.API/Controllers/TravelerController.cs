using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Solucionesetech.Command.Traveler.Application.Interfaces;
using Solucionesetech.CrossCutting;
using Solucionesetech.Dtos.Common.Response;
using Solucionesetech.Dtos.Traveler.Request;
using Solucionesetech.Dtos.Traveler.Response;

namespace Solucionesetech.API.Controllers
{
    [Produces("application/json")]
    [Consumes("application/json")]
    [ApiExplorerSettings(GroupName = "Traveler")]
    [Route("Traveler")]
    [ApiController]

    public class TravelerController : ControllerBase
    {
        private readonly ITravelerApplicationService _TravelerApplicationService;
        public TravelerController(ITravelerApplicationService TravelerApplicationService)
        {
            _TravelerApplicationService = TravelerApplicationService;
        }

        [HttpGet]

        public async Task<IActionResult> GetAll()
        {
            var response = await _TravelerApplicationService.GetAll();
            return Ok(response);
        }

        [HttpPost]
        [Route("paginated")]
        public async Task<IActionResult> SearchPaginated([FromBody] SearchPaginatedTravelerRequestDto filters)
        {

            SearchPaginatedResponseDto<SearchTravelerResponseDto> response = await _TravelerApplicationService.SearchPaginated(filters);
            return Ok(response);
        }

        [HttpGet]
        // [Authorize(Permission.ManagementUsers)]
        [Route("{TravelerId}")]


        public IActionResult Get(int TravelerId)
        {
            var response = _TravelerApplicationService.GetById(TravelerId);
            return Ok(response);
        }
        [HttpPost]

        public async Task<IActionResult> Add([FromBody] AddTravelerRequestDto requestDto)
        {
            
            await _TravelerApplicationService.Add(requestDto);
            var response = new
            {
                status = 1,
            };
            return StatusCode(StatusCodes.Status201Created, response);
        }

        [HttpPut]

        public IActionResult Update([FromBody] UpdateTravelerRequestDto updaterTravelerRequestDto)
        {
            
            _TravelerApplicationService.Update(updaterTravelerRequestDto);
            var response = new { status = 1 };
            return Ok(response);
        }

       

        [HttpDelete]
        // [Authorize(Permission.ManagementUsers)]
        [Route("{TravelerId}")]

        public async Task<IActionResult> Delete(int TravelerId)
        {
            await _TravelerApplicationService.Delete(TravelerId);
            return Ok();
        }
    }
}
