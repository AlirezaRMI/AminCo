using System.ComponentModel.DataAnnotations.Schema;
using Domain.Entites.Base;

namespace Domain.Entites
{
    [Table("Products", Schema = "Commerce")]
    public class Product : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public decimal? DiscountPrice { get; set; } 
        public int StockQuantity { get; set; }
        public string? MainImageUrl { get; set; }
        public long CategoryId { get; set; }
        public virtual Category Category { get; set; } = null!;
        public virtual ICollection<SpecialSale> SpecialSales { get; set; } = new List<SpecialSale>();
    }
}