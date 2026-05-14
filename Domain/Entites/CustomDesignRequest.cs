using System.ComponentModel.DataAnnotations.Schema;
using Domain.Entites.Base;
using Domain.Enums;

namespace Domain.Entites
{
    [Table("CustomDesignRequests", Schema = "Requests")]
    public class CustomDesignRequest : BaseEntity
    {
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string? Attachments { get; set; } 
        public RequestStatus Status { get; set; } = RequestStatus.Pending;
        public string? AdminResponse { get; set; }
    }
}