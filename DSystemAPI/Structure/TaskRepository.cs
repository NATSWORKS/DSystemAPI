using DSystemAPI.Model;

namespace DSystemAPI.Structure
{
    public class TaskRepository : ITaskRepository
    {
        public readonly ConnectionContext _context = new ConnectionContext();

        public void Add(TaskModel taskModel)
        {
            _context.Tasks.Add(taskModel);
            _context.SaveChanges();
        }

        public List<TaskModel> Get()
        {
            return _context.Tasks.ToList();
        }
    }
}
