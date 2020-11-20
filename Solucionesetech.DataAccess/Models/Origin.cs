using System;
using System.Collections.Generic;

#nullable disable

namespace Solucionesetech.DataAccess.Models
{
    public partial class Origin
    {
        public Origin()
        {
            AvailableTravels = new HashSet<AvailableTravel>();
        }

        public int OriginId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<AvailableTravel> AvailableTravels { get; set; }
    }
}
