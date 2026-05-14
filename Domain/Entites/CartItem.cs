using System.ComponentModel.DataAnnotations.Schema;
using Domain.Entites.Base;

namespace Domain.Entites;

[Table("CartItems", Schema = "Sales")]
public class CartItem : BaseEntity
{
    public long CartId { get; set; }
    public long ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public virtual Cart Cart { get; set; } = null!;
    public virtual Product Product { get; set; } = null!;
}