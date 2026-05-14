using Application.DTOs.Common;
using Domain.Enums;

namespace Application.DTOs.DiscountCodes
{
    public class DiscountCodeDto : BaseDto
    {
        public string Code { get; set; } = string.Empty;
        public DiscountType Type { get; set; }
        public decimal Value { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int UsageLimit { get; set; }
        public int UsedCount { get; set; }
    }

    public class CreateDiscountCodeDto
    {
        public string Code { get; set; } = string.Empty;
        public DiscountType Type { get; set; }
        public decimal Value { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int UsageLimit { get; set; } = 1;
    }

    public class UpdateDiscountCodeDto
    {
        public long Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public DiscountType Type { get; set; }
        public decimal Value { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int UsageLimit { get; set; }
        public bool IsActive { get; set; }
    }
}