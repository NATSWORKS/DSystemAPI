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

        public TaskModel? GetById(int id)
        {
            return _context.Tasks.Find(id);
        }

        public void Update(TaskModel task)
        {
            _context.Tasks.Update(task);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var task = _context.Tasks.Find(id);
            if (task != null)
            {
                _context.Tasks.Remove(task);
                _context.SaveChanges();
            }
        }
    }
}
