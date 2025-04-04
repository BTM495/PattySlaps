using Microsoft.AspNetCore.Mvc;
using PattySlaps.Data;
using PattySlaps;

namespace PattySlaps.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShiftScheduleEmployeeController : ControllerBase
    {
        private readonly Repository<ShiftScheduleEmployee> _shiftScheduleEmployeeRepository;

        public ShiftScheduleEmployeeController(Repository<ShiftScheduleEmployee> shiftScheduleEmployeeRepository)
        {
            _shiftScheduleEmployeeRepository = shiftScheduleEmployeeRepository;
        }

        // ✅ Get all shift schedule employees
        [HttpGet]
        public ActionResult<IEnumerable<ShiftScheduleEmployee>> GetAll()
        {
            return Ok(_shiftScheduleEmployeeRepository.GetAll());
        }

        // ✅ Get a single shift schedule employee by ID
        [HttpGet("{id}")]
        public ActionResult<ShiftScheduleEmployee> GetById(int id)
        {
            var shiftScheduleEmployee = _shiftScheduleEmployeeRepository.GetById(id);
            if (shiftScheduleEmployee == null) return NotFound();
            return Ok(shiftScheduleEmployee);
        }

        // ✅ Add a new shift schedule employee
        [HttpPost]
        public ActionResult<ShiftScheduleEmployee> Create(ShiftScheduleEmployee shiftScheduleEmployee)
        {
            _shiftScheduleEmployeeRepository.Add(shiftScheduleEmployee);
            return CreatedAtAction(nameof(GetById), new { id = shiftScheduleEmployee.ShiftScheduleEmployeeID }, shiftScheduleEmployee);
        }

        // ✅ Update an existing shift schedule employee
        [HttpPut("{id}")]
        public IActionResult Update(int id, ShiftScheduleEmployee updatedShiftScheduleEmployee)
        {
            var shiftScheduleEmployee = _shiftScheduleEmployeeRepository.GetById(id);
            if (shiftScheduleEmployee == null) return NotFound();

            // Update fields
            shiftScheduleEmployee.ScheduleID = updatedShiftScheduleEmployee.ScheduleID;
            shiftScheduleEmployee.EmployeeID = updatedShiftScheduleEmployee.EmployeeID;

            _shiftScheduleEmployeeRepository.Update(shiftScheduleEmployee);
            return NoContent();
        }

        // ✅ Delete a shift schedule employee
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var shiftScheduleEmployee = _shiftScheduleEmployeeRepository.GetById(id);
            if (shiftScheduleEmployee == null) return NotFound();

            _shiftScheduleEmployeeRepository.Delete(shiftScheduleEmployee);
            return NoContent();
        }
    }
}
