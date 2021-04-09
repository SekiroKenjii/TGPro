using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TGPro.Service.Catalog.Vendors;
using TGPro.Service.SystemResources;
using TGPro.Service.ViewModel.Vendors;

namespace TGPro.BackendAPI.Controllers
{
    //[Route("/api/vendor")]
    [Authorize(Roles = ConstantStrings.AdminRole)]
    [ApiController]
    public class VendorController : Controller
    {
        private readonly IVendorService _vendorService;
        public VendorController(IVendorService vendorService)
        {
            _vendorService = vendorService;
        }

        [HttpGet("/api/vendor/{vendorId}")]
        public async Task<IActionResult> GetById(int vendorId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _vendorService.GetById(vendorId);
            if (!result.IsSuccessed)
                return BadRequest(result.Message);
            return Ok(result.ResultObj);
        }

        [HttpGet("/api/vendor/all")]
        public async Task<IActionResult> GetAllVendor()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _vendorService.GetListVendor();
            if (!result.IsSuccessed)
                return BadRequest(result.Message);
            return Ok(result.ResultObj);
        }

        [HttpPost("/api/vendor/add")]
        public async Task<IActionResult> Create([FromBody] VendorRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _vendorService.Create(request);
            if (!result.IsSuccessed)
                return BadRequest(result.Message);
            return Ok();
        }

        [HttpDelete("/api/vendor/delete/{vendorId}")]
        public async Task<IActionResult> Delete(int vendorId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _vendorService.Delete(vendorId);
            if (!result.IsSuccessed)
                return BadRequest(result.Message);
            return Ok();
        }

        [HttpPut("/api/vendor/update/{vendorId}")]
        public async Task<IActionResult> Update(int vendorId, [FromBody] VendorRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _vendorService.Update(vendorId, request);
            if (!result.IsSuccessed)
                return BadRequest(result.Message);
            return Ok();
        }
    }
}
