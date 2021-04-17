using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TGPro.Service.Catalog.ProductImages;
using TGPro.Service.Catalog.Products;
using TGPro.Service.DTOs.ProductImages;
using TGPro.Service.DTOs.Products;

namespace TGPro.BackendAPI.Controllers
{
    //[Authorize(Roles = ConstantStrings.AdminRole)]
    [ApiController]
    public class ProductsController : BaseApiController
    {
        private readonly IProductService _productService;
        private readonly IProductImageService _productImageService;
        public ProductsController(IProductService productService, IProductImageService productImageService)
        {
            _productService = productService;
            _productImageService = productImageService;
        }

        [HttpGet("/api/product/{productId}")]
        public async Task<IActionResult> GetById(int productId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _productService.GetById(productId);
            if (!result.IsSuccessed)
                return BadRequest(result.Message);
            return Ok(result.ResultObj);
        }

        [HttpGet("/api/product/all")]
        public async Task<IActionResult> GetAllProduct()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _productService.GetListProduct();
            if (!result.IsSuccessed)
                return BadRequest(result.Message);
            return Ok(result.ResultObj);
        }

        [HttpPost("/api/product/add")]
        public async Task<IActionResult> Create([FromBody] ProductRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _productService.Create(request);
            if (!result.IsSuccessed)
                return BadRequest(result.Message);
            return Ok();
        }

        [HttpPut("/api/product/update/{productId}")]
        public async Task<IActionResult> Update(int productId, [FromBody] ProductRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _productService.Update(productId, request);
            if (!result.IsSuccessed)
                return BadRequest(result.Message);
            return Ok();
        }

        [HttpDelete("/api/product/delete/{productId}")]
        public async Task<IActionResult> Delete(int productId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _productService.Delete(productId);
            if (!result.IsSuccessed)
                return BadRequest(result.Message);
            return Ok();
        }
        
        [HttpPut("/api/product/image/add/{productId}")]
        public async Task<IActionResult> AddProductImage(int productId, [FromForm] ProductImageRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _productImageService.Create(productId, request);
            if (!result.IsSuccessed)
                return BadRequest(result.Message);
            return Ok();
        }

        [HttpPut("/api/product/image/update/{productId}")]
        public async Task<IActionResult> UpdateProductImage(int productId, [FromForm] ProductImageRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _productImageService.Update(productId, request);
            if (!result.IsSuccessed)
                return BadRequest(result.Message);
            return Ok();
        }

        [HttpDelete("/api/product/image/delete/{productImageId}")]
        public async Task<IActionResult> DeleteProductImage(int productImageId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _productImageService.Delete(productImageId);
            if (!result.IsSuccessed)
                return BadRequest(result.Message);
            return Ok();
        }
    }
}
