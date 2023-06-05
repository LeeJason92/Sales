using System;
using System.Collections.Generic;

namespace CustomerService.Models
{
    public partial class MCustomer
    {
        public int CustomerNo { get; set; }
        public string CustomerName { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public int CustomerType { get; set; }
    }
}
