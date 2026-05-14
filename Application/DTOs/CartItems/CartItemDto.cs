using Application.DTOs.Common;

namespace Application.DTOs.CartItems
{
    public class CartItemDto : BaseDto
    {
        public long CartId { get; set; }
        public long ProductId { get; set; }
        public string? ProductName { get; set; }
        public string? ProductImageUrl { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice => Quantity * UnitPrice;
    }

    public class CreateCartItemDto
    {
        public long CartId { get; set; }
        public long ProductId { get; set; }
        public int Quantity { get; set; } = 1;
        public decimal UnitPrice { get; set; } 
    }

    public class UpdateCartItemDto
    {
        public long Id { get; set; }
        public int Quantity { get; set; }
    }
}