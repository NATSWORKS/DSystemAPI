using Microsoft.EntityFrameworkCore;

namespace DSystemAPI.Model
{
    public interface ITaskRepository
    {
        void Add(TaskModel taskModel);

        List<TaskModel> Get();

        TaskModel? GetById(int id);

        void Update(TaskModel taskModel);

        public void Delete(int id);
    }
}
