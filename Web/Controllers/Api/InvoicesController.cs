using Application.DTOs.Invoices;
using Application.Logics.Intefaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Filters;

namespace Web.Controllers.Api
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class InvoicesController(IInvoiceService invoiceService) : ControllerBase
    {
        [HttpGet("by-order/{orderId}")]
        public async Task<ApiResult<InvoiceDto>> GetByOrderId(long orderId)
            => await invoiceService.GetByOrderIdAsync(orderId);

        [HttpGet("{id}")]
        public async Task<ApiResult<InvoiceDto>> GetById(long id)
            => await invoiceService.GetByIdAsync(id);

        [Authorize(Roles = "Admin")]
        [HttpPost("finalize/{orderId}")]
        public async Task<ApiResult<InvoiceDto>> FinalizeInvoice(long orderId)
            => await invoiceService.FinalizeInvoiceAsync(orderId);
    }
}