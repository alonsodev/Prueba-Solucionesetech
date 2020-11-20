using FluentValidation;
using Solucionesetech.Dtos.Common.Request;
using System;
using System.Collections.Generic;
using System.Text;

namespace Solucionesetech.Dtos.Origin.Request
{
    public class SearchPaginatedOriginRequestDto : SearchPaginatedRequestDto
    {
        public string Name { get; set; }
    }
    public class SearchPaginatedOriginRequestValidator : AbstractValidator<SearchPaginatedOriginRequestDto>
    {
        public SearchPaginatedOriginRequestValidator()
        {
            //RuleFor(p => p.Name).NotEmpty().WithMessage("Name is required");
            //RuleFor(p => p.LastName).NotEmpty();
        }
    }
}
