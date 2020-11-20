using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Solucionesetech.Command.Origin.Application.Interfaces;
using Solucionesetech.CrossCutting;
using Solucionesetech.Dtos.Common.Response;
using Solucionesetech.Dtos.Origin.Request;
using Solucionesetech.Dtos.Origin.Response;

namespace Solucionesetech.API.Controllers
{
    [Produces("application/json")]
    [Consumes("application/json")]
    [ApiExplorerSettings(GroupName = "Origin")]
    [Route("Origin")]
    [ApiController]

    public class OriginController : ControllerBase
    {
        private readonly IOriginApplicationService _OriginApplicationService;
        public OriginController(IOriginApplicationService OriginApplicationService)
        {
            _OriginApplicationService = OriginApplicationService;
        }

        [HttpGet]

        public async Task<IActionResult> GetAll()
        {
            var response = await _OriginApplicationService.GetAll();
            return Ok(response);
        }

        [HttpPost]
        [Route("paginated")]
        public async Task<IActionResult> SearchPaginated([FromBody] SearchPaginatedOriginRequestDto filters)
        {
        
            SearchPaginatedResponseDto<SearchOriginResponseDto> response = await _OriginApplicationService.SearchPaginated(filters);
            return Ok(response);
        }

        [HttpGet]
        // [Authorize(Permission.ManagementUsers)]
        [Route("{OriginId}")]
        public IActionResult Get(int OriginId)
        {
            var response = _OriginApplicationService.GetById(OriginId);
            return Ok(response);
        }
        [HttpPost]

        public async Task<IActionResult> Add([FromBody] AddOriginRequestDto requestDto)
        {
            
            await _OriginApplicationService.Add(requestDto);
            var response = new
            {
                status = 1,
            };
            return StatusCode(StatusCodes.Status201Created, response);
        }

        [HttpPut]

        public IActionResult Update([FromBody] UpdateOriginRequestDto updaterOriginRequestDto)
        {
            
            _OriginApplicationService.Update(updaterOriginRequestDto);
            var response = new { status = 1 };
            return Ok(response);
        }

       

        [HttpDelete]
        // [Authorize(Permission.ManagementUsers)]
        [Route("{OriginId}")]

        public async Task<IActionResult> Delete(int OriginId)
        {
            await _OriginApplicationService.Delete(OriginId);
            return Ok();
        }
    }
}
