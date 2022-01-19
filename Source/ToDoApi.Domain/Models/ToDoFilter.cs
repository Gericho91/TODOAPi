using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoApi.Domain.Models
{
    public class ToDoFilter
    {
        public Nullable<bool> IsFinished { get; set; }  
        public string? FilterText { get; set; }
        public Nullable<int> PageNumber { get; set; }
        public Nullable<int> PageSize { get; set; }
    }
}
