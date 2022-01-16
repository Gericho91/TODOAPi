using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using ToDoApi.Domain.Users;

namespace ToDoApi.Domain.ToDos
{
    public class ToDo
    {
        [Key]
        [Required]
        public Guid Id { get; set; }

        [Required]
        public string Description { get; set; }

        [DefaultValue(false)]
        public bool IsFinished { get; set; }

        public Nullable<DateTime> FinishedAt { get; set; }

        [Required]
        public Guid UserId { get; set; }

        [Required]
        public DateTime CreationTime { get; set; }

        public Nullable<DateTime> LastModifiedTime { get; set; }

        [ForeignKey("UserId")]
        public virtual AppUser User { get; set; }

    }
}
