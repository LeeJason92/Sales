using System;
using System.Collections.Generic;

namespace OrderServices.Models
{
    public partial class VOrderDetail
    {
        public int SalesOrderNo { get; set; }
        public DateTime SalesOrderDate { get; set; }
        public int BusinessUnitNo { get; set; }
        public string BusinessUnitName { get; set; } = null!;
        public int CustomerNo { get; set; }
        public string CustomerName { get; set; } = null!;
        public string StoreDesc { get; set; } = null!;
        public int ProductNo { get; set; }
        public string ProductDesc { get; set; } = null!;
        public int Quantity { get; set; }
        public double SubTotal { get; set; }
        public double DiscountRate { get; set; }
        public double Discount { get; set; }
        public double Total { get; set; }
        public string StatusOrder { get; set; } = null!;
        public DateTime DeliveryDate { get; set; }
    }
}
