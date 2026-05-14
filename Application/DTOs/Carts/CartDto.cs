using Application.DTOs.Common;

namespace Application.DTOs.Carts
{
    public class CartDto : BaseDto
    {
        public long UserId { get; set; }
        public List<CartItemDto> Items { get; set; } = [];
        public decimal TotalPrice => Items.Sum(i => i.TotalPrice);
    }

    public class CartItemDto : BaseDto
    {
        public long CartId { get; set; }

        public string? ProductImageUrl { get; set; }
        
        public long ProductId { get; set; }
        public string? ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice => Quantity * UnitPrice;
    }

    public class AddToCartDto
    {
        public long ProductId { get; set; }
        public int Quantity { get; set; } = 1;
    }

    public class UpdateCartItemDto
    {
        public long CartItemId { get; set; }
        public int Quantity { get; set; }
    }
}