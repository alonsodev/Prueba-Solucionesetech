using Solucionesetech.Dtos.Common.Response;
using Solucionesetech.Dtos.Destination.Request;
using Solucionesetech.Dtos.Destination.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Solucionesetech.Command.Destination.Application.Interfaces
{
    public interface IDestinationApplicationService
    {
        Task Add(AddDestinationRequestDto requestDto);
        void Update(UpdateDestinationRequestDto requestDto);
        DetailDestinationResponseDto GetById(int id);
        Task Delete(int DestinationId);

        Task<SearchPaginatedResponseDto<SearchDestinationResponseDto>> GetAll();
        Task<SearchPaginatedResponseDto<SearchDestinationResponseDto>> SearchPaginated(SearchPaginatedDestinationRequestDto filters);
        
    }
}
