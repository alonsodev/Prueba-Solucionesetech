using FluentValidation;
using System;

namespace Solucionesetech.Dtos.Traveler.Request
{
    public class UpdateTravelerRequestDto
    {
        public short TravelerId { get; set; }
        public string IdentificationDocument { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
    }

    public class UpdateTravelerRequestValidator : AbstractValidator<UpdateTravelerRequestDto>
    {
        public UpdateTravelerRequestValidator()
        {
            RuleFor(p => p.IdentificationDocument).NotEmpty().WithMessage("Documento de identidad es requerido");
            RuleFor(p => p.Name).NotEmpty().WithMessage("Nombre es requerido");

        }
    }
}