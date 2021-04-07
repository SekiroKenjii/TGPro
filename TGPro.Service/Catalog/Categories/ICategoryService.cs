using System.Collections.Generic;
using System.Threading.Tasks;
using TGPro.Data.Entities;
using TGPro.Service.Common;
using TGPro.Service.ViewModel.Categories;

namespace TGPro.Service.Catalog.Categories
{
    public interface ICategoryService
    {
        Task<ApiResponse<string>> Create(CategoryRequest request);

        Task<ApiResponse<string>> Update(int categoryId, CategoryRequest request);

        Task<ApiResponse<string>> Delete(int categoryId);

        Task<ApiResponse<Category>> GetById(int categoryId);

        Task<ApiResponse<List<Category>>> GetListCategory();
    }
}
