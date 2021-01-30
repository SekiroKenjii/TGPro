using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TGPro.Service.Catalog.Demands;
using TGPro.Service.ViewModel.Demands;

namespace TGPro.BackendAPI.Controllers
{
    [Route("api/[controller]")]
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

        [HttpGet("/demands")]
        public async Task<IActionResult> GetListDemand()
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

        [HttpPost("/create_demand")]
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

        [HttpPut("/update_demand/{demandId}")]
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

        [HttpDelete("/delete_demand/{demandId}")]
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
