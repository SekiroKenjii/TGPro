using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TGPro.Service.Catalog.Conditions;
using TGPro.Service.Common;
using TGPro.Service.DTOs.Conditions;

namespace TGPro.BackendAPI.Controllers
{
    [Authorize(Roles = ConstantStrings.AdminRole)]
    [ApiController]
    public class ConditionController : BaseApiController
    {
        private readonly IConditionService _conditionService;
        public ConditionController(IConditionService conditionService)
        {
            _conditionService = conditionService;
        }

        [HttpGet("/api/condition/{conditionId}")]
        public async Task<IActionResult> GetById(int conditionId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _conditionService.GetById(conditionId);
            if (!result.IsSuccessed)
                return BadRequest(result.Message);
            return Ok(result.ResultObj);
        }

        [HttpGet("/api/condition/all")]
        public async Task<IActionResult> GetAllCondition()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _conditionService.GetListCondition();
            if (!result.IsSuccessed)
                return BadRequest(result.Message);
            return Ok(result.ResultObj);
        }

        [HttpPost("/api/condition/add")]
        public async Task<IActionResult> Create([FromBody] ConditionRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _conditionService.Create(request);
            if (!result.IsSuccessed)
                return BadRequest(result.Message);
            return Ok();
        }

        [HttpPut("/api/condition/update/{conditionId}")]
        public async Task<IActionResult> Update(int conditionId, [FromBody] ConditionRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _conditionService.Update(conditionId, request);
            if (!result.IsSuccessed)
                return BadRequest(result.Message);
            return Ok();
        }

        [HttpDelete("/api/condition/delete/{conditionId}")]
        public async Task<IActionResult> Delete(int conditionId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _conditionService.Delete(conditionId);
            if (!result.IsSuccessed)
                return BadRequest(result.Message);
            return Ok();
        }
    }
}
