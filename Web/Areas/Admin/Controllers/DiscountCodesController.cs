using Application.DTOs.DiscountCodes;
using Application.Logics.Intefaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class DiscountCodesController(IDiscountCodeService discountService) : Controller
    {
        public async Task<IActionResult> Index() => View(await discountService.GetAllAsync());
        public async Task<IActionResult> Details(long id) => View(await discountService.GetByIdAsync(id));
        public IActionResult Create() => View();

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateDiscountCodeDto dto)
        {
            if (!ModelState.IsValid) return View(dto);
            await discountService.CreateAsync(dto);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(long id)
        {
            var code = await discountService.GetByIdAsync(id);
            var dto = new UpdateDiscountCodeDto
            {
                Id = code.Id,
                Code = code.Code,
                Type = code.Type,
                Value = code.Value,
                StartDate = code.StartDate,
                EndDate = code.EndDate,
                UsageLimit = code.UsageLimit,
                IsActive = code.IsActive
            };
            return View(dto);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UpdateDiscountCodeDto dto)
        {
            if (!ModelState.IsValid) return View(dto);
            await discountService.UpdateAsync(dto);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(long id)
        {
            await discountService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}