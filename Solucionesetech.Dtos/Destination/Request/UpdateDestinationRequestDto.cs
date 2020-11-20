using FluentValidation;
using System;

namespace Solucionesetech.Dtos.Destination.Request
{
    public class UpdateDestinationRequestDto
    {
        public int DestinationId { get; set; }
        public string Name { get; set; }
    }

    public class UpdateDestinationRequestValidator : AbstractValidator<UpdateDestinationRequestDto>
    {
        public UpdateDestinationRequestValidator()
        {
            RuleFor(p => p.Name).NotEmpty().WithMessage("Nombre es requerido");

        }
    }
}