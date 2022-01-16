using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

using ToDoApi.Domain.Configurations;
using ToDoApi.Domain.Users;

namespace ToDoApi.Application.Users
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;

        public UserService(
            UserManager<IdentityUser> userManager,
            IOptionsMonitor<JwtSettings> jwtSettings)
        {
            _userManager = _userManager ?? throw new ArgumentNullException(nameof(_userManager));
        }

    }
}
