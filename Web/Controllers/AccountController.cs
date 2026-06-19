using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Application.Logics.Intefaces;
using Application.DTOs.Users;
using System.Security.Claims;
using Web.Models;

namespace Web.Controllers
{
    [AllowAnonymous]
    public class AccountController(
        IUserService userService,
        IRoleService roleService,
        ILogger<AccountController> logger)
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
            
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.FullName),
                new Claim(ClaimTypes.Email, user.Email)
            };
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, new AuthenticationProperties
            {
                IsPersistent = true,
                ExpiresUtc = DateTime.UtcNow.AddDays(7)
            });

            logger.LogInformation("User {Email} logged in", user.Email);

            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                return Redirect(returnUrl);
            if (roles.Contains("Admin"))
                return RedirectToAction("Index", "Dashboard", new { area = "Admin" });
            
            return RedirectToAction("Index", "Home");
        }
        
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterUserDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            try
            {
                var user = await userService.RegisterAsync(dto);
                var customerRole = await roleService.GetByNameAsync("Customer");
                if (customerRole != null)
                {
                    await roleService.AssignRoleToUserAsync(user.Id, customerRole.Id);
                }
                
                TempData["RegisterSuccess"] = "ثبت‌نام با موفقیت انجام شد. لطفاً وارد شوید.";
                return RedirectToAction(nameof(Login));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "خطا در ثبت‌نام کاربر");
                ModelState.AddModelError("", "خطا در ثبت‌نام. ایمیل ممکن است تکراری باشد.");
                return View(dto);
            }
        }
        
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
    }
}