using FluentValidation;
using Solucionesetech.Dtos.Common.Request;
using System;
using System.Collections.Generic;
using System.Text;

namespace Solucionesetech.Dtos.Travel.Request
{
    public class SearchPaginatedTravelRequestDto : SearchPaginatedRequestDto
    {
        public int TravelId { get; set; }
        public string TravelerIdentificationDocument { get; set; }
        public string TravelerName { get; set; }
        public string AvailableTravelCode { get; set; }
    }
    public class SearchPaginatedTravelRequestValidator : AbstractValidator<SearchPaginatedTravelRequestDto>
    {
        public SearchPaginatedTravelRequestValidator()
        {
            //RuleFor(p => p.Name).NotEmpty().WithMessage("Name is required");
            //RuleFor(p => p.LastName).NotEmpty();
        }
    }
}
