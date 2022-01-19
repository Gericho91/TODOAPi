using ToDoApi.Application.ToDos.Dto;
using ToDoApi.Domain.Models;

namespace ToDoApi.Application.ToDos
{
    public interface IToDoService
    {
        /// <summary>
        /// Add ToDo
        /// </summary>
        /// <param name="toDoDto">Basic information of ToDo</param>
        /// <param name="userId">User unique identifier</param>
        Task AddToDoAsync(CreateToDoDto toDoDto, Guid userId);

        /// <summary>
        /// Set ToDo as finished
        /// </summary>
        /// <param name="id">ToDo unique identifier</param>
        Task ToDoFinishedAsync(Guid id);

        /// <summary>
        /// Get user's ToDos
        /// </summary>
        /// <param name="userId">User unique identifier</param>
        Task<List<ToDoDto>> GetToDosAsync(Guid userId);

        /// <summary>
        /// Get user's ToDos which finished or not
        /// </summary>
        /// <param name="isFinished">ToDo is finished</param>
        /// <param name="userId">User unique identifier</param>
        /// <returns>List of ToDo</returns>
        Task<List<ToDoDto>> GetToDosByStateAsync(bool isFinished, Guid userId);
        
        /// <summary>
        /// Delete ToDo
        /// </summary>
        /// <param name="id">ToDo unique identifier</param>
        Task DeleteToDoAsync(Guid id);

        /// <summary>
        /// Get ToDo
        /// </summary>
        /// <param name="id">ToDo unique identifier</param>
        /// <returns>ToDo</returns>
        Task<ToDoDto> GetToDoByIdAsync(Guid id);

        /// <summary>
        /// Update ToDo
        /// </summary>
        /// <param name="toDoDto">ToDo</param>
        Task UpdateToDoAsync(UpdateToDoDto toDoDto);

        /// <summary>
        /// Filter ToDo's description
        /// </summary>
        /// <param name="filterText">Filter text</param>
        /// <param name="userId">User unique identifier</param>
        /// <returns>List of ToDo</returns>
        Task<List<ToDoDto>> FilterToDoAsync(string filterText, Guid userId);

        /// <summary>
        /// User paginated ToDos
        /// </summary>
        /// <param name="page">Page number</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="userId">User unique identifier</param>
        /// <returns>List of ToDo and count of Todo</returns>
        Task<PaginationResult<ToDoDto>> GetToDosByPagingAsync(int page, int pageSize, Guid userId);
    }
}
