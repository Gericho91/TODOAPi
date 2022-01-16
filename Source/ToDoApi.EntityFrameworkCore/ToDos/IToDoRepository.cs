using ToDoApi.Domain.ToDos;

namespace ToDoApi.EntityFrameworkCore.ToDos
{
    public interface IToDoRepository
    {
        Task<bool> InsertAsync(ToDo toDo);
        Task<bool> Update(ToDo toDo);
        Task<ToDo> GetById(Guid id);
        Task<bool> Delete(Guid id);
        Task<List<ToDo>> GetByUserId(Guid userId);
        IQueryable<ToDo> Filter(Guid userId, bool? isFinished = null, string? description = null);
        Task<List<ToDo>> PaginateQuery(IQueryable<ToDo> query, int page, int pageSize);
    }
}
