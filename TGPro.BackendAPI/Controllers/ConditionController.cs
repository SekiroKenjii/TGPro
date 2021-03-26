using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TGPro.Service.Catalog.Conditions;
using TGPro.Service.SystemResources;
using TGPro.Service.ViewModel.Conditions;

namespace TGPro.BackendAPI.Controllers
{
    //[Route("/api/condition")]
    [ApiController]
    public class ConditionController : Controller
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
            var condition = await _conditionService.GetById(conditionId);
            if (condition == null)
                return NotFound(SystemFunctions.FindByIdError("condition", conditionId));
            return Ok(condition);
        }

        [HttpGet("/api/condition/all")]
        public async Task<IActionResult> GetAllCondition()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var condition = await _conditionService.GetListCondition();
            if (condition.Count == 0)
                return NotFound(SystemFunctions.GetAllError("condition"));
            return Ok(condition);
        }

        [HttpPost("/api/condition/add")]
        public async Task<IActionResult> Create([FromForm] ConditionRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var affectedResult = await _conditionService.Create(request);
            if (affectedResult == ConstantStrings.TGBadRequest)
                return BadRequest(ConstantStrings.emptyNameFieldError);
            if (affectedResult == 0)
                return BadRequest(ConstantStrings.undefinedError);
            return Ok();
        }

        [HttpPut("/api/condition/update/{conditionId}")]
        public async Task<IActionResult> Update(int conditionId, [FromForm] ConditionRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var affectedResult = await _conditionService.Update(conditionId, request);
            if (affectedResult == ConstantStrings.TGNotFound)
                return NotFound(SystemFunctions.FindByIdError("condition", conditionId));
            if (affectedResult == ConstantStrings.TGBadRequest)
                return BadRequest(ConstantStrings.emptyNameFieldError);
            if (affectedResult == 0)
                return BadRequest(ConstantStrings.undefinedError);
            return Ok();
        }

        [HttpDelete("/api/condition/delete/{conditionId}")]
        public async Task<IActionResult> Delete(int conditionId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var affectedResult = await _conditionService.Delete(conditionId);
            if (affectedResult == ConstantStrings.TGNotFound)
                return NotFound(SystemFunctions.FindByIdError("condition", conditionId));
            if (affectedResult == 0)
                return BadRequest(ConstantStrings.undefinedError);
            return Ok();
        }
    }
}
