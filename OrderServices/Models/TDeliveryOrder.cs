using System;
using System.Collections.Generic;

namespace OrderServices.Models
{
    public partial class TDeliveryOrder
    {
        public int DeliveryOrderNo { get; set; }
        public DateTime DeliveryOrderDate { get; set; }
        public int OutboundNo { get; set; }
        public int SalesOrderNo { get; set; }
        public int PurchaseOrderNo { get; set; }
        public int Id { get; set; }
        public int CustomerNo { get; set; }
        public string CustomerName { get; set; } = null!;
        public string CustomerAddress { get; set; } = null!;
        public int ProductNo { get; set; }
        public string ProductDesc { get; set; } = null!;
        public int SparepartNo { get; set; }
        public string SparepartDesc { get; set; } = null!;
        public int Quantity { get; set; }
    }
}
