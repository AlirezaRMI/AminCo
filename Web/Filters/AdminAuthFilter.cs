using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Web.Filters;

public class AdminAuthFilter : IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var userId = context.HttpContext.Session.GetString("AdminId");
        if (string.IsNullOrEmpty(userId))
        {
            context.Result = new RedirectToActionResult("Login", "Account", new { area = "Admin" });
        }
    }
}