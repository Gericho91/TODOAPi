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

        //Én, mint felhasználó, legyek képes a rendszerben egy új TODO elem hozzáadására.Egy TODO elem szövegesen rögzít egy feladatot, és van egy logikai tulajdonsága, mely jelzi, hogy elkészült-e.
        public async Task AddToDo(CreateToDoDto toDoDto, Guid userId)
        {
            var toDo = _mapper.Map<CreateToDoDto, ToDo>(toDoDto);
            
            await _repository.InsertAsync(toDo);
        }

        //Én, mint felhasználó, legyek képes a rendszerben egy felvett TODO elemet készre jelenteni.
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

        //Én, mint felhasználó, legyek képes a rendszerben a felvett TODO elemek listázására.
        public async Task<List<ToDoDto>> GetToDos()
        {
            var toDos = await _repository.Filter(Guid.NewGuid()).ToListAsync();
            return _mapper.Map<List<ToDoDto>>(toDos);
        }
             
        //Én, mint felhasználó, legyek képes a rendszerben az elkészült / el nem készült TODO elemek listázására.
        public async Task GetToDosByFinished(bool isFinished)
        {

        }

        //Én, mint felhasználó, legyek képes a rendszerben TODO elemek törlésére.
        public async Task DeleteToDo()
        {

        }

        //Én, mint felhasználó, legyek képes a rendszerben TODO elemek szövegezésének módosítására.
        public async Task UpdateToDo()
        {

        }

        //Én, mint felhasználó, legyek képes a rendszerben a TODO elemek közti szöveges keresésre.
        public async Task FilterToDo()
        {

        }

        //Én, mint felhasználó, legyek képes a rendszerben a listázott TODO elemek közt oldalanként böngészni.Egy oldalon legfeljebb 25 elem szerepelhet.
        public async Task GetToDosByPaging()
        {

        }
    }
}
