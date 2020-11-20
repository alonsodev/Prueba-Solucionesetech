using System;
using System.Collections.Generic;
using System.Text;

namespace Solucionesetech.Dtos.Travel.Response
{
    public class SearchTravelResponseDto
    {
        public int TravelId { get; set; }
        public string TravelerIdentificationDocument { get; set; }
        public string TravelerName { get; set; }
        public string AvailableTravelCode { get; set; }
        public decimal AvailableTravelPrice { get; set; }
        public string OriginName { get; set; }
        public string DestinationName { get; set; }
    }
}
