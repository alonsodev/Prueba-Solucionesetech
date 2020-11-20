using FluentValidation;
using Solucionesetech.Dtos.Common.Request;
using System;
using System.Collections.Generic;
using System.Text;

namespace Solucionesetech.Dtos.Traveler.Request
{
    public class SearchPaginatedTravelerRequestDto : SearchPaginatedRequestDto
    {
        public short TravelerId { get; set; }
        public string IdentificationDocument { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
    }
    public class SearchPaginatedTravelerRequestValidator : AbstractValidator<SearchPaginatedTravelerRequestDto>
    {
        public SearchPaginatedTravelerRequestValidator()
        {
            //RuleFor(p => p.Name).NotEmpty().WithMessage("Name is required");
            //RuleFor(p => p.LastName).NotEmpty();
        }
    }
}
