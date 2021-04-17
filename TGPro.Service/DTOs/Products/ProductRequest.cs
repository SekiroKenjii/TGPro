namespace TGPro.Service.DTOs.Products
{
    public class ProductRequest
    {
        public string Name { get; set; }
        public int VendorId { get; set; }
        public int CategoryId { get; set; }
        public int ConditionId { get; set; }
        public int DemandId { get; set; }
        public int TrademarkId { get; set; }
        public string Cpu { get; set; }
        public string Screen { get; set; }
        public string Ram { get; set; }
        public string Gpu { get; set; }
        public string Storage { get; set; }
        public string Pin { get; set; }
        public string Connection { get; set; }
        public float Weight { get; set; }
        public string OS { get; set; }
        public string Color { get; set; }
        public string Warranty { get; set; }
        public double Price { get; set; }
        public int UnitsInStock { get; set; }
        public int UnitsOnOrder { get; set; }
        public string Description { get; set; }
        public bool Discontinued { get; set; }
    }
}
