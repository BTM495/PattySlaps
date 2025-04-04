using Microsoft.AspNetCore.Mvc;
using PattySlaps.Data;
using PattySlaps;

namespace PattySlapsAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HireRequestsController : ControllerBase
    {
        private readonly Repository<HireRequest> _hireRequestsRepository;

        public HireRequestsController(Repository<HireRequest> hireRequestsRepository)
        {
            _hireRequestsRepository = hireRequestsRepository;
        }

        // ? Get all hire requests
        [HttpGet]
        public ActionResult<IEnumerable<HireRequest>> GetAll()
        {
            var hireRequests = _hireRequestsRepository.GetAll();
            return Ok(hireRequests);
        }

        // ? Get a single hire request by ID
        [HttpGet("{id}")]
        public ActionResult<HireRequest> GetById(int id)
        {
            var hireRequest = _hireRequestsRepository.GetById(id);
            if (hireRequest == null) return NotFound();
            return Ok(hireRequest);
        }

        // ? Add a new hire request
        [HttpPost]
        public ActionResult<HireRequest> Create(HireRequest hireRequest)
        {
            _hireRequestsRepository.Add(hireRequest);
            return CreatedAtAction(nameof(GetById), new { id = hireRequest.RequestID }, hireRequest);
        }

        // ? Update an existing hire request
        [HttpPut("{id}")]
        public IActionResult Update(int id, HireRequest updatedHireRequest)
        {
            var hireRequest = _hireRequestsRepository.GetById(id);
            if (hireRequest == null) return NotFound();

            // Update fields
            hireRequest.Date = updatedHireRequest.Date;
            hireRequest.Position = updatedHireRequest.Position;
            hireRequest.Status = updatedHireRequest.Status;
            hireRequest.StartingDate = updatedHireRequest.StartingDate;
            hireRequest.RequestingManager = updatedHireRequest.RequestingManager;

            _hireRequestsRepository.Update(hireRequest);
            return NoContent();
        }

        // ? Delete a hire request
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var hireRequest = _hireRequestsRepository.GetById(id);
            if (hireRequest == null) return NotFound();

            _hireRequestsRepository.Delete(hireRequest);
            return NoContent();
        }
    }
}
