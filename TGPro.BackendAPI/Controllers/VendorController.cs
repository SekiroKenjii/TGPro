﻿using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TGPro.Service.Catalog.Vendors;
using TGPro.Service.SystemResources;
using TGPro.Service.ViewModel.Vendors;

namespace TGPro.BackendAPI.Controllers
{
    //[Route("/api/vendor")]
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
            var vendor = await _vendorService.GetById(vendorId);
            if (vendor == null)
                return NotFound(SystemFunctions.FindByIdError("vendors", vendorId));
            return Ok(vendor);
        }

        [HttpGet("/api/vendor/all")]
        public async Task<IActionResult> GetAllVendor()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var vendor = await _vendorService.GetListVendor();
            if (vendor.Count == 0)
                return NotFound(SystemFunctions.GetAllError("vendors"));
            return Ok(vendor);
        }

        [HttpPost("/api/vendor/add")]
        public async Task<IActionResult> Create([FromBody] VendorRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var affectedResult = await _vendorService.Create(request);
            if (affectedResult == ConstantStrings.TGBadRequest)
                return BadRequest(ConstantStrings.emptyNameFieldError);
            if (affectedResult == 0)
                return BadRequest(ConstantStrings.undefinedError);
            return Ok();
        }

        [HttpDelete("/api/vendor/delete/{vendorId}")]
        public async Task<IActionResult> Delete(int vendorId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var affectedResult = await _vendorService.Delete(vendorId);
            if (affectedResult == ConstantStrings.TGNotFound)
                return NotFound(SystemFunctions.FindByIdError("vendors", vendorId));
            if (affectedResult == 0)
                return BadRequest(ConstantStrings.undefinedError);
            return Ok();
        }

        [HttpPut("/api/vendor/update/{vendorId}")]
        public async Task<IActionResult> Update(int vendorId, [FromBody] VendorRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var affectedResult = await _vendorService.Update(vendorId, request);
            if (affectedResult == ConstantStrings.TGNotFound)
                return NotFound(SystemFunctions.FindByIdError("vendors", vendorId));
            if (affectedResult == ConstantStrings.TGBadRequest)
                return BadRequest(ConstantStrings.emptyNameFieldError);
            if (affectedResult == 0)
                return BadRequest(ConstantStrings.undefinedError);
            return Ok();
        }
    }
}
