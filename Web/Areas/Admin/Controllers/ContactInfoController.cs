using Application.DTOs.ContactInfo;
using Application.Logics.Intefaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ContactInfoController(IContactInfoService contactService) : Controller
    {
        public async Task<IActionResult> Index() => View(await contactService.GetAsync());
      
        public async Task<IActionResult> Edit() => View(await contactService.GetAsync());

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UpdateContactInfoDto dto)
        {
            if (!ModelState.IsValid) return View(dto);
            await contactService.UpdateAsync(dto);
            return RedirectToAction(nameof(Index));
        }
    }
}