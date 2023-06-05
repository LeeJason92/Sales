using System;
using System.Collections.Generic;

namespace CustomerService.Models
{
    public partial class MCustomerType
    {
        public int CustomerTypeNo { get; set; }
        public string CustomerTypeName { get; set; } = null!;
    }
}
