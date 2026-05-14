using System.ComponentModel.DataAnnotations.Schema;
using Domain.Entites.Base;
using Domain.Enums;

namespace Domain.Entites
{
    [Table("DiscountCodes", Schema = "Commerce")]
    public class DiscountCode : BaseEntity
    {
        public string Code { get; set; } = string.Empty;
        public DiscountType Type { get; set; } 
        public decimal Value { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int UsageLimit { get; set; } = 1;
        public int UsedCount { get; set; } = 0;
    }
}