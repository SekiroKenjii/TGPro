using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TGPro.Service.Catalog.Conditions;
using TGPro.Service.ViewModel.Conditions;

namespace TGPro.BackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConditionController : Controller
    {
        private readonly IConditionService _conditionService;
        public ConditionController(IConditionService conditionService)
        {
            _conditionService = conditionService;
        }

        [HttpGet("/condition_by_id/{conditionId}")]
        public async Task<IActionResult> GetById(int conditionId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var condition = await _conditionService.GetById(conditionId);
            if (condition == null)
                return BadRequest();
            return Ok(condition);
        }

        [HttpGet("/conditions")]
        public async Task<IActionResult> GetListCondition()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var condition = await _conditionService.GetListCondition();
            if (condition.Count == 0)
                return BadRequest();
            return Ok(condition);
        }

        [HttpPost("/create_condition")]
        public async Task<IActionResult> Create([FromBody] ConditionRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var affectedResult = await _conditionService.Create(request);
            if (affectedResult == 0)
                return BadRequest();
            return Ok();
        }

        [HttpPut("/update_condition/{conditionId}")]
        public async Task<IActionResult> Update(int conditionId, [FromBody] ConditionRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var affectedResult = await _conditionService.Update(conditionId, request);
            if (affectedResult == 0)
                return BadRequest();
            return Ok();
        }

        [HttpDelete("/delete_condition/{conditionId}")]
        public async Task<IActionResult> Delete(int conditionId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var affectedResult = await _conditionService.Delete(conditionId);
            if (affectedResult == 0)
                return BadRequest();
            return Ok();
        }
    }
}
