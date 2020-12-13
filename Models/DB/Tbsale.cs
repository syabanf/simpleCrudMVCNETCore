using System;
using System.Collections.Generic;

#nullable disable

namespace Test.Models.DB
{
    public partial class Tbsale
    {
        public Tbsale()
        {
            Tbinvoices = new HashSet<Tbinvoice>();
        }

        public Guid Guidsales { get; set; }
        public string SalesName { get; set; }

        public virtual ICollection<Tbinvoice> Tbinvoices { get; set; }
    }
}
