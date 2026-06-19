using Application.DTOs.SpecialSales;
using Application.Logics.Intefaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class SpecialSalesController(ISpecialSaleService saleService,IProductService productService) : Controller
    {
        public async Task<IActionResult> Index() => View(await saleService.GetAllAsync());
        public async Task<IActionResult> Details(long id) => View(await saleService.GetByIdAsync(id));
        public IActionResult Create() => View();

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateSpecialSaleDto dto)
        {
            if (!ModelState.IsValid) return View(dto);
            await saleService.CreateAsync(dto);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(long id)
        {
            var sale = await saleService.GetByIdAsync(id);
            var dto = new UpdateSpecialSaleDto
            {
                Id = sale.Id,
                ProductId = sale.ProductId,
                SalePrice = sale.SalePrice,
                StartDate = sale.StartDate,
                EndDate = sale.EndDate
            };
            
            ViewData["Products"] = new SelectList(await productService.GetAllAsync(), "Id", "Name", dto.ProductId);
            return View(dto);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UpdateSpecialSaleDto dto)
        {
            if (!ModelState.IsValid) return View(dto);
            await saleService.UpdateAsync(dto);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(long id)
        {
            await saleService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}