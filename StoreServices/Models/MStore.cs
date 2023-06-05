using System;
using System.Collections.Generic;

namespace StoreServices.Models
{
    public partial class MStore
    {
        public int StoreNo { get; set; }
        public int StoreArea { get; set; }
        public string StoreDesc { get; set; } = null!;
        public string StoreAddress { get; set; } = null!;
        public string StorePhone { get; set; } = null!;
    }
}
