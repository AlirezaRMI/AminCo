using Application.DTOs.Products;
using Application.Logics.Intefaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ProductsController(IProductService productService) : Controller
    {
        public async Task<IActionResult> Index() => View(await productService.GetAllAsync());
        
        public async Task<IActionResult> Details(long id) => View(await productService.GetByIdAsync(id));
        
        public IActionResult Create() => View();
        
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateProductDto dto)
        {
            if (!ModelState.IsValid) return View(dto);
            await productService.CreateAsync(dto);
            return RedirectToAction(nameof(Index));
        }
        
        public async Task<IActionResult> Edit(long id) => View(await productService.GetByIdAsync(id));
        
        
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UpdateProductDto dto)
        {
            if (!ModelState.IsValid) return View(dto);
            await productService.UpdateAsync(dto);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(long id)
        {
            await productService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}