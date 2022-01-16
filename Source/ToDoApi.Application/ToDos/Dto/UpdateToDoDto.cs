using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoApi.Application.ToDos.Dto
{
    public class UpdateToDoDto
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
    }
}
