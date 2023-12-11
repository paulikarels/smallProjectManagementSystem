using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using backend.Models;
using backend.Repositories;


namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("_myAllowSpecificOrigins")]
    public class ProjectController : Controller
    {
        private IConfiguration _configuration;
        private readonly IRepository<Project> _projectRepository;
        public ProjectController(IConfiguration configuration, IRepository<Project> projectRepository)
        {
            _configuration = configuration;
            _projectRepository = projectRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            IEnumerable<Project> projects = _projectRepository.GetAll();
            return Ok(projects);
        }

        [HttpGet("{projectID}")]
        public IActionResult Get(int projectID)
        {
            var project = _projectRepository.GetById(projectID);
            if (project == null)
            {
                return NotFound();
            }
            return Ok(project);
        }

        [HttpPost]
        public IActionResult Post(Project newProject)
        {
            bool added = _projectRepository.Add(newProject);
            if (!added)
            {
                return BadRequest("Failed to add project");
            }

            return Ok();
        }

        [HttpPut("{projectID}")]
        public IActionResult Put(Project updatedProject)
        {
            bool updated = _projectRepository.Update(updatedProject);
            if (updated)
            {
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

        [HttpDelete("{projectID}")]
        public IActionResult Delete(int projectID)
        {
            bool deleted = _projectRepository.Delete(projectID);
            if (deleted)
            {
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }



    }
    
}
