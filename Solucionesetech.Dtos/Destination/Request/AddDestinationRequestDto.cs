using FluentValidation;
using System;

namespace Solucionesetech.Dtos.Destination.Request
{
    public class AddDestinationRequestDto
    {

        public int DestinationId { get; set; }
        public string Name { get; set; }
    }

    public class AddDestinationRequestValidator : AbstractValidator<AddDestinationRequestDto>
    {
        public AddDestinationRequestValidator()
        {
            RuleFor(p => p.Name).NotEmpty().WithMessage("Nombre es requerido");
        }
    }
}