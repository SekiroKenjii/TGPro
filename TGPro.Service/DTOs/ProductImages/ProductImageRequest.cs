using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace TGPro.Service.DTOs.ProductImages
{
    public class ProductImageRequest
    {
        public IEnumerable<IFormFile> ProductImages { get; set; }
    }
}
