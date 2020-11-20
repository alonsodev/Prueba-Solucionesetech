using System;
using System.Collections.Generic;
using System.Text;

namespace Solucionesetech.Dtos.Traveler.Response
{
    public class SearchTravelerResponseDto
    {
        public int TravelerId { get; set; }
        public string IdentificationDocument { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
    }
}
