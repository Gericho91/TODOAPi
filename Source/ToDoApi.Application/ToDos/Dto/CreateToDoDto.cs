using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoApi.Application.ToDos.Dto
{
    public class CreateToDoDto
    {
        /// <summary>
        /// ToDo's description
        /// </summary>
        public string Description { get; set; }
        public bool? IsFinished { get; set; }
        public Nullable<DateTime> FinishedAt { get; set; }
    }
}
