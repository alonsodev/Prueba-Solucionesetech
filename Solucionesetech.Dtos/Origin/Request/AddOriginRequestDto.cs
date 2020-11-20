using FluentValidation;
using System;

namespace Solucionesetech.Dtos.Origin.Request
{
    public class AddOriginRequestDto
    {

        public int OriginId { get; set; }
        public string Name { get; set; }
    }

    public class AddOriginRequestValidator : AbstractValidator<AddOriginRequestDto>
    {
        public AddOriginRequestValidator()
        {
            RuleFor(p => p.Name).NotEmpty().WithMessage("Nombre es requerido");
        }
    }
}