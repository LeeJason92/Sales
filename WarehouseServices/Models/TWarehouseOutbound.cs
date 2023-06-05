using System;
using System.Collections.Generic;

namespace WarehouseServices.Models
{
    public partial class TWarehouseOutbound
    {
        public int OutboundNo { get; set; }
        public DateTime OutboundDate { get; set; }
        public int SalesOrderNo { get; set; }
        public int ProductNo { get; set; }
        public string ProductDesc { get; set; } = null!;
        public int SparepartNo { get; set; }
        public string SparepartDesc { get; set; } = null!;
        public int Quantity { get; set; }
    }
}
