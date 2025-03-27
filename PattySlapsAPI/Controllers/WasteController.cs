using Microsoft.AspNetCore.Mvc;
using PattySlaps.Data;
using PattySlaps;

namespace PattySlapsAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WasteController : ControllerBase
    {
        private readonly Repository<WasteRecord> _wasteRepository;

        public WasteController(Repository<WasteRecord> wasteRepository)
        {
            _wasteRepository = wasteRepository;
        }

        // ✅ Get all waste records
        [HttpGet]
        public ActionResult<IEnumerable<WasteRecord>> GetAll()
        {
            var records = _wasteRepository.GetAll();
            return Ok(records);
        }

        // ✅ Get a single waste record by ID
        [HttpGet("{id}")]
        public ActionResult<WasteRecord> GetById(int id)
        {
            var record = _wasteRepository.GetById(id);
            if (record == null) return NotFound();
            return Ok(record);
        }

        // ✅ Add a new waste record
        [HttpPost]
        public ActionResult<WasteRecord> Create(WasteRecord record)
        {
            _wasteRepository.Add(record);
            return CreatedAtAction(nameof(GetById), new { id = record.WasteID }, record);
        }

        // ✅ Update an existing waste record
        [HttpPut("{id}")]
        public IActionResult Update(int id, WasteRecord updatedRecord)
        {
            var record = _wasteRepository.GetById(id);
            if (record == null) return NotFound();

            // Update fields
            record.WasteType = updatedRecord.WasteType;
            record.Quantity = updatedRecord.Quantity;

            _wasteRepository.Update(record);
            return NoContent();
        }

        // ✅ Delete a waste record
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var record = _wasteRepository.GetById(id);
            if (record == null) return NotFound();

            _wasteRepository.Delete(record);
            return NoContent();
        }
    }
}
