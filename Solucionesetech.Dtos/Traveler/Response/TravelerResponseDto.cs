using Solucionesetech.Dtos.Common.Response;
using System;
using System.Collections.Generic;

namespace Solucionesetech.Dtos.Traveler.Response
{
    public class TravelerResponseDto
    {
        public int TravelerId { get; set; }
        public string IdentificationDocument { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }

    }

   
}