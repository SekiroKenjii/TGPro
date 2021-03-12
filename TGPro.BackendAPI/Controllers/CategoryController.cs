using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TGPro.Service.Catalog.Categories;
using TGPro.Service.ViewModel.Categories;

namespace TGPro.BackendAPI.Controllers
{
    //[Route("/api/category")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet("/category_by_id/{categoryId}")]
        public async Task<IActionResult> GetById(int categoryId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var category = await _categoryService.GetById(categoryId);
            if (category == null)
                return BadRequest();
            return Ok(category);
        }

        [HttpGet("/api/category/all")]
        public async Task<IActionResult> GetAllCategory()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var category = await _categoryService.GetListCategory();
            if (category.Count == 0)
                return BadRequest();
            return Ok(category);
        }

        [HttpPost("/api/category/add")]
        public async Task<IActionResult> AddCategory([FromBody] CategoryRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var affectedResult = await _categoryService.Create(request);
            if (affectedResult == 0)
                return BadRequest();
            return Ok();
        }

        [HttpPut("/api/category/update/{categoryId}")]
        public async Task<IActionResult> UpdateCategory(int categoryId, [FromBody] CategoryRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var affectedResult = await _categoryService.Update(categoryId, request);
            if (affectedResult == 0)
                return BadRequest();
            return Ok();
        }

        [HttpDelete("/api/category/delete/{categoryId}")]
        public async Task<IActionResult> DeleteCategory(int categoryId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var affectedResult = await _categoryService.Delete(categoryId);
            if (affectedResult == 0)
                return BadRequest();
            return Ok();
        }
    }
}
