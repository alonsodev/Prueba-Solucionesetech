using FluentValidation;
using Solucionesetech.Dtos.Common.Request;
using System;
using System.Collections.Generic;
using System.Text;

namespace Solucionesetech.Dtos.AvailableTravel.Request
{
    public class SearchPaginatedAvailableTravelRequestDto : SearchPaginatedRequestDto
    {
        public string Code { get; set; }
        public short Capacity { get; set; }
        public decimal Price { get; set; }
        public int DestinationName { get; set; }
        public int OriginName { get; set; }
    }
    public class SearchPaginatedAvailableTravelRequestValidator : AbstractValidator<SearchPaginatedAvailableTravelRequestDto>
    {
        public SearchPaginatedAvailableTravelRequestValidator()
        {
            //RuleFor(p => p.Name).NotEmpty().WithMessage("Name is required");
            //RuleFor(p => p.LastName).NotEmpty();
        }
    }
}
