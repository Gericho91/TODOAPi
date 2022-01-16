using ToDoApi.Application.ToDos.Dto;

namespace ToDoApi.Application.ToDos
{
    public interface IToDoService
    {
        Task<List<ToDoDto>> GetToDos();
    }
}
