using ToDoApi.Domain.ToDos;

namespace ToDoApi.EntityFrameworkCore.ToDos
{
    public interface IToDoRepository
    {
        /// <summary>
        /// Add ToDo
        /// </summary>
        /// <param name="toDo">User's ToDo</param>
        /// <returns>Insert was success</returns>
        Task<bool> InsertAsync(ToDo toDo);

        /// <summary>
        /// Update ToDo
        /// </summary>
        /// <param name="toDo">User's ToDo</param>
        /// <returns>Update was sucess</returns>
        Task<bool> UpdateAsync(ToDo toDo);

        /// <summary>
        /// Get ToDo
        /// </summary>
        /// <param name="id">ToDo unique identifier</param>
        /// <returns>User's ToDo</returns>
        Task<ToDo> GetByIdAsync(Guid id);

        /// <summary>
        /// Remove ToDo
        /// </summary>
        /// <param name="id">ToDo unique identifier</param>
        /// <returns></returns>
        Task<bool> DeleteAsync(Guid id);

        /// <summary>
        /// Get all user's ToDo
        /// </summary>
        /// <param name="userId">User unique identifier</param>
        /// <returns>User's ToDos</returns>
        Task<List<ToDo>> GetByUserIdAsync(Guid userId);

        /// <summary>
        /// Filter ToDos
        /// </summary>
        /// <param name="userId">User unique identifier</param>
        /// <param name="isFinished">ToDo is finished</param>
        /// <param name="filterText">ToDo description</param>
        /// <returns>Filtered ToDos</returns>
        IQueryable<ToDo> Filter(Guid userId, bool? isFinished = null, string? filterText = null);

        /// <summary>
        /// Paginate ToDos
        /// </summary>
        /// <param name="query">ToDo list</param>
        /// <param name="page">Page number</param>
        /// <param name="pageSize">Page size</param>
        /// <returns>Paginated ToDos</returns>
        Task<List<ToDo>> PaginateQueryAsync(IQueryable<ToDo> query, int page, int pageSize);
    }
}
