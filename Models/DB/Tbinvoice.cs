using System;
using System.Collections.Generic;

#nullable disable

namespace Test.Models.DB
{
    public partial class Tbinvoice
    {
        public Tbinvoice()
        {
            TbinvoiceDetails = new HashSet<TbinvoiceDetail>();
        }

        public Guid Guidinvoice { get; set; }
        public string InvoiceNumber { get; set; }
        public DateTime? InvoiceDate { get; set; }
        public Guid? Guidsales { get; set; }
        public Guid? Guidcourier { get; set; }
        public Guid? Guidpayment { get; set; }
        public string ShipTo { get; set; }
        public string TargetTo { get; set; }

        public virtual Tbcourier GuidcourierNavigation { get; set; }
        public virtual Tbpayment GuidpaymentNavigation { get; set; }
        public virtual Tbsale GuidsalesNavigation { get; set; }
        public virtual ICollection<TbinvoiceDetail> TbinvoiceDetails { get; set; }
    }
}
