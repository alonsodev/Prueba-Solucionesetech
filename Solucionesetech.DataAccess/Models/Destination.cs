using System;
using System.Collections.Generic;

#nullable disable

namespace Solucionesetech.DataAccess.Models
{
    public partial class Destination
    {
        public Destination()
        {
            AvailableTravels = new HashSet<AvailableTravel>();
        }

        public int DestinationId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<AvailableTravel> AvailableTravels { get; set; }
    }
}
