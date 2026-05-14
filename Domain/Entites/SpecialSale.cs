using System.ComponentModel.DataAnnotations.Schema;
using Domain.Entites.Base;

namespace Domain.Entites
{
    [Table("SpecialSales", Schema = "Commerce")]
    public class SpecialSale : BaseEntity
    {
        public long ProductId { get; set; }
        public decimal SalePrice { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        
        public virtual Product Product { get; set; } = null!;
        
        public bool IsActive => StartDate <= DateTime.UtcNow && EndDate >= DateTime.UtcNow;
    }
}