using DSystemAPI.Model;
using DSystemAPI.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

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

        /*
        ========================
        Add
        ------------------------
        Registra uma tarefa no banco de dados.
        ========================
        */
        [HttpPost]
        public IActionResult Add(TaskViewModel taskView)
        {
            var task = new TaskModel(taskView.Title, taskView.Description, taskView.DateCreated, taskView.DateConclusion, taskView.Status);
            _taskRepository.Add(task);
            return Ok();
        }

        /*
        ========================
        Get
        ------------------------
        Busca tarefas no banco de dados.
        ========================
        */
        [HttpGet]
        public IActionResult Get()
        {
            var tasks = _taskRepository.Get();
            return Ok(tasks);
        }

        /*
        ========================
        Update
        ------------------------
        Atualiza tarefas no banco de dados.
        ========================
        */
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] TaskViewModel taskView)
        {
            var task = _taskRepository.GetById(id);
            if (task == null)
            {
                return NotFound("Tarefa não encontrada.");
            }
            // Atualizar os dados da tarefa
            task.Title = taskView.Title;
            task.Description = taskView.Description;
            task.DateConclusion = taskView.DateConclusion;
            task.Status = taskView.Status;
            try
            {
                _taskRepository.Update(task);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        /*
        ========================
        Delete
        ------------------------
        Deleta tarefas do banco de dados.
        ========================
        */
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var task = _taskRepository.GetById(id);
            if (task == null)
            {
                return NotFound("Tarefa não encontrada.");
            }

            _taskRepository.Delete(id);
            return NoContent();
        }
    }
}
