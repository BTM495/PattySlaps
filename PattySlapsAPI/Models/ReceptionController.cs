using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PattySlaps.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PattySlaps.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReceptionController : ControllerBase
    {
        private readonly Repository<ReceptionQCChecklist> _qcChecklistRepository;

        public ReceptionController(Repository<ReceptionQCChecklist> qcChecklistRepository)
        {
            _qcChecklistRepository = qcChecklistRepository;
        }

        // ✅ Get all reception QC checklists
        [HttpGet]
        public ActionResult<IEnumerable<ReceptionQCChecklist>> GetAll()
        {
            var checklists = _qcChecklistRepository.GetAll();
            return Ok(checklists);
        }

        // ✅ Get a single reception QC checklist by ID
        [HttpGet("{id}")]
        public ActionResult<ReceptionQCChecklist> GetById(int id)
        {
            var checklist = _qcChecklistRepository.GetById(id);
            if (checklist == null) return NotFound();
            return Ok(checklist);
        }

        // ✅ Add a new reception QC checklist
        [HttpPost]
        public ActionResult<ReceptionQCChecklist> Create(ReceptionQCChecklist checklist)
        {
            _qcChecklistRepository.Add(checklist);
            return CreatedAtAction(nameof(GetById), new { id = checklist.QCID }, checklist);
        }

        // ✅ Update an existing reception QC checklist
        [HttpPut("{id}")]
        public IActionResult Update(int id, ReceptionQCChecklist updatedChecklist)
        {
            var checklist = _qcChecklistRepository.GetById(id);
            if (checklist == null) return NotFound();

            // Update fields
            checklist.Date = updatedChecklist.Date;
            checklist.ItemID = updatedChecklist.ItemID;
            checklist.ItemName = updatedChecklist.ItemName;
            checklist.ItemDefect = updatedChecklist.ItemDefect;
            checklist.Quantity = updatedChecklist.Quantity;
            checklist.ItemPicture = updatedChecklist.ItemPicture;
            checklist.Completed = updatedChecklist.Completed;

            _qcChecklistRepository.Update(checklist);
            return NoContent();
        }

        // ✅ Delete a reception QC checklist
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var checklist = _qcChecklistRepository.GetById(id);
            if (checklist == null) return NotFound();

            _qcChecklistRepository.Delete(checklist);
            return NoContent();
        }
    }
}
