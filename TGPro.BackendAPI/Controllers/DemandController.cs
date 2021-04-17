using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TGPro.Service.Catalog.Demands;
using TGPro.Service.Common;
using TGPro.Service.DTOs.Demands;

namespace TGPro.BackendAPI.Controllers
{
    [Authorize(Roles = ConstantStrings.AdminRole)]
    [ApiController]
    public class DemandController : BaseApiController
    {
        private readonly IDemandService _demandService;
        public DemandController(IDemandService demandService)
        {
            _demandService = demandService;
        }

        [HttpGet("/api/demand/{demandId}")]
        public async Task<IActionResult> GetById(int demandId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _demandService.GetById(demandId);
            if (!result.IsSuccessed)
                return BadRequest(result.Message);
            return Ok(result.ResultObj);
        }

        [HttpGet("/api/demand/all")]
        public async Task<IActionResult> GetAllDemand()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _demandService.GetListDemand();
            if (!result.IsSuccessed)
                return BadRequest(result.Message);
            return Ok(result.ResultObj);
        }

        [HttpPost("/api/demand/add")]
        public async Task<IActionResult> Create([FromBody] DemandRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _demandService.Create(request);
            if (!result.IsSuccessed)
                return BadRequest(result.Message);
            return Ok();
        }

        [HttpPut("/api/demand/update/{demandId}")]
        public async Task<IActionResult> Update(int demandId, [FromBody] DemandRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _demandService.Update(demandId, request);
            if (!result.IsSuccessed)
                return BadRequest(result.Message);
            return Ok();
        }

        [HttpDelete("/api/demand/delete/{demandId}")]
        public async Task<IActionResult> Delete(int demandId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _demandService.Delete(demandId);
            if (!result.IsSuccessed)
                return BadRequest(result.Message);
            return Ok();
        }
    }
}
