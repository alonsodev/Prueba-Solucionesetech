using FluentValidation;
using System;

namespace Solucionesetech.Dtos.Travel.Request
{
    public class AddTravelRequestDto
    {

        public int TravelId { get; set; }
        public int AvailableTravelId { get; set; }
        public int TravelerId { get; set; }
    }

    public class AddOriginRequestValidator : AbstractValidator<AddTravelRequestDto>
    {
        public AddOriginRequestValidator()
        {
            RuleFor(p => p.TravelId).NotEmpty().WithMessage("Viaje es requerido");
            RuleFor(p => p.TravelerId).NotEmpty().WithMessage("Viajero es requerido");
        }
    }
}