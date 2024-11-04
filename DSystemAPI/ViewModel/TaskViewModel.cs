using DSystemAPI.Model;

namespace DSystemAPI.ViewModel
{
    public class TaskViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateConclusion { get; set; }
        public TaskModel.StatusE Status { get; set; }
    }
}
