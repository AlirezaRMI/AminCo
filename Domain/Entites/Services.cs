using System.ComponentModel.DataAnnotations.Schema;
using Domain.Entites.Base;

namespace Domain.Entites
{
    [Table("Services", Schema = "Content")]
    public class Service : BaseEntity
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string? IconUrl { get; set; } 
        public int DisplayOrder { get; set; } = 0;
    }
}