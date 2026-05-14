using Application.Logics.Intefaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class InvoicesController(IInvoiceService invoiceService) : Controller
    {
        public async Task<IActionResult> Index() => View(await invoiceService.GetAllAsync());
       
        public async Task<IActionResult> Details(long id) => View(await invoiceService.GetByIdAsync(id));
        
        public async Task<IActionResult> Finalize(long orderId)
        {
            await invoiceService.FinalizeInvoiceAsync(orderId);
            return RedirectToAction(nameof(Index));
        }
    }
}