using System;
using System.Collections.Generic;

namespace OrderServices.Models
{
    public partial class TSalesOrderRevenueReport
    {
        public List<TSalesOrder> ListSalesOrder { get; set; } = new List<TSalesOrder>();
        public double GrandTotalQuantity { get; set; }
        public double GrandTotalSalesOrder { get; set; }
    }
}
