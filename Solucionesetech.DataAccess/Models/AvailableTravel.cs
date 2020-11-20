using System;
using System.Collections.Generic;

#nullable disable

namespace Solucionesetech.DataAccess.Models
{
    public partial class AvailableTravel
    {
        public AvailableTravel()
        {
            Travels = new HashSet<Travel>();
        }

        public int AvailableTravelId { get; set; }
        public string Code { get; set; }
        public short Capacity { get; set; }
        public decimal Price { get; set; }
        public int DestinationId { get; set; }
        public int OriginId { get; set; }

        public virtual Destination Destination { get; set; }
        public virtual Origin Origin { get; set; }
        public virtual ICollection<Travel> Travels { get; set; }
    }
}
