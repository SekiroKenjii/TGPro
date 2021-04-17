namespace TGPro.Service.DTOs.Products.ViewModel
{
    public class ProductImageViewModel
    {
        public int ProductId { get; set; }

        public string ImageUrl { get; set; }
        public string PublicId { get; set; }
        public string Caption { get; set; }
        public bool IsDefault { get; set; }
        public int SortOrder { get; set; }
    }
}
