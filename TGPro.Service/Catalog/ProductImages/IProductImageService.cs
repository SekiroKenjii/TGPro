using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TGPro.Service.Common;
using TGPro.Service.DTOs.ProductImages;

namespace TGPro.Service.Catalog.ProductImages
{
    public interface IProductImageService
    {
        Task<ApiResponse<string>> Create(int productId, ProductImageRequest request);
        Task<ApiResponse<string>> Update(int productId, ProductImageRequest request);
        Task<ApiResponse<string>> Delete(int productImageId);
    }
}
