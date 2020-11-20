using System;
using System.Collections.Generic;
using System.Text;

namespace Solucionesetech.Dtos.AvailableTravel.Response
{
    public class SearchAvailableTravelResponseDto
    {
        public int AvailableTravelId { get; set; }
        public string Code { get; set; }
        public short Capacity { get; set; }
        public decimal Price { get; set; }
        public int DestinationId { get; set; }
        public int OriginId { get; set; }
        public string DestinationName { get; set; }
        public string OriginName { get; set; }
    }
}
