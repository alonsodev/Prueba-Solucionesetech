using Solucionesetech.Dtos.Common.Response;
using Solucionesetech.Dtos.AvailableTravel.Request;
using Solucionesetech.Dtos.AvailableTravel.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Solucionesetech.Command.AvailableTravel.Application.Interfaces
{
    public interface IAvailableTravelApplicationService
    {
        Task Add(AddAvailableTravelRequestDto requestDto);
        void Update(UpdateAvailableTravelRequestDto requestDto);
        DetailAvailableTravelResponseDto GetById(int id);
        Task Delete(int AvailableTravelId);

        Task<SearchPaginatedResponseDto<SearchAvailableTravelResponseDto>> GetAll();
        Task<SearchPaginatedResponseDto<SearchAvailableTravelResponseDto>> SearchPaginated(SearchPaginatedAvailableTravelRequestDto filters);
        
    }
}
