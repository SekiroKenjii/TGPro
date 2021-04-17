using TGPro.Data.Enums;

namespace TGPro.Service.DTOs.Products.ViewModel
{
    public class TrademarkViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public Status Status { get; set; }
        public string PublicId { get; set; }
    }
}
