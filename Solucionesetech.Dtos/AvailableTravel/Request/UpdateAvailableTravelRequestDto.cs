using FluentValidation;
using System;

namespace Solucionesetech.Dtos.AvailableTravel.Request
{
    public class UpdateAvailableTravelRequestDto
    {
        public int AvailableTravelId { get; set; }
        public string Code { get; set; }
        public short Capacity { get; set; }
        public decimal Price { get; set; }
        public int DestinationId { get; set; }
        public int OriginId { get; set; }
    }

    public class UpdateAvailableTravelRequestValidator : AbstractValidator<UpdateAvailableTravelRequestDto>
    {
        public UpdateAvailableTravelRequestValidator()
        {
            RuleFor(p => p.Code).NotEmpty().WithMessage("Código es requerido");
            RuleFor(p => p.Capacity).NotEmpty().WithMessage("Número de plazas es requerido");
            RuleFor(p => p.Price).NotEmpty().WithMessage("Precio es requerido");
            RuleFor(p => p.DestinationId).NotEmpty().WithMessage("Destino es requerido");
            RuleFor(p => p.OriginId).NotEmpty().WithMessage("Origen es requerido");

        }
    }
}