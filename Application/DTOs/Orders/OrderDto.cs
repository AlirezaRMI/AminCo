using Application.DTOs.Common;
using Domain.Enums;

namespace Application.DTOs.Orders
{
    public class OrderDto : BaseDto
    {
        public long UserId { get; set; }
        public DateTime OrderDate { get; set; }
        public OrderStatus Status { get; set; }
        public decimal SubTotal { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public string? DiscountCode { get; set; }
        public string? ShippingAddress { get; set; }
        public string? PaymentMethod { get; set; }
        public bool IsPaid { get; set; }
        public List<OrderItemDto> OrderItems { get; set; } = [];
    }

    public class OrderItemDto : BaseDto
    {
        public long OrderId { get; set; }
        public long ProductId { get; set; }

        public string? ProductImageUrl { get; set; }
        public string? ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
    }

    public class CreateOrderDto
    {
        public string? ShippingAddress { get; set; }
        public string? PaymentMethod { get; set; }
        public string? DiscountCode { get; set; }
    }

    public class UpdateOrderStatusDto
    {
        public long OrderId { get; set; }
        public OrderStatus Status { get; set; }
    }
}