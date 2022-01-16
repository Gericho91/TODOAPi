using ToDoApi.Host.Attributes;

namespace ToDoApi.Host.Models
{
    public class Pagination
    {
        public int Page { get; set; }

        [MaxPageSize]
        public int PageSize { get; set; }
    }
}
