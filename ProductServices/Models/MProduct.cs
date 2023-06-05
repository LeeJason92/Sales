using System;
using System.Collections.Generic;

namespace ProductServices.Models
{
    public partial class MProduct
    {
        public int ProductNo { get; set; }
        public string ProductDesc { get; set; } = null!;
        public int ProductType { get; set; }
        public string ProductBrand { get; set; } = null!;
        public string Uom { get; set; } = null!;
        public double Cogs { get; set; }
        public double Stock { get; set; }
    }
}
