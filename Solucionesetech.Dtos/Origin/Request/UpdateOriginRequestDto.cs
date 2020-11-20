using FluentValidation;
using System;

namespace Solucionesetech.Dtos.Origin.Request
{
    public class UpdateOriginRequestDto
    {
        public int OriginId { get; set; }
        public string Name { get; set; }
    }

    public class UpdateOriginRequestValidator : AbstractValidator<UpdateOriginRequestDto>
    {
        public UpdateOriginRequestValidator()
        {
            RuleFor(p => p.Name).NotEmpty().WithMessage("Nombre es requerido");

        }
    }
}