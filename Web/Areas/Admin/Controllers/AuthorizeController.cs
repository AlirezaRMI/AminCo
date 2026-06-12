using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Application.Logics.Intefaces;
using Web.Models;
using System.Security.Claims;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [AllowAnonymous]
    public class AuthorizeController(
        IUserService userService,
        IJwtService jwtService,
        IRoleService roleService,
        ILogger<AuthorizeController> logger)
        : Controller
    {
        [HttpGet]
        public IActionResult Login(string? returnUrl = null)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string? returnUrl = null)
        {
            if (string.IsNullOrWhiteSpace(model.Email) || string.IsNullOrWhiteSpace(model.Password))
            {
                ModelState.AddModelError("", "ایمیل و رمز عبور الزامی است.");
                return View(model);
            }

            var isValid = await userService.ValidateCredentialsAsync(model.Email, model.Password);
            if (!isValid)
            {
                ModelState.AddModelError("", "ایمیل یا رمز عبور اشتباه است.");
                return View(model);
            }

            var user = await userService.GetByEmailAsync(model.Email);
            var roles = await roleService.GetUserRolesAsync(user.Id);
            if (!roles.Contains("Admin"))
            {
                ModelState.AddModelError("", "دسترسی ادمین ندارید.");
                return View(model);
            }

            try
            {
                var token = jwtService.GenerateToken(user, roles);
                Response.Cookies.Append("accessToken", token, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = false,
                    SameSite = SameSiteMode.Lax,
                    Expires = DateTime.UtcNow.AddDays(1)
                });
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Token generation failed");
                ModelState.AddModelError("", "خطا در احراز هویت");
                return View(model);
            }

            return LocalRedirect("/Admin/Dashboard");
            
        }

        public IActionResult Logout()
        {
            Response.Cookies.Delete("accessToken");
            return RedirectToAction("Login");
        }

        [Authorize]
        public IActionResult Test()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var roles = User.FindAll(ClaimTypes.Role).Select(c => c.Value).ToList();
            return Content($"Authenticated: {User.Identity.IsAuthenticated}\nUserId: {userId}\nRoles: {string.Join(", ", roles)}");
        }
    }
}