using Solucionesetech.Dtos.Common.Response;
using Solucionesetech.Dtos.Traveler.Request;
using Solucionesetech.Dtos.Traveler.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Solucionesetech.Command.Traveler.Application.Interfaces
{
    public interface ITravelerApplicationService
    {
        Task Add(AddTravelerRequestDto requestDto);
        void Update(UpdateTravelerRequestDto requestDto);
        DetailTravelerResponseDto GetById(int id);
        Task Delete(int TravelerId);

        Task<SearchPaginatedResponseDto<SearchTravelerResponseDto>> GetAll();
        Task<SearchPaginatedResponseDto<SearchTravelerResponseDto>> SearchPaginated(SearchPaginatedTravelerRequestDto filters);
        
    }
}
