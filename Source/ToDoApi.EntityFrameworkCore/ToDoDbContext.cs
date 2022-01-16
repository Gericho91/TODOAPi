using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using ToDoApi.Domain.Roles;
using ToDoApi.Domain.ToDos;
using ToDoApi.Domain.Users;

namespace TODOApi.EntityFrameworkCore
{
    public class ToDoDbContext : IdentityDbContext<AppUser, AppUserRole, Guid>
    {
        public ToDoDbContext(DbContextOptions<ToDoDbContext> options)
    : base(options)
        {
        }

        public DbSet<ToDo> ToDos { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
