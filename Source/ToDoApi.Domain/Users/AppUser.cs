using Microsoft.AspNetCore.Identity;

namespace ToDoApi.Domain.Users
{
    public class AppUser : IdentityUser<Guid>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime CreationDate { get; set; }
        public Nullable<DateTime> LastModifiedDate { get; set; }
    }
}
