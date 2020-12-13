using System;
using System.Collections.Generic;

#nullable disable

namespace Test.Models.DB
{
    public partial class TbinvoiceDetail
    {
        public Guid GuidinvoiceDetail { get; set; }
        public Guid Guidinvoice { get; set; }
        public string Item { get; set; }
        public int Weight { get; set; }
        public int Qty { get; set; }
        public int UnitPrice { get; set; }

        public virtual Tbinvoice GuidinvoiceNavigation { get; set; }
    }
}
