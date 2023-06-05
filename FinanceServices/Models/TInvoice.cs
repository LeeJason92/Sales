using System;
using System.Collections.Generic;

namespace FinanceServices.Models
{
    public partial class TInvoice
    {
        public int InvoiceNo { get; set; }
        public DateTime InvoiceDate { get; set; }
        public int SalesOrderNo { get; set; }
        public int DeliveryOrderNo { get; set; }
        public int CustomerNo { get; set; }
        public string CustomerName { get; set; } = null!;
        public string CustomerAddress { get; set; } = null!;
        public int ProductNo { get; set; }
        public string ProductDesc { get; set; } = null!;
        public int SparepartNo { get; set; }
        public string SparepartDesc { get; set; } = null!;
        public int Quantity { get; set; }
        public double PricePerUnit { get; set; }
        public double DiscountRate { get; set; }
    }
}
