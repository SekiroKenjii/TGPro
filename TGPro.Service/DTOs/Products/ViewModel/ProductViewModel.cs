using System.Collections.Generic;

namespace TGPro.Service.DTOs.Products.ViewModel
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public VendorViewModel Vendor { get; set; }
        public CategoryViewModel Category { get; set; }
        public ConditionViewModel Condition { get; set; }
        public DemandViewModel Demand { get; set; }
        public TrademarkViewModel Trademark { get; set; }
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
        public IEnumerable<ProductImageViewModel> ProductImages { get; set; }
    }
}
