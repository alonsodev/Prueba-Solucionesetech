using Solucionesetech.Dtos.Common.Response;
using Solucionesetech.Dtos.Origin.Request;
using Solucionesetech.Dtos.Origin.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Solucionesetech.Command.Origin.Application.Interfaces
{
    public interface IOriginApplicationService
    {
        Task<SearchPaginatedResponseDto<SearchOriginResponseDto>> GetAll();
        Task<SearchPaginatedResponseDto<SearchOriginResponseDto>> SearchPaginated(SearchPaginatedOriginRequestDto filters);
        Task Add(AddOriginRequestDto requestDto);
        void Update(UpdateOriginRequestDto requestDto);
        DetailOriginResponseDto GetById(int id);
        Task Delete(int OriginId);
    }
}
