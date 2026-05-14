using Application.DTOs.Sections;
using Application.Logics.Intefaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class SectionsController(ISectionService sectionService) : Controller
    {
        public async Task<IActionResult> Index() => View(await sectionService.GetAllAsync());
        public async Task<IActionResult> Details(long id) => View(await sectionService.GetByIdAsync(id));
        public IActionResult Create() => View();

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateSectionDto dto)
        {
            if (!ModelState.IsValid) return View(dto);
            await sectionService.CreateAsync(dto);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(long id) => View(await sectionService.GetByIdAsync(id));

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UpdateSectionDto dto)
        {
            if (!ModelState.IsValid) return View(dto);
            await sectionService.UpdateAsync(dto);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(long id)
        {
            await sectionService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}