using Application.DTOs.Common;

namespace Application.DTOs.Invoices
{
    public class InvoiceDto : BaseDto
    {
        public long OrderId { get; set; }
        public string InvoiceNumber { get; set; } = string.Empty;
        public bool IsFinal { get; set; }
        public DateTime InvoiceDate { get; set; }
        public decimal Amount { get; set; }
        public string? PdfUrl { get; set; }
    }

    public class CreateInvoiceDto
    {
        public long OrderId { get; set; }
        public bool IsFinal { get; set; }
    }
}