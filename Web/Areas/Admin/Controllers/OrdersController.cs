using Application.DTOs.Orders;
using Application.Logics.Intefaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class OrdersController(IOrderService orderService) : Controller
    {
        public async Task<IActionResult> Index() => View(await orderService.GetAllAsync()); 
        public async Task<IActionResult> Details(long id) => View(await orderService.GetByIdAsync(id));
        public async Task<IActionResult> UpdateStatus(long id)
        {
            var order = await orderService.GetByIdAsync(id);
            return View(order);
        }
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateStatus(UpdateOrderStatusDto dto)
        {
            await orderService.UpdateOrderStatusAsync(dto);
            return RedirectToAction(nameof(Index));
        }
    }
}