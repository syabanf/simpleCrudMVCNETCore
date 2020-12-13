using System;
using System.Collections.Generic;

#nullable disable

namespace Test.Models.DB
{
    public partial class Tbcourier
    {
        public Tbcourier()
        {
            Tbinvoices = new HashSet<Tbinvoice>();
        }

        public Guid Guidcourier { get; set; }
        public string CourierName { get; set; }
        public int Cost { get; set; }

        public virtual ICollection<Tbinvoice> Tbinvoices { get; set; }
    }
}
