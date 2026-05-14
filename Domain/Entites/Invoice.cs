using System.ComponentModel.DataAnnotations.Schema;
using Domain.Entites.Base;

namespace Domain.Entites;

[Table("Invoices", Schema = "Sales")]
public class Invoice : BaseEntity
{
    public long OrderId { get; set; }
    public string InvoiceNumber { get; set; } = string.Empty;
    public bool IsFinal { get; set; } 
    public DateTime InvoiceDate { get; set; } = DateTime.UtcNow;
    public decimal Amount { get; set; }
    public string? PdfUrl { get; set; }
    
    public virtual Order Order { get; set; } = null!;
}