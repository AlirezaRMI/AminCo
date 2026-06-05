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
            logger.LogInformation("=== LOGIN ATTEMPT for {Email} ===", model.Email);

            if (string.IsNullOrWhiteSpace(model.Email) || string.IsNullOrWhiteSpace(model.Password))
            {
                ModelState.AddModelError("", "ایمیل و رمز عبور هر دو الزامی هستند.");
                return View(model);
            }

            if (!ModelState.IsValid) return View(model);

            var isValid = await userService.ValidateCredentialsAsync(model.Email, model.Password);
            logger.LogInformation("ValidateCredentialsAsync result: {IsValid}", isValid);
            if (!isValid)
            {
                ModelState.AddModelError("", "ایمیل یا رمز عبور اشتباه است.");
                return View(model);
            }
            
            var user = await userService.GetByEmailAsync(model.Email);
            if (user == null)
            {
                ModelState.AddModelError("", "کاربر یافت نشد.");
                return View(model);
            }

            logger.LogInformation("User found: Id={UserId}, Email={Email}", user.Id, user.Email);

            // 3. گرفتن نقش‌ها از RoleService
            var roles = await roleService.GetUserRolesAsync(user.Id);
            logger.LogInformation("User roles: {Roles}", string.Join(", ", roles));

            if (!roles.Contains("Admin"))
            {
                ModelState.AddModelError("", "شما دسترسی ادمین ندارید.");
                logger.LogWarning("User {Email} does not have Admin role.", model.Email);
                return View(model);
            }

            // 4. ساخت توکن JWT
            var token = jwtService.GenerateToken(user, roles);
            logger.LogInformation("Token generated (first 50 chars): {Token}",
                token?.Substring(0, Math.Min(50, token?.Length ?? 0)));

            // 5. ذخیره در کوکی
            Response.Cookies.Append("accessToken", token, new CookieOptions
            {
                HttpOnly = true,
                Secure = false, // در لوکال host false بگذار
                SameSite = SameSiteMode.Lax,
                Expires = DateTime.UtcNow.AddDays(1)
            });
            logger.LogInformation("Cookie 'accessToken' set.");

            // 6. هدایت
            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                return Redirect(returnUrl);

            logger.LogInformation("Redirecting to Dashboard/Index");
            return RedirectToAction("Index", "Dashboard");
        }

        public IActionResult Logout()
        {
            Response.Cookies.Delete("accessToken");
            logger.LogInformation("User logged out, cookie deleted.");
            return RedirectToAction("Login");
        }

        [Authorize]
        public IActionResult Test()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var roles = User.FindAll(ClaimTypes.Role).Select(c => c.Value).ToList();
            return Content(
                $"Authenticated: {User.Identity.IsAuthenticated}\nUserId: {userId}\nRoles: {string.Join(", ", roles)}");
        }
    }
}