using System;
using System.Collections.Generic;

#nullable disable

namespace Solucionesetech.DataAccess.Models
{
    public partial class Travel
    {
        public int TravelId { get; set; }
        public int AvailableTravelId { get; set; }
        public int TravelerId { get; set; }

        public virtual AvailableTravel AvailableTravel { get; set; }
        public virtual Traveler Traveler { get; set; }
    }
}
