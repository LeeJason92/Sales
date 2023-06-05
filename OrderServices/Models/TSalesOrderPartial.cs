using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrderServices.Models
{
    public partial class TSalesOrder
    {
        [NotMapped]
        public double TotalPrice { get; set; }
    }
}
