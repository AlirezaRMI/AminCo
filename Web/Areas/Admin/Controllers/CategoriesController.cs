using Application.DTOs.Categories;
using Application.Logics.Intefaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class CategoriesController(ICategoryService categoryService) : Controller
    {
        public async Task<IActionResult> Index() => View(await categoryService.GetAllAsync());
        
        public async Task<IActionResult> Details(long id) => View(await categoryService.GetByIdAsync(id));
        
        public IActionResult Create() => View();

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateCategoryDto dto)
        {
            if (!ModelState.IsValid) return View(dto);
            await categoryService.CreateAsync(dto);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(long id)
        {
            var category = await categoryService.GetByIdAsync(id);
            var updateDto = new UpdateCategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description,
                ImageUrl = category.ImageUrl,
                SectionId = category.SectionId,
                IsActive = category.IsActive
            };
            return View(updateDto);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UpdateCategoryDto dto)
        {
            if (!ModelState.IsValid) return View(dto);
            await categoryService.UpdateAsync(dto);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(long id)
        {
            await categoryService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}