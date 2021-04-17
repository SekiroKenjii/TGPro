using System.Collections.Generic;
using System.Threading.Tasks;
using TGPro.Service.Common;
using TGPro.Service.DTOs.Products;
using TGPro.Service.DTOs.Products.ViewModel;

namespace TGPro.Service.Catalog.Products
{
    public interface IProductService
    {
        Task<ApiResponse<IEnumerable<ProductViewModel>>> GetListProduct();
        Task<ApiResponse<ProductDetailsViewModel>> GetAllProductDetails();
        Task<ApiResponse<string>> Create(ProductRequest request);

        Task<ApiResponse<string>> Update(int productId, ProductRequest request);

        Task<ApiResponse<string>> Delete(int productId);

        Task<ApiResponse<ProductViewModel>> GetById(int productId);
    }
}
