using System.Collections.Generic;
using System.Threading.Tasks;
using TGPro.Data.Entities;
using TGPro.Service.ViewModel.Categories;

namespace TGPro.Service.Catalog.Categories
{
    public interface ICategoryService
    {
        Task<int> Create(CategoryRequest request);

        Task<int> Update(int categoryId, CategoryRequest request);

        Task<int> Delete(int categoryId);

        Task<Category> GetById(int categoryId);

        Task<List<Category>> GetListCategory();
    }
}
