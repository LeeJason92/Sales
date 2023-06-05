using System;
using System.Collections.Generic;

namespace PricingServices.Models
{
    public partial class MPricing
    {
        public int Id { get; set; }
        public int ProductNo { get; set; }
        public int StoreNo { get; set; }
        public int CustomerTypeNo { get; set; }
        public DateTime ValidFrom { get; set; }
        public DateTime ValidTo { get; set; }
        public double Amount { get; set; }
    }
}
