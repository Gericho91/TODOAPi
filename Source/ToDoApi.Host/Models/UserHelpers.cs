using System.Security.Claims;
using System.Security.Principal;

namespace ToDoApi.Host.Models
{
    public static class UserHelpers
    {
        public static Guid? GetUserId(this IPrincipal principal)
        {
            var claimsIdentity = (ClaimsIdentity)principal.Identity;
            var claim = claimsIdentity?.FindFirst("Id");
            return claim?.Value != null ? Guid.Parse(claim.Value) : null;
        }

        public static string? GetUserEmailAddress(this IPrincipal principal)
        {
            var claimsIdentity = (ClaimsIdentity)principal.Identity;
            var claim = claimsIdentity?.FindFirst(ClaimTypes.NameIdentifier);
            return claim?.Value;
        }
    }
}
