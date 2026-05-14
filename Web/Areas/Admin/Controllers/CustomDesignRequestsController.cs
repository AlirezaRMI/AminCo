using Application.DTOs.CustomDesignRequests;
using Application.Logics.Intefaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class CustomDesignRequestsController(ICustomDesignRequestService requestService) : Controller
    {
        public async Task<IActionResult> Index() => View(await requestService.GetAllAsync());
       
        public async Task<IActionResult> Details(long id) => View(await requestService.GetByIdAsync(id));
        
        public async Task<IActionResult> Edit(long id)
        {
            var dto = await requestService.GetByIdAsync(id);
            return View(dto);
        }
        
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UpdateCustomDesignRequestDto dto)
        {
            if (!ModelState.IsValid) return View(dto);
            await requestService.UpdateStatusAsync(dto);
            return RedirectToAction(nameof(Index));
        }
        
        public async Task<IActionResult> Delete(long id)
        {
            await requestService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}