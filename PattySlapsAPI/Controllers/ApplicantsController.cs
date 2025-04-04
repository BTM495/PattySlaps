using Microsoft.AspNetCore.Mvc;
using PattySlaps.Data;
using PattySlaps;

namespace PattySlapsAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ApplicantsController : ControllerBase
    {
        private readonly Repository<Applicant> _applicantsRepository;

        public ApplicantsController(Repository<Applicant> applicantsRepository)
        {
            _applicantsRepository = applicantsRepository;
        }

        // ? Get all applicants
        [HttpGet]
        public ActionResult<IEnumerable<Applicant>> GetAll()
        {
            var applicants = _applicantsRepository.GetAll();
            return Ok(applicants);
        }

        // ? Get a single applicant by ID
        [HttpGet("{id}")]
        public ActionResult<Applicant> GetById(int id)
        {
            var applicant = _applicantsRepository.GetById(id);
            if (applicant == null) return NotFound();
            return Ok(applicant);
        }

        // ? Add a new applicant
        [HttpPost]
        public ActionResult<Applicant> Create(Applicant applicant)
        {
            _applicantsRepository.Add(applicant);
            return CreatedAtAction(nameof(GetById), new { id = applicant.ApplicantID }, applicant);
        }

        // ? Update an existing applicant
        [HttpPut("{id}")]
        public IActionResult Update(int id, Applicant updatedApplicant)
        {
            var applicant = _applicantsRepository.GetById(id);
            if (applicant == null) return NotFound();

            // Update fields
            applicant.PersonalInfo = updatedApplicant.PersonalInfo;
            applicant.EducationLevel = updatedApplicant.EducationLevel;
            applicant.Experience = updatedApplicant.Experience;
            applicant.Availability = updatedApplicant.Availability;
            applicant.HourPreferences = updatedApplicant.HourPreferences;
            applicant.Resume = updatedApplicant.Resume;

            _applicantsRepository.Update(applicant);
            return NoContent();
        }

        // ? Delete an applicant
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var applicant = _applicantsRepository.GetById(id);
            if (applicant == null) return NotFound();

            _applicantsRepository.Delete(applicant);
            return NoContent();
        }
    }
}

