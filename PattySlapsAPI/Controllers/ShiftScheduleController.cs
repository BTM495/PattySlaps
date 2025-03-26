using Microsoft.AspNetCore.Mvc;
using PattySlaps.Data;
using System.Collections.Generic;

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

        [HttpGet]
        public ActionResult<IEnumerable<ShiftSchedule>> GetAll()
        {
            return Ok(_shiftScheduleRepository.GetAll());
        }

        [HttpGet("{id}")]
        public ActionResult<ShiftSchedule> GetById(int id)
        {
            var shiftSchedule = _shiftScheduleRepository.GetById(id);
            if (shiftSchedule == null) return NotFound();
            return Ok(shiftSchedule);
        }

        [HttpPost]
        public ActionResult<ShiftSchedule> Create(ShiftSchedule shiftSchedule)
        {
            _shiftScheduleRepository.Add(shiftSchedule);
            return CreatedAtAction(nameof(GetById), new { id = shiftSchedule.ScheduleID }, shiftSchedule);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, ShiftSchedule updatedShiftSchedule)
        {
            var shiftSchedule = _shiftScheduleRepository.GetById(id);
            if (shiftSchedule == null) return NotFound();

            shiftSchedule.Date = updatedShiftSchedule.Date;
            shiftSchedule.Shift = updatedShiftSchedule.Shift;
            shiftSchedule.Status = updatedShiftSchedule.Status;
            shiftSchedule.ConflictAlerts = updatedShiftSchedule.ConflictAlerts;
            _shiftScheduleRepository.Update(shiftSchedule);

            return NoContent();
        }

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