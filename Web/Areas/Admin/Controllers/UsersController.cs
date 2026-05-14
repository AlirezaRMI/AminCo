using Application.DTOs.Users;
using Application.Logics.Intefaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class UsersController(IUserService userService, IRoleService roleService) : Controller
    {
        public async Task<IActionResult> Index() => View(await userService.GetAllAsync());
        public async Task<IActionResult> Details(long id) => View(await userService.GetByIdAsync(id));
        public async Task<IActionResult> Edit(long id) => View(await userService.GetByIdAsync(id));
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UpdateUserDto dto)
        {
            if (!ModelState.IsValid) return View(dto);
            await userService.UpdateAsync(dto);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(long id)
        {
            await userService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> ManageRoles(long id)
        {
            var user = await userService.GetByIdAsync(id);
            var allRoles = await roleService.GetAllAsync();
            var userRoles = await roleService.GetUserRolesAsync(id);
            var model = new UserRolesManagementViewModel
            {
                UserId = user.Id,
                UserName = user.FullName,
                Roles = allRoles.Select(r => new RoleCheckboxDto
                {
                    RoleId = r.Id,
                    RoleName = r.Name,
                    IsAssigned = userRoles.Contains(r.Name)
                }).ToList()
            };
            return View(model);
        }
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> ManageRoles(UserRolesManagementViewModel model)
        {
            var currentRoles = await roleService.GetUserRolesAsync(model.UserId);
            foreach (var role in model.Roles)
            {
                if (role.IsAssigned && !currentRoles.Contains(role.RoleName))
                    await roleService.AssignRoleToUserAsync(model.UserId, role.RoleId);
                else if (!role.IsAssigned && currentRoles.Contains(role.RoleName))
                    await roleService.UnassignRoleFromUserAsync(model.UserId, role.RoleId);
            }
            return RedirectToAction(nameof(Index));
        }
    }

    public class UserRolesManagementViewModel
    {
        public long UserId { get; set; }
        public string UserName { get; set; }
        public List<RoleCheckboxDto> Roles { get; set; } = new();
    }
    public class RoleCheckboxDto
    {
        public long RoleId { get; set; }
        public string RoleName { get; set; }
        public bool IsAssigned { get; set; }
    }
}