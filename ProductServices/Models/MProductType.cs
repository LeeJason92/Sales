using System;
using System.Collections.Generic;

namespace ProductServices.Models
{
    public partial class MProductType
    {
        public int ProductTypeNo { get; set; }
        public string ProductTypeName { get; set; } = null!;
    }
}
