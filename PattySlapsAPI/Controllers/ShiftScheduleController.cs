using Microsoft.AspNetCore.Mvc;
using PattySlaps.Data;
using PattySlaps;

namespace PattySlaps.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShiftScheduleController : ControllerBase
    {
        private readonly Repository<ShiftSchedule> _shiftScheduleRepository;

        public ShiftScheduleController(Repository<ShiftSchedule> shiftScheduleRepository)
        {
            _shiftScheduleRepository = shiftScheduleRepository;
        }

        // ✅ Get all shift schedules
        [HttpGet]
        public ActionResult<IEnumerable<ShiftSchedule>> GetAll()
        {
            return Ok(_shiftScheduleRepository.GetAll());
        }

        // ✅ Get a single shift schedule by ID
        [HttpGet("{id}")]
        public ActionResult<ShiftSchedule> GetById(int id)
        {
            var shiftSchedule = _shiftScheduleRepository.GetById(id);
            if (shiftSchedule == null) return NotFound();
            return Ok(shiftSchedule);
        }

        // ✅ Add a new shift schedule
        [HttpPost]
        public ActionResult<ShiftSchedule> Create(ShiftSchedule shiftSchedule)
        {
            _shiftScheduleRepository.Add(shiftSchedule);
            return CreatedAtAction(nameof(GetById), new { id = shiftSchedule.ScheduleID }, shiftSchedule);
        }

        // ✅ Update an existing shift schedule
        [HttpPut("{id}")]
        public IActionResult Update(int id, ShiftSchedule updatedShiftSchedule)
        {
            var shiftSchedule = _shiftScheduleRepository.GetById(id);
            if (shiftSchedule == null) return NotFound();

            // Update fields
            shiftSchedule.Date = updatedShiftSchedule.Date;
            shiftSchedule.Shift = updatedShiftSchedule.Shift;
            shiftSchedule.Status = updatedShiftSchedule.Status;

            _shiftScheduleRepository.Update(shiftSchedule);
            return NoContent();
        }

        // ✅ Delete a shift schedule
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var shiftSchedule = _shiftScheduleRepository.GetById(id);
            if (shiftSchedule == null) return NotFound();

            _shiftScheduleRepository.Delete(shiftSchedule);
            return NoContent();
        }
    }
}
