using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DSystemAPI.Model
{
    [Table("Tasks")]
    public class TaskModel
    {
        [Key]
        public int Id { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public DateTime DateCreated { get; private set; }
        public DateTime? DateConclusion { get; private set; }
        public StatusE Status { get; private set; }

        public enum StatusE
        {
            Pendente,
            EmProgresso,
            Concluida
        }

        public TaskModel(string title, string description, DateTime dateCreated, DateTime? dateConclusion, StatusE status)
        {
            this.Title = title ?? throw new ArgumentNullException(nameof(title));
            this.Description = description;
            this.DateCreated = dateCreated;
            this.DateConclusion = dateConclusion;
            this.Status = status;
        }
    }
}
