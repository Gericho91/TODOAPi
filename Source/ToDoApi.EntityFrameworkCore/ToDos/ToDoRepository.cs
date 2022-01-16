using Microsoft.EntityFrameworkCore;

using ToDoApi.Domain.ToDos;

using TODOApi.EntityFrameworkCore;

namespace ToDoApi.EntityFrameworkCore.ToDos
{
    public class ToDoRepository: IToDoRepository
    {
        private readonly ToDoDbContext _context;

        public ToDoRepository(
            ToDoDbContext context
            )
        {
            _context = context;
        }

        public async Task<bool> InsertAsync(ToDo toDo)
        {
            await _context.ToDos.AddAsync(toDo);
            var updatedEntities = await _context.SaveChangesAsync();
            return updatedEntities > 0;
        }

        public async Task<bool> UpdateAsync(ToDo toDo)
        {
            _context.ToDos.Update(toDo);
            var updatedEntities = await _context.SaveChangesAsync();
            return updatedEntities > 0;
        }

        public async Task<ToDo> GetByIdAsync(Guid id)
        {
            return await _context.ToDos.FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var toDo = await GetByIdAsync(id);

            _context.ToDos.Remove(toDo);

            var updatedEntities = await _context.SaveChangesAsync();

            return updatedEntities > 0;
        }

        public async Task<List<ToDo>> GetByUserIdAsync(Guid userId)
        {
            return await (from t in _context.ToDos
                    where t.UserId == userId
                    select t).ToListAsync();
        }

        public IQueryable<ToDo> Filter(Guid userId, bool? isFinished = null, string? filterText = null) =>
            (from t in _context.ToDos
             where t.UserId == userId &&
             (isFinished == null || t.IsFinished == isFinished) &&
             (filterText == null || t.Description.ToLower().Contains(filterText.ToLower()))
             select t);
                     
        public async Task<List<ToDo>> PaginateQueryAsync(IQueryable<ToDo> query, int page, int pageSize)
        {
            return await query.Skip(page*pageSize).Take(pageSize).ToListAsync();
        }

    }
}
