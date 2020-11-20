using Solucionesetech.Dtos.Common.Response;
using Solucionesetech.Dtos.Travel.Request;
using Solucionesetech.Dtos.Travel.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Solucionesetech.Command.Travel.Application.Interfaces
{
    public interface ITravelApplicationService
    {
        Task Add(AddTravelRequestDto requestDto);
        void Update(UpdateTravelRequestDto requestDto);
        DetailTravelResponseDto GetById(int id);
        Task Delete(int TravelId);

        Task<SearchPaginatedResponseDto<SearchTravelResponseDto>> GetAll();
        Task<SearchPaginatedResponseDto<SearchTravelResponseDto>> SearchPaginated(SearchPaginatedTravelRequestDto filters);
        
    }
}
