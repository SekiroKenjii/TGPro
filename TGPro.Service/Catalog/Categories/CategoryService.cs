using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TGPro.Data.EF;
using TGPro.Data.Entities;
using TGPro.Service.Common;
using TGPro.Service.DTOs.Categories;

namespace TGPro.Service.Catalog.Categories
{
    public class CategoryService : ICategoryService
    {
        private readonly TGProDbContext _db;
        public CategoryService(TGProDbContext db)
        {
            _db = db;
        }

        public async Task<ApiResponse<string>> Create(CategoryRequest request)
        {
            if (string.IsNullOrEmpty(request.Name))
                return new ApiErrorResponse<string>(ConstantStrings.emptyNameFieldError);
            var category = new Category()
            {
                Name = request.Name,
                Description = request.Description
            };
            _db.Categories.Add(category);
            await _db.SaveChangesAsync();
            return new ApiSuccessResponse<string>(ConstantStrings.addSuccessfully);
        }

        public async Task<ApiResponse<string>> Delete(int categoryId)
        {
            var categoryFromDb = await _db.Categories.FindAsync(categoryId);
            if (categoryFromDb == null) 
                return new ApiErrorResponse<string>(ConstantStrings.FindByIdError(categoryId));
            _db.Categories.Remove(categoryFromDb);
            await _db.SaveChangesAsync();
            return new ApiSuccessResponse<string>(ConstantStrings.deleteSuccessfully);
        }

        public async Task<ApiResponse<Category>> GetById(int categoryId)
        {
            var categoryFromDb = await _db.Categories.FindAsync(categoryId);
            if (categoryFromDb == null)
                return new ApiErrorResponse<Category>(ConstantStrings.FindByIdError(categoryId));
            return new ApiSuccessResponse<Category>(categoryFromDb);
        }

        public async Task<ApiResponse<List<Category>>> GetListCategory()
        {
            List<Category> lstCategory = await _db.Categories.OrderBy(c => c.Name).ToListAsync();
            if (lstCategory.Count == 0)
                return new ApiErrorResponse<List<Category>>(ConstantStrings.getAllError);
            return new ApiSuccessResponse<List<Category>>(lstCategory);
        }

        public async Task<ApiResponse<string>> Update(int categoryId, CategoryRequest request)
        {
            if (string.IsNullOrEmpty(request.Name))
                return new ApiErrorResponse<string>(ConstantStrings.emptyNameFieldError);
            var categoryFromDb = await _db.Categories.FindAsync(categoryId);
            if (categoryFromDb == null)
                return new ApiErrorResponse<string>(ConstantStrings.FindByIdError(categoryId));
            categoryFromDb.Name = request.Name;
            categoryFromDb.Description = request.Description;
            _db.Categories.Update(categoryFromDb);
            await _db.SaveChangesAsync();
            return new ApiSuccessResponse<string>(ConstantStrings.editSuccessfully);
        }
    }
}
