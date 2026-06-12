// Domain/Entites/Partner.cs
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Entites.Base;

namespace Domain.Entites
{
    [Table("Partners", Schema = "Content")]
    public class Partner : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string? LogoUrl { get; set; }
        public string? Website { get; set; }
        public int DisplayOrder { get; set; } = 0;
        public bool IsActive { get; set; } = true;
    }
}