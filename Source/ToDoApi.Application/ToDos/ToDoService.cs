using AutoMapper;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using ToDoApi.Application.ToDos.Dto;
using ToDoApi.Domain.ToDos;
using ToDoApi.EntityFrameworkCore.ToDos;

namespace ToDoApi.Application.ToDos
{
    public class ToDoService : IToDoService
    {
        private readonly IToDoRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public ToDoService(
            IToDoRepository repository,
            IMapper mapper,
            ILogger<ToDoService> logger
            )
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task AddToDoAsync(CreateToDoDto toDoDto, Guid userId)
        {
            var toDo = _mapper.Map<CreateToDoDto, ToDo>(toDoDto);
            toDo.UserId = userId;

            var result = await _repository.InsertAsync(toDo);

            if (result == false)
            {
                _logger.LogError("Couldn't insert ToDo", toDoDto);
                throw new Exception("Error on insert ToDo");
            }
        }

        public async Task ToDoFinishedAsync(Guid id)
        {
            var toDo = await _repository.GetByIdAsync(id);

            if (toDo == null)
            {
                _logger.LogWarning("ToDo not found by Id", id);
                throw new Exception("ToDo not found");
            }

            if(toDo.IsFinished == true)
            {
                _logger.LogWarning("ToDo is already finished", id);
                throw new Exception("ToDo is already finished");
            }

            toDo.IsFinished = true;
            toDo.FinishedAt = DateTime.Now;

            var result = await _repository.UpdateAsync(toDo);

            if(result == false)
            {
                _logger.LogError("Couldn't apply ToDo changes", id, toDo);
                throw new Exception("Error on finishing ToDo");
            }
        }

        public async Task DeleteToDoAsync(Guid id)
        {
            await _repository.DeleteAsync(id);
        }

        public async Task UpdateToDoAsync(UpdateToDoDto toDoDto)
        {
            var toDo = await _repository.GetByIdAsync(toDoDto.Id);
            toDo = _mapper.Map(toDoDto, toDo);
            await _repository.UpdateAsync(toDo);
        }

        public async Task<List<ToDoDto>> GetToDosAsync(Guid userId)
        {
            var toDos = await _repository.Filter(userId).ToListAsync();
            return _mapper.Map<List<ToDoDto>>(toDos);
        }

        public async Task<List<ToDoDto>> GetToDosByFinishedAsync(bool isFinished, Guid userId)
        {
            var toDos = await _repository.Filter(userId, isFinished).ToListAsync();
            return _mapper.Map<List<ToDoDto>>(toDos);
        }

        public async Task<List<ToDoDto>> FilterToDoAsync(string filterText, Guid userId)
        {
            var toDos = await _repository.Filter(userId, null, filterText).ToListAsync();
            return _mapper.Map<List<ToDoDto>>(toDos);
        }

        public async Task<List<ToDoDto>> GetToDosByPagingAsync(int page, int pageSize, Guid userId)
        {
            var query = _repository.Filter(userId);
            var toDos = await _repository.PaginateQueryAsync(query, page, pageSize);
            return _mapper.Map<List<ToDoDto>>(toDos);
        }
    }
}
