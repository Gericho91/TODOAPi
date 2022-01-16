using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using ToDoApi.Domain.Users;

namespace ToDoApi.Application.ToDos.Dto
{
    public class ToDoDto
    {
        public Guid Id { get; set; }

        public string Description { get; set; }

        public bool IsFinished { get; set; }

        public Nullable<DateTime> FinishedAt { get; set; }

        public Guid UserId { get; set; }

        public DateTime CreationTime { get; set; }

        public Nullable<DateTime> LastModifiedTime { get; set; }
    }
}
