using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using backend.Models;
using backend.Repositories;
using Task = backend.Models.Task;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("_myAllowSpecificOrigins")]
    public class TaskController : Controller
    {
        private IConfiguration _configuration;
        private readonly IRepository<Task> _taskRepository;
        public TaskController(IConfiguration configuration, IRepository<Task> taskRepository)
        {
            _configuration = configuration;
            _taskRepository = taskRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            IEnumerable<Task> tasks = _taskRepository.GetAll();
            return Ok(tasks);
        }

        [HttpGet("{taskID}")]
        public IActionResult Get(int taskID)
        {
            var task = _taskRepository.GetById(taskID);
            if (task == null)
            {
                return NotFound();
            }
            return Ok(task);
        }

        [HttpPost]
        public IActionResult Post(Task newTaks)
        {
            bool added = _taskRepository.Add(newTaks);
            if (!added)
            {
                return BadRequest("Failed to add task");
            }

            return Ok();
        }

        [HttpPut("{taskID}")]
        public IActionResult Put(Task updatedTask)
        {
            bool updated = _taskRepository.Update(updatedTask);
            if (updated)
            {
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

        [HttpDelete("{taskID}")]
        public IActionResult Delete(int taskID)
        {
            bool deleted = _taskRepository.Delete(taskID);
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
