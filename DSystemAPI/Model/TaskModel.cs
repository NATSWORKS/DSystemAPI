using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DSystemAPI.Model
{
    [Table("Tasks")]
    public class TaskModel
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateConclusion { get; set; }
        public StatusE Status { get; set; }

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
