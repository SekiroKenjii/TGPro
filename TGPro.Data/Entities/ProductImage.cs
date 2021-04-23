using System;
using System.Collections.Generic;
using System.Text;

namespace TGPro.Data.Entities
{
    public class ProductImage
    {
        public int Id { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }

        public string ImageUrl { get; set; }
        public string PublicId { get; set; }
        public string Caption { get; set; }
        public int SortOrder { get; set; }
    }
}
