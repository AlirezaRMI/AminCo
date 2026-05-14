using Application.DTOs.AboutUs;
using Application.Logics.Intefaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AboutUsController(IAboutUsService aboutService) : Controller
    {
        public async Task<IActionResult> Index() => View(await aboutService.GetAsync());
       
        public async Task<IActionResult> Edit() => View(await aboutService.GetAsync());
        
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UpdateAboutUsDto dto)
        {
            if (!ModelState.IsValid) return View(dto);
            await aboutService.UpdateAsync(dto);
            return RedirectToAction(nameof(Index));
        }
    }
}