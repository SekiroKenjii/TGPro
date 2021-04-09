using Microsoft.AspNetCore.Http;
using TGPro.Data.Enums;

namespace TGPro.Service.DTOs.Trademarks
{
    public class TrademarkRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Status Status { get; set; }
        public IFormFile Image { get; set; }
    }
}
