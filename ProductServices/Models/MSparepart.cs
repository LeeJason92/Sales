using System;
using System.Collections.Generic;

namespace ProductServices.Models
{
    public partial class MSparepart
    {
        public int SparepartNo { get; set; }
        public string SparepartDesc { get; set; } = null!;
        public string Uom { get; set; } = null!;
        public int SparepartType { get; set; }
        public double StandardCost { get; set; }
        public double Stock { get; set; }
    }
}
