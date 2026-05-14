using System.ComponentModel.DataAnnotations.Schema;
using Domain.Entites.Base;

namespace Domain.Entites
{
    [Table("Categories", Schema = "Commerce")]
    public class Category : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public long SectionId { get; set; }
        public virtual Section Section { get; set; } = null!;
        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
    }
}