using Application.DTOs.Invoices;
using Application.Logics.Intefaces;
using AutoMapper;
using Domain.Contract;
using Domain.Entites;
using Domain.Exceptions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Application.Logics.Services
{
    public class InvoiceService(
        IAsyncRepository<Invoice, long> invoiceRepo,
        IServiceScopeFactory scopeFactory,
        IMapper mapper,
        ILogger<InvoiceService> logger)
        : IInvoiceService
    {
        private async Task<Order> GetOrderByIdAsync(long orderId)
        {
            using var scope = scopeFactory.CreateScope();
            var orderRepo = scope.ServiceProvider.GetRequiredService<IAsyncRepository<Order, long>>();
            var order = await orderRepo.GetByIdAsync(orderId);
            if (order == null || order.IsDeleted)
                throw new NotFoundException("سفارش یافت نشد.");
            return order;
        }

        public async Task<InvoiceDto> CreateProformaInvoiceAsync(long orderId)
        {
            var order = await GetOrderByIdAsync(orderId);

            var existing = await invoiceRepo.GetSingleAsync(i => i.OrderId == orderId && !i.IsFinal && !i.IsDeleted);
            if (existing != null)
                return mapper.Map<InvoiceDto>(existing);

            var invoice = new Invoice
            {
                OrderId = orderId,
                InvoiceNumber = GenerateInvoiceNumber(false),
                IsFinal = false,
                InvoiceDate = DateTime.UtcNow,
                Amount = order.TotalAmount
            };
            await invoiceRepo.AddEntity(invoice);
            await invoiceRepo.SaveChangesAsync();
            return mapper.Map<InvoiceDto>(invoice);
        }

        public async Task<InvoiceDto> FinalizeInvoiceAsync(long orderId)
        {
            // بررسی وجود سفارش
            await GetOrderByIdAsync(orderId);

            var invoice = await invoiceRepo.GetSingleAsync(i => i.OrderId == orderId && !i.IsDeleted);
            if (invoice == null)
                invoice = new Invoice { OrderId = orderId, IsFinal = false };

            invoice.IsFinal = true;
            invoice.InvoiceNumber = GenerateInvoiceNumber(true);
            invoice.InvoiceDate = DateTime.UtcNow;

            // برای به‌روز کردن مبلغ، دوباره سفارش را می‌خوانیم (با استفاده از scope جدید)
            using var scope = scopeFactory.CreateScope();
            var orderRepo = scope.ServiceProvider.GetRequiredService<IAsyncRepository<Order, long>>();
            var order = await orderRepo.GetByIdAsync(orderId);
            invoice.Amount = order?.TotalAmount ?? 0;

            await invoiceRepo.UpdateEntity(invoice);
            await invoiceRepo.SaveChangesAsync();
            return mapper.Map<InvoiceDto>(invoice);
        }

        public async Task<InvoiceDto> GetByOrderIdAsync(long orderId)
        {
            var invoice = await invoiceRepo.GetSingleAsync(i => i.OrderId == orderId && !i.IsDeleted);
            if (invoice == null)
                throw new NotFoundException("فاکتور یافت نشد.");
            return mapper.Map<InvoiceDto>(invoice);
        }

        public async Task<InvoiceDto> GetByIdAsync(long id)
        {
            var invoice = await invoiceRepo.GetByIdAsync(id);
            if (invoice == null || invoice.IsDeleted)
                throw new NotFoundException("فاکتور یافت نشد.");
            return mapper.Map<InvoiceDto>(invoice);
        }

        public async Task<List<InvoiceDto>> GetAllAsync()
        {
            logger.LogInformation("getting all invoices.");
            var invoices=await invoiceRepo.GetAllAsync();
            return mapper.Map<List<InvoiceDto>>(invoices);
        }

        private string GenerateInvoiceNumber(bool isFinal)
        {
            var prefix = isFinal ? "INV" : "PRO";
            return $"{prefix}-{DateTime.UtcNow:yyyyMMddHHmmss}-{Guid.NewGuid().ToString().Substring(0, 6)}";
        }

        public Task<byte[]> GeneratePdfAsync(long invoiceId) => throw new NotImplementedException();
    }
}