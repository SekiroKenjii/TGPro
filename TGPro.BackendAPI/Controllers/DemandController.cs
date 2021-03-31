using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TGPro.Service.Catalog.Demands;
using TGPro.Service.SystemResources;
using TGPro.Service.ViewModel.Demands;

namespace TGPro.BackendAPI.Controllers
{
    //[Route("/api/demand")]
    [ApiController]
    public class DemandController : Controller
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
            var demand = await _demandService.GetById(demandId);
            if (demand == null)
                return NotFound(SystemFunctions.FindByIdError("demands", demandId));
            return Ok(demand);
        }

        [HttpGet("/api/demand/all")]
        public async Task<IActionResult> GetAllDemand()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var demand = await _demandService.GetListDemand();
            if (demand.Count == 0)
                return NotFound(SystemFunctions.GetAllError("demands"));
            return Ok(demand);
        }

        [HttpPost("/api/demand/add")]
        public async Task<IActionResult> Create([FromForm] DemandRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var affectedResult = await _demandService.Create(request);
            if (affectedResult == ConstantStrings.TGBadRequest)
                return BadRequest(ConstantStrings.emptyNameFieldError);
            if (affectedResult == 0)
                return BadRequest(ConstantStrings.undefinedError);
            return Ok();
        }

        [HttpPut("/api/demand/update/{demandId}")]
        public async Task<IActionResult> Update(int demandId, [FromForm] DemandRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var affectedResult = await _demandService.Update(demandId, request);
            if (affectedResult == ConstantStrings.TGNotFound)
                return NotFound(SystemFunctions.FindByIdError("demands", demandId));
            if (affectedResult == ConstantStrings.TGBadRequest)
                return BadRequest(ConstantStrings.emptyNameFieldError);
            if (affectedResult == 0)
                return BadRequest(ConstantStrings.undefinedError);
            return Ok();
        }

        [HttpDelete("/api/demand/delete/{demandId}")]
        public async Task<IActionResult> Delete(int demandId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var affectedResult = await _demandService.Delete(demandId);
            if (affectedResult == ConstantStrings.TGNotFound)
                return NotFound(SystemFunctions.FindByIdError("demands", demandId));
            if (affectedResult == 0)
                return BadRequest(ConstantStrings.undefinedError);
            return Ok();
        }
    }
}
