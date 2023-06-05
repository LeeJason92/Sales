namespace WarehouseServices.Models
{
    public class SalesOrderModel
    {
        public int SalesOrderNo { get; set; }
        public DateTime SalesOrderDate { get; set; }
        public int PurchaseOrderNo { get; set; }
        public int BusinessUnitNo { get; set; }
        public string BusinessUnitName { get; set; } = null!;
        public int StoreNo { get; set; }
        public string StoreDesc { get; set; } = null!;
        public int CustomerNo { get; set; }
        public string CustomerName { get; set; } = null!;
        public string CustomerAddress { get; set; } = null!;
        public int ProductNo { get; set; }
        public string ProductDesc { get; set; } = null!;
        public int SparepartNo { get; set; }
        public string SparepartDesc { get; set; } = null!;
        public int Quantity { get; set; }
        public double PricePerUnit { get; set; }
        public double DiscountRate { get; set; }

    }
}
