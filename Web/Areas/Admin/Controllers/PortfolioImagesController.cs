using Application.DTOs.PortfolioImages;
using Application.Logics.Intefaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class PortfolioImagesController(IPortfolioImageService imageService) : Controller
    {
        public async Task<IActionResult> Index(long portfolioId) => View(await imageService.GetByPortfolioIdAsync(portfolioId));
        
        public async Task<IActionResult> Details(long id) => View(await imageService.GetByIdAsync(id));
        
        public IActionResult Create(long portfolioId) => View(new CreatePortfolioImageDto { PortfolioId = portfolioId });
        
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreatePortfolioImageDto dto)
        {
            if (!ModelState.IsValid) return View(dto);
            await imageService.CreateAsync(dto);
            return RedirectToAction(nameof(Index), new { portfolioId = dto.PortfolioId });
        }
        
        public async Task<IActionResult> Edit(long id) => View(await imageService.GetByIdAsync(id));
        
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UpdatePortfolioImageDto dto)
        {
            if (!ModelState.IsValid) return View(dto);
            await imageService.UpdateAsync(dto);
            return RedirectToAction(nameof(Index), new { portfolioId = dto.Id });
        }
        
        public async Task<IActionResult> Delete(long id)
        {
            var image = await imageService.GetByIdAsync(id);
            await imageService.DeleteAsync(id);
            return RedirectToAction(nameof(Index), new { portfolioId = image.PortfolioId });
        }
    }
}