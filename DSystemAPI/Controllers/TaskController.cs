using DSystemAPI.Model;
using DSystemAPI.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace DSystemAPI.Controllers
{
    [ApiController]
    [Route("api/v1/task")]
    public class TaskController : ControllerBase
    {
        private ITaskRepository _taskRepository;

        public TaskController(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository ?? throw new ArgumentNullException(nameof(taskRepository));
        }

        [HttpPost]
        public IActionResult Add(TaskViewModel taskView)
        {
            var task = new TaskModel(taskView.Title, taskView.Description, taskView.DateCreated, taskView.DateConclusion, taskView.Status);
            _taskRepository.Add(task);
            return Ok();
        }

        [HttpGet]
        public IActionResult Gat()
        {
            var tasks = _taskRepository.Get();
            return Ok(tasks);
        }
    }
}
