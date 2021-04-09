using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TGPro.Service.Catalog.Categories;
using TGPro.Service.SystemResources;
using TGPro.Service.ViewModel.Categories;

namespace TGPro.BackendAPI.Controllers
{
    //[Route("/api/category")]
    [Authorize(Roles = ConstantStrings.AdminRole)]
    [ApiController]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet("/api/category/{categoryId}")]
        public async Task<IActionResult> GetById(int categoryId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _categoryService.GetById(categoryId);
            if (!result.IsSuccessed)
                return BadRequest(result.Message);
            return Ok(result.ResultObj);
        }

        [HttpGet("/api/category/all")]
        public async Task<IActionResult> GetAllCategory()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _categoryService.GetListCategory();
            if (result.ResultObj.Count == 0)
                return BadRequest(result.Message);
            return Ok(result.ResultObj);
        }

        [HttpPost("/api/category/add")]
        public async Task<IActionResult> AddCategory([FromBody] CategoryRequest request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result = await _categoryService.Create(request);
            if (!result.IsSuccessed)
                return BadRequest(result.Message);
            return Ok();
        }

        [HttpPut("/api/category/update/{categoryId}")]
        public async Task<IActionResult> UpdateCategory(int categoryId, [FromBody] CategoryRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _categoryService.Update(categoryId, request);
            if (!result.IsSuccessed)
                return BadRequest(result.Message);
            return Ok();
        }

        [HttpDelete("/api/category/delete/{categoryId}")]
        public async Task<IActionResult> DeleteCategory(int categoryId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _categoryService.Delete(categoryId);
            if (!result.IsSuccessed)
                return BadRequest(result.Message);
            return Ok();
        }
    }
}
