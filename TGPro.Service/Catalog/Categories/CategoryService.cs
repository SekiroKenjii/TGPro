using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TGPro.Data.EF;
using TGPro.Data.Entities;
using TGPro.Service.Catalog.Categories;
using TGPro.Service.Exceptions;
using TGPro.Service.ViewModel.Categories;

namespace TGPro.Service.Catalog.Categories
{
    public class CategoryService : ICategoryService
    {
        private readonly TGProDbContext _db;
        public CategoryService(TGProDbContext db)
        {
            _db = db;
        }

        public async Task<int> Create(CategoryRequest request)
        {
            if (request.Name == string.Empty) throw new TGProException("Name cannot be null");
            else
            {
                var category = new Category()
                {
                    Name = request.Name,
                    Description = request.Description
                };
                _db.Categories.Add(category);
                return await _db.SaveChangesAsync();
            }
        }

        public async Task<int> Delete(int categoryId)
        {
            var categoryFromDb = await _db.Categories.FindAsync(categoryId);
            if (categoryFromDb == null) throw new TGProException($"Cannot find any categories with Id: {categoryId}");
            else
            {
                _db.Categories.Remove(categoryFromDb);
                return await _db.SaveChangesAsync();
            }
        }

        public async Task<Category> GetById(int categoryId)
        {
            var categoryFromDb = await _db.Categories.FindAsync(categoryId);
            if (categoryFromDb == null) throw new TGProException($"Cannot find any categories with Id: {categoryId}");
            else
            {
                return categoryFromDb;
            }
        }

        public async Task<List<Category>> GetListCategory()
        {
            List<Category> lstCategory = await _db.Categories.OrderBy(c => c.Name).ToListAsync();
            if (lstCategory == null) throw new TGProException("Cannot find any categories");
            else
            {
                return lstCategory;
            }
        }

        public async Task<int> Update(int categoryId, CategoryRequest request)
        {
            var categoryFromDb = await _db.Categories.FindAsync(categoryId);
            if (categoryFromDb == null) throw new TGProException($"Cannot find any categories with Id: {categoryId}");
            else
            {
                categoryFromDb.Name = request.Name;
                categoryFromDb.Description = request.Description;
                _db.Categories.Update(categoryFromDb);
                return await _db.SaveChangesAsync();
            }
        }
    }
}
