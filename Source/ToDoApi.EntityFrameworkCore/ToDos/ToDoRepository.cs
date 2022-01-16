using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        /// <summary>
        /// Add ToDo
        /// </summary>
        /// <param name="toDo">User's ToDo</param>
        /// <returns>Insert was success</returns>
        public async Task<bool> InsertAsync(ToDo toDo)
        {
            await _context.ToDos.AddAsync(toDo);
            var updatedEntities = await _context.SaveChangesAsync();
            return updatedEntities > 0;
        }

        /// <summary>
        /// Update ToDo
        /// </summary>
        /// <param name="toDo">User's ToDo</param>
        /// <returns>Update was sucess</returns>
        public async Task<bool> Update(ToDo toDo)
        {
            _context.ToDos.Update(toDo);
            var updatedEntities = await _context.SaveChangesAsync();
            return updatedEntities > 0;
        }

        /// <summary>
        /// Get ToDo
        /// </summary>
        /// <param name="id">ToDo unique identifier</param>
        /// <returns>User's ToDo</returns>
        public async Task<ToDo> GetById(Guid id)
        {
            return await _context.ToDos.FirstOrDefaultAsync(t => t.Id == id);
        }

        /// <summary>
        /// Remove ToDo
        /// </summary>
        /// <param name="id">ToDo unique identifier</param>
        /// <returns></returns>
        public async Task<bool> Delete(Guid id)
        {
            var toDo = await GetById(id);

            _context.ToDos.Remove(toDo);

            var updatedEntities = await _context.SaveChangesAsync();

            return updatedEntities > 0;
        }

        /// <summary>
        /// Get all user's ToDo
        /// </summary>
        /// <param name="userId">User unique identifier</param>
        /// <returns>User's ToDos</returns>
        public async Task<List<ToDo>> GetByUserId(Guid userId)
        {
            return await (from t in _context.ToDos
                    where t.UserId == userId
                    select t).ToListAsync();
        }

        /// <summary>
        /// Filter ToDos
        /// </summary>
        /// <param name="userId">User unique identifier</param>
        /// <param name="isFinished">ToDo is finished</param>
        /// <param name="description">ToDo description</param>
        /// <returns>Filtered ToDos</returns>
        public IQueryable<ToDo> Filter(Guid userId, bool? isFinished = null, string? description = null) =>
            (from t in _context.ToDos
             where t.UserId == userId &&
             (isFinished == null || t.IsFinished == isFinished) &&
             (description == null || t.Description.ToLower().Contains(description.ToLower()))
             select t);
                     
        /// <summary>
        /// Paginate ToDos
        /// </summary>
        /// <param name="query">ToDo list</param>
        /// <param name="page">Page number</param>
        /// <param name="pageSize">Page size</param>
        /// <returns>Paginated ToDos</returns>
        public async Task<List<ToDo>> PaginateQuery(IQueryable<ToDo> query, int page, int pageSize)
        {
            return await query.Skip(page*pageSize).Take(pageSize).ToListAsync();
        }

    }
}
