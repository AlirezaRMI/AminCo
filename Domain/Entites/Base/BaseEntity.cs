using System.Reflection.Emit;
using Domain.Common;

namespace Domain.Entites.Base
{
    public class BaseEntity : IEntity<long>
    {
        public long Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public long UpdatedBy { get; set; }
        public bool IsDeleted { get; set; } = false;
        public bool IsActive { get; set; }
    }
}