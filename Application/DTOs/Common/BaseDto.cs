namespace Application.DTOs.Common
{
    public abstract class BaseDto
    {
        public long Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsActive { get; set; }
    }
}