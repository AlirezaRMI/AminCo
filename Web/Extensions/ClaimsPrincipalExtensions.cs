using System.Security.Claims;

namespace Web.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static long GetUserId(this ClaimsPrincipal principal)
        {
            var claim = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value
                        ?? principal.FindFirst("sub")?.Value;
            return long.TryParse(claim, out var id) ? id : 0;
        }

        public static string? GetEmail(this ClaimsPrincipal principal)
        {
            return principal.FindFirst(ClaimTypes.Email)?.Value
                   ?? principal.FindFirst("email")?.Value;
        }
        
        public static string[] GetPermissions(this ClaimsPrincipal principal)
        {
            return principal.FindAll("permission").Select(c => c.Value).ToArray();
        }

        public static string[] GetRoles(this ClaimsPrincipal principal)
        {
            return principal.FindAll(ClaimTypes.Role).Select(c => c.Value).ToArray();
        }

        public static bool IsAuthenticated(this ClaimsPrincipal principal)
        {
            return principal.Identity?.IsAuthenticated == true;
        }
    }
}