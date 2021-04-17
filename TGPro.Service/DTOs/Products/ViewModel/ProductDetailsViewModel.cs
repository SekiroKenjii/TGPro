using System.Collections.Generic;
using TGPro.Data.Entities;

namespace TGPro.Service.DTOs.Products.ViewModel
{
    public class ProductDetailsViewModel
    {
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<Vendor> Vendors { get; set; }
        public IEnumerable<Trademark> Trademarks { get; set; }
        public IEnumerable<Demand> Demands { get; set; }
        public IEnumerable<Condition> Conditions { get; set; }
    }
}
