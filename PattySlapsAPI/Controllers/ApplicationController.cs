using Microsoft.AspNetCore.Mvc;
using PattySlaps.Data;
using System.Collections.Generic;

namespace PattySlaps.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ApplicationController : ControllerBase
    {
        private readonly Repository<Application> _applicationRepository;

        public ApplicationController(Repository<Application> applicationRepository)
        {
            _applicationRepository = applicationRepository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Application>> GetAll()
        {
            return Ok(_applicationRepository.GetAll());
        }

        [HttpGet("{id}")]
        public ActionResult<Application> GetById(int id)
        {
            var application = _applicationRepository.GetById(id);
            if (application == null) return NotFound();
            return Ok(application);
        }

        [HttpPost]
        public ActionResult<Application> Create(Application application)
        {
            _applicationRepository.Add(application);
            return CreatedAtAction(nameof(GetById), new { id = application.ApplicationID }, application);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Application updatedApplication)
        {
            var application = _applicationRepository.GetById(id);
            if (application == null) return NotFound();

            application.Status = updatedApplication.Status;
            application.SubmissionDate = updatedApplication.SubmissionDate;
            _applicationRepository.Update(application);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var application = _applicationRepository.GetById(id);
            if (application == null) return NotFound();

            _applicationRepository.Delete(application);
            return NoContent();
        }
    }
}