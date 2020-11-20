using System;
using System.Collections.Generic;

#nullable disable

namespace Solucionesetech.DataAccess.Models
{
    public partial class Traveler
    {
        public Traveler()
        {
            Travels = new HashSet<Travel>();
        }

        public int TravelerId { get; set; }
        public string IdentificationDocument { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }

        public virtual ICollection<Travel> Travels { get; set; }
    }
}
