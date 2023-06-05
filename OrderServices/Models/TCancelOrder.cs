using System;
using System.Collections.Generic;

namespace OrderServices.Models
{
    public partial class TCancelOrder
    {
        public int SalesOrderNo { get; set; }
        public int Id { get; set; }
        public int StoreNo { get; set; }
        public string StoreDesc { get; set; } = null!;
        public int BusinessUnitNo { get; set; }
        public string BusinessUnitName { get; set; } = null!;
        public int CustomerNo { get; set; }
        public string CustomerName { get; set; } = null!;
        public string CustomerAddress { get; set; } = null!;
        public int ProductNo { get; set; }
        public string ProductDesc { get; set; } = null!;
        public int SparepartNo { get; set; }
        public string SparepartDesc { get; set; } = null!;
        public int Quantity { get; set; }
        public double PricePerUnit { get; set; }
        public DateTime CancelDate { get; set; }
        public string CancelReason { get; set; } = null!;
    }
}
