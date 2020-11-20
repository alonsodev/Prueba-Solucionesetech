using Solucionesetech.Dtos.Common.Response;
using System;
using System.Collections.Generic;

namespace Solucionesetech.Dtos.AvailableTravel.Response
{
    public class AvailableTravelResponseDto
    {
        public int AvailableTravelId { get; set; }
        public string Code { get; set; }
        public short Capacity { get; set; }
        public decimal Price { get; set; }
        public int DestinationId { get; set; }
        public int OriginId { get; set; }

    }

   
}