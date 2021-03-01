using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TGPro.Service.Catalog.Demands;
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

        [HttpGet("/demand_by_id/{demandId}")]
        public async Task<IActionResult> GetById(int demandId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var demand = await _demandService.GetById(demandId);
            if (demand == null)
                return BadRequest();
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
                return BadRequest(demand);
            return Ok(demand);
        }

        [HttpPost("/api/demand/add")]
        public async Task<IActionResult> Create([FromBody] DemandRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var affectedResult = await _demandService.Create(request);
            if (affectedResult == 0)
                return BadRequest();
            return Ok();
        }

        [HttpPut("/api/demand/update/{demandId}")]
        public async Task<IActionResult> Update(int demandId, [FromBody] DemandRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var affectedResult = await _demandService.Update(demandId, request);
            if (affectedResult == 0)
                return BadRequest();
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
            if (affectedResult == 0)
                return BadRequest();
            return Ok();
        }
    }
}
