using System;
using System.Collections.Generic;

#nullable disable

namespace Test.Models.DB
{
    public partial class Tbpayment
    {
        public Tbpayment()
        {
            Tbinvoices = new HashSet<Tbinvoice>();
        }

        public Guid Guidpayment { get; set; }
        public string PaymentType { get; set; }

        public virtual ICollection<Tbinvoice> Tbinvoices { get; set; }
    }
}
