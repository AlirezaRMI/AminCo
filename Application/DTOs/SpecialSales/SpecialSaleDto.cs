using Application.DTOs.Common;

namespace Application.DTOs.SpecialSales
{
    public class SpecialSaleDto : BaseDto
    {
        public long ProductId { get; set; }
        public string? ProductName { get; set; }
        public decimal SalePrice { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; }
    }

    public class CreateSpecialSaleDto
    {
        public long ProductId { get; set; }
        public decimal SalePrice { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }

    public class UpdateSpecialSaleDto
    {
        public long Id { get; set; }
        public long ProductId { get; set; }
        public decimal SalePrice { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}