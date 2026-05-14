using Application.DTOs.Invoices;

namespace Application.Logics.Intefaces
{
    public interface IInvoiceService
    {
        Task<InvoiceDto> CreateProformaInvoiceAsync(long orderId);
        Task<InvoiceDto> FinalizeInvoiceAsync(long orderId);
        Task<InvoiceDto> GetByOrderIdAsync(long orderId);
        Task<InvoiceDto> GetByIdAsync(long id);
        
        Task<List<InvoiceDto>> GetAllAsync();
        Task<byte[]> GeneratePdfAsync(long invoiceId);
    }
}