using FluentValidation;
using Solucionesetech.Dtos.Common.Request;
using System;
using System.Collections.Generic;
using System.Text;

namespace Solucionesetech.Dtos.Destination.Request
{
    public class SearchPaginatedDestinationRequestDto : SearchPaginatedRequestDto
    {
        public string Name { get; set; }
    }
    public class SearchPaginatedDestinationRequestValidator : AbstractValidator<SearchPaginatedDestinationRequestDto>
    {
        public SearchPaginatedDestinationRequestValidator()
        {
            //RuleFor(p => p.Name).NotEmpty().WithMessage("Name is required");
            //RuleFor(p => p.LastName).NotEmpty();
        }
    }
}
