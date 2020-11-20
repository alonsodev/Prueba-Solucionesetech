using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Solucionesetech.Command.Destination.Application.Interfaces;
using Solucionesetech.CrossCutting;
using Solucionesetech.Dtos.Common.Response;
using Solucionesetech.Dtos.Destination.Request;
using Solucionesetech.Dtos.Destination.Response;

namespace Solucionesetech.API.Controllers
{
    [Produces("application/json")]
    [Consumes("application/json")]
    [ApiExplorerSettings(GroupName = "Destination")]
    [Route("Destination")]
    [ApiController]

    public class DestinationController : ControllerBase
    {
        private readonly IDestinationApplicationService _DestinationApplicationService;
        public DestinationController(IDestinationApplicationService DestinationApplicationService)
        {
            _DestinationApplicationService = DestinationApplicationService;
        }

        [HttpGet]

        public async Task<IActionResult> GetAll()
        {
            var response = await _DestinationApplicationService.GetAll();
            return Ok(response);
        }

        [HttpPost]
        [Route("paginated")]
        public async Task<IActionResult> SearchPaginated([FromBody] SearchPaginatedDestinationRequestDto filters)
        {

            SearchPaginatedResponseDto<SearchDestinationResponseDto> response = await _DestinationApplicationService.SearchPaginated(filters);
            return Ok(response);
        }

        [HttpGet]
        // [Authorize(Permission.ManagementUsers)]
        [Route("{DestinationId}")]


        public IActionResult Get(int DestinationId)
        {
            var response = _DestinationApplicationService.GetById(DestinationId);
            return Ok(response);
        }
        [HttpPost]

        public async Task<IActionResult> Add([FromBody] AddDestinationRequestDto requestDto)
        {
            
            await _DestinationApplicationService.Add(requestDto);
            var response = new
            {
                status = 1,
            };
            return StatusCode(StatusCodes.Status201Created, response);
        }

        [HttpPut]

        public IActionResult Update([FromBody] UpdateDestinationRequestDto updaterDestinationRequestDto)
        {
            
            _DestinationApplicationService.Update(updaterDestinationRequestDto);
            var response = new { status = 1 };
            return Ok(response);
        }

       

        [HttpDelete]
        // [Authorize(Permission.ManagementUsers)]
        [Route("{DestinationId}")]

        public async Task<IActionResult> Delete(int DestinationId)
        {
            await _DestinationApplicationService.Delete(DestinationId);
            return Ok();
        }
    }
}
