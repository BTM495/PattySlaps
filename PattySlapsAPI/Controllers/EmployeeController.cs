using Microsoft.AspNetCore.Mvc;
using PattySlaps.Data;
using System.Collections.Generic;

namespace PattySlaps.Controllers
{
    [ApiController]
    [Route("api/Employee")]
    public class EmployeeController : ControllerBase
    {
        private readonly Repository<Employee> _employeeRepository;

        public EmployeeController(Repository<Employee> employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Employee>> GetAll()
        {
            return Ok(_employeeRepository.GetAll());
        }

        [HttpGet("{id}")]
        public ActionResult<Employee> GetById(int id)
        {
            var employee = _employeeRepository.GetById(id);
            if (employee == null) return NotFound();
            return Ok(employee);
        }

        [HttpPost]
        public ActionResult<Employee> Create(Employee employee)
        {
            _employeeRepository.Add(employee);
            return CreatedAtAction(nameof(GetById), new { id = employee.EmployeeID }, employee);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Employee updatedEmployee)
        {
            var employee = _employeeRepository.GetById(id);
            if (employee == null) return NotFound();

            employee.FirstName = updatedEmployee.FirstName;
            employee.LastName = updatedEmployee.LastName;
            employee.Role = updatedEmployee.Role;
            employee.Wage = updatedEmployee.Wage;
            _employeeRepository.Update(employee);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var employee = _employeeRepository.GetById(id);
            if (employee == null) return NotFound();

            _employeeRepository.Delete(employee);
            return NoContent();
        }
    }
}