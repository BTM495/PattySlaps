using Microsoft.AspNetCore.Mvc;
using PattySlaps.Data;
using System.Collections.Generic;

namespace PattySlaps.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InventoryController : ControllerBase
    {
        private readonly Repository<InventoryRecord> _inventoryRepository;

        public InventoryController(Repository<InventoryRecord> inventoryRepository)
        {
            _inventoryRepository = inventoryRepository;
        }

        // ✅ Get all inventory records
        [HttpGet]
        public ActionResult<IEnumerable<InventoryRecord>> GetAll()
        {
            var records = _inventoryRepository.GetAll();
            return Ok(records);
        }

        // ✅ Get a single inventory record by ID
        [HttpGet("{id}")]
        public ActionResult<InventoryRecord> GetById(int id)
        {
            var record = _inventoryRepository.GetById(id);
            if (record == null) return NotFound();
            return Ok(record);
        }

        // ✅ Add a new inventory record
        [HttpPost]
        public ActionResult<InventoryRecord> Create(InventoryRecord record)
        {
            _inventoryRepository.Add(record);
            return CreatedAtAction(nameof(GetById), new { id = record.RecordID }, record);
        }

        // ✅ Update an existing inventory record
        [HttpPut("{id}")]
        public IActionResult Update(int id, InventoryRecord updatedRecord)
        {
            var record = _inventoryRepository.GetById(id);
            if (record == null) return NotFound();

            // Update fields
            record.Date = updatedRecord.Date;
            record.Time = updatedRecord.Time;
            record.SoDQuantity = updatedRecord.SoDQuantity;
            record.EoDQuantity = updatedRecord.EoDQuantity;
            record.QuantityUsed = updatedRecord.QuantityUsed;
            record.DiscrepancyFlag = updatedRecord.DiscrepancyFlag;

            _inventoryRepository.Update(record);
            return NoContent();
        }

        // ✅ Delete an inventory record
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var record = _inventoryRepository.GetById(id);
            if (record == null) return NotFound();

            _inventoryRepository.Delete(record);
            return NoContent();
        }
    }
}
