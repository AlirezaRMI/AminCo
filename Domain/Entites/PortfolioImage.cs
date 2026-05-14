using System.ComponentModel.DataAnnotations.Schema;
using Domain.Entites.Base;

namespace Domain.Entites
{
    [Table("PortfolioImages", Schema = "Content")]
    public class PortfolioImage : BaseEntity
    {
        public long PortfolioId { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        public string? Title { get; set; }
        public string? AltText { get; set; }  
        public int DisplayOrder { get; set; }
        public bool IsMain { get; set; } = false;  
        public virtual Portfolio Portfolio { get; set; } = null!;
    }
}