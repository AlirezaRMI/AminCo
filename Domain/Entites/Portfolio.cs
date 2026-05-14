using System.ComponentModel.DataAnnotations.Schema;
using Domain.Entites.Base;
using Domain.Enums;

namespace Domain.Entites
{
    [Table("Portfolios", Schema = "Content")]
    public class Portfolio : BaseEntity
    {
        public string Title { get; set; } = string.Empty;
        public string? Slug { get; set; }   
        public string? Description { get; set; }
        public string? ClientName { get; set; }    
        public DateTime? ProjectDate { get; set; } 
        public string? ProjectUrl { get; set; }    
        public PortfolioCategory Category { get; set; } = PortfolioCategory.Other;
        public int DisplayOrder { get; set; } = 0;
        
        public virtual ICollection<PortfolioImage> Images { get; set; } = new List<PortfolioImage>();
    }
}