﻿using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TGPro.Service.Catalog.Trademarks;
using TGPro.Service.ViewModel.Trademarks;

namespace TGPro.BackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrademarkController : Controller
    {
        private readonly ITrademarkService _trademarkService;
        public TrademarkController(ITrademarkService trademarkService)
        {
            _trademarkService = trademarkService;
        }

        [HttpGet("/trademark_by_id/{trademarkId}")]
        public async Task<IActionResult> GetById(int trademarkId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var trademark = await _trademarkService.GetById(trademarkId);
            if (trademark == null)
                return BadRequest();
            return Ok(trademark);
        }

        [HttpGet("/trademarks")]
        public async Task<IActionResult> GetListTrademark()
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var trademark = await _trademarkService.GetListTrademark();
            if (trademark.Count == 0)
                return BadRequest();
            return Ok(trademark);
        }

        [HttpPost("/create_trademark")]
        public async Task<IActionResult> Create([FromForm] TrademarkRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var affectedResult = await _trademarkService.Create(request);
            if (affectedResult == 0)
                return BadRequest();
            return Ok();
        }

        [HttpPut("/update_trademark/{trademarkId}")]
        public async Task<IActionResult> Update(int trademarkId, [FromForm] TrademarkRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var affectedResult = await _trademarkService.Update(trademarkId, request);
            if (affectedResult == 0)
                return BadRequest();
            return Ok();
        }

        [HttpDelete("/delete_trademark/{trademarkId}")]
        public async Task<IActionResult> DeleteTrademark(int trademarkId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var affectedResult = await _trademarkService.Delete(trademarkId);
            if (affectedResult == 0)
                return BadRequest();
            return Ok();
        }
    }
}