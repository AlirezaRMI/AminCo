using Application.DTOs.Portfolios;
using Application.Logics.Intefaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class PortfoliosController(IPortfolioService portfolioService) : Controller
    {
        public async Task<IActionResult> Index() => View(await portfolioService.GetAllAsync());
        
        public async Task<IActionResult> Details(long id) => View(await portfolioService.GetByIdAsync(id));
        
        public IActionResult Create() => View();
        
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreatePortfolioDto dto)
        {
            if (!ModelState.IsValid) return View(dto);
            await portfolioService.CreateAsync(dto);
            return RedirectToAction(nameof(Index));
        }
        
        public async Task<IActionResult> Edit(long id) => View(await portfolioService.GetByIdAsync(id));
        
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UpdatePortfolioDto dto)
        {
            if (!ModelState.IsValid) return View(dto);
            await portfolioService.UpdateAsync(dto);
            return RedirectToAction(nameof(Index));
        }
        
        public async Task<IActionResult> Delete(long id)
        {
            await portfolioService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}