using System.ComponentModel.DataAnnotations.Schema;
using Domain.Entites.Base;
using Domain.Enums;

namespace Domain.Entites;

[Table("Orders", Schema = "Sales")]
public class Order : BaseEntity
{
    public long UserId { get; set; }
    public DateTime OrderDate { get; set; } = DateTime.UtcNow;
    public OrderStatus Status { get; set; } = OrderStatus.Pending;
    public decimal SubTotal { get; set; }
    public decimal DiscountAmount { get; set; }
    public decimal TaxAmount { get; set; }
    public decimal TotalAmount { get; set; }
    public string? DiscountCode { get; set; }
    public string? ShippingAddress { get; set; }
    public string? PaymentMethod { get; set; }
    public bool IsPaid { get; set; }
    
    public virtual User User { get; set; } = null!;
    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    public virtual Invoice? Invoice { get; set; }
}


