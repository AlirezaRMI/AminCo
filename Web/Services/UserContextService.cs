using System.Security.Claims;
using Domain.Contract;
using Web.Extensions;

namespace Web.Services
{
    public class UserContextService(IHttpContextAccessor httpContextAccessor) : IUserContextService
    {
        private ClaimsPrincipal? User => httpContextAccessor.HttpContext?.User;

        public long UserId => User?.GetUserId() ?? 0;
        public string[] Roles => User?.GetRoles() ?? [];
        public string[] Permissions => User?.GetPermissions() ?? [];
        public bool IsAuthenticated => User?.Identity?.IsAuthenticated ?? false;
    }
}