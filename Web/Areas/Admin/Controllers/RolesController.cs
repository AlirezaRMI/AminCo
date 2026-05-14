using Application.DTOs.Roles;
using Application.Logics.Intefaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class RolesController(IRoleService roleService) : Controller
    {
        public async Task<IActionResult> Index() => View(await roleService.GetAllAsync());
        public async Task<IActionResult> Details(long id) => View(await roleService.GetByIdAsync(id));
        public IActionResult Create() => View();
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateRoleDto dto)
        {
            if (!ModelState.IsValid) return View(dto);
            await roleService.CreateAsync(dto);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Edit(long id) => View(await roleService.GetByIdAsync(id));
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UpdateRoleDto dto)
        {
            if (!ModelState.IsValid) return View(dto);
            await roleService.UpdateAsync(dto);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(long id)
        {
            await roleService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}