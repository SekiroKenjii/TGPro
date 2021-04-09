using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TGPro.Service.Catalog.Trademarks;
using TGPro.Service.Common;
using TGPro.Service.DTOs.Trademarks;

namespace TGPro.BackendAPI.Controllers
{
    //[Route("/api/trademark")]
    [Authorize(Roles = ConstantStrings.AdminRole)]
    [ApiController]
    public class TrademarkController : Controller
    {
        private readonly ITrademarkService _trademarkService;
        public TrademarkController(ITrademarkService trademarkService)
        {
            _trademarkService = trademarkService;
        }

        [HttpGet("/api/trademark/{trademarkId}")]
        public async Task<IActionResult> GetById(int trademarkId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _trademarkService.GetById(trademarkId);
            if (!result.IsSuccessed)
                return BadRequest(result.Message);
            return Ok(result.ResultObj);
        }

        [HttpGet("/api/trademark/all")]
        public async Task<IActionResult> GetAllTrademark()
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _trademarkService.GetListTrademark();
            if (!result.IsSuccessed)
                return BadRequest(result.Message);
            return Ok(result.ResultObj);
        }

        [HttpPost("/api/trademark/add")]
        public async Task<IActionResult> Create([FromForm] TrademarkRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _trademarkService.Create(request);
            if (!result.IsSuccessed)
                return BadRequest(result.Message);
            return Ok();
        }

        [HttpPut("/api/trademark/update/{trademarkId}")]
        public async Task<IActionResult> Update(int trademarkId, [FromForm] TrademarkRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _trademarkService.Update(trademarkId, request);
            if (!result.IsSuccessed)
                return BadRequest(result.Message);
            return Ok();
        }

        [HttpDelete("/api/trademark/delete/{trademarkId}")]
        public async Task<IActionResult> DeleteTrademark(int trademarkId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _trademarkService.Delete(trademarkId);
            if (!result.IsSuccessed)
                return BadRequest(result.Message);
            return Ok();
        }
    }
}
