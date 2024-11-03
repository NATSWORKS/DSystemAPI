namespace DSystemAPI.Model
{
    public interface ITaskRepository
    {
        void Add(TaskModel taskModel);

        List<TaskModel> Get();
    }
}
