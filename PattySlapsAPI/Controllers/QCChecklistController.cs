using Microsoft.AspNetCore.Mvc;
using PattySlaps.Data;
using PattySlaps;

namespace PattySlaps.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QCChecklistController : ControllerBase
    {
        private readonly Repository<ReceptionQCChecklist> _qcChecklistRepository;

        public QCChecklistController(Repository<ReceptionQCChecklist> qcChecklistRepository)
        {
            _qcChecklistRepository = qcChecklistRepository;
        }

        // ✅ Get all QC checklists
        [HttpGet]
        public ActionResult<IEnumerable<ReceptionQCChecklist>> GetAll()
        {
            return Ok(_qcChecklistRepository.GetAll());
        }

        // ✅ Get a single QC checklist by ID
        [HttpGet("{id}")]
        public ActionResult<ReceptionQCChecklist> GetById(int id)
        {
            var qcChecklist = _qcChecklistRepository.GetById(id);
            if (qcChecklist == null) return NotFound();
            return Ok(qcChecklist);
        }

        // ✅ Add a new QC checklist
        [HttpPost]
        public ActionResult<ReceptionQCChecklist> Create(ReceptionQCChecklist qcChecklist)
        {
            _qcChecklistRepository.Add(qcChecklist);
            return CreatedAtAction(nameof(GetById), new { id = qcChecklist.QCID }, qcChecklist);
        }

        // ✅ Update an existing QC checklist
        [HttpPut("{id}")]
        public IActionResult Update(int id, ReceptionQCChecklist updatedQCChecklist)
        {
            var qcChecklist = _qcChecklistRepository.GetById(id);
            if (qcChecklist == null) return NotFound();

            // Update fields
            qcChecklist.Date = updatedQCChecklist.Date;
            qcChecklist.ItemID = updatedQCChecklist.ItemID;
            qcChecklist.ItemName = updatedQCChecklist.ItemName;
            qcChecklist.ItemDefect = updatedQCChecklist.ItemDefect;
            qcChecklist.Quantity = updatedQCChecklist.Quantity;
            qcChecklist.ItemPicture = updatedQCChecklist.ItemPicture;
            qcChecklist.Completed = updatedQCChecklist.Completed;

            _qcChecklistRepository.Update(qcChecklist);
            return NoContent();
        }

        // ✅ Delete a QC checklist
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var qcChecklist = _qcChecklistRepository.GetById(id);
            if (qcChecklist == null) return NotFound();

            _qcChecklistRepository.Delete(qcChecklist);
            return NoContent();
        }
    }
}

