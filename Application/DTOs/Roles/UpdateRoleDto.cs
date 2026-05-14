namespace Application.DTOs.Roles;

public class UpdateRoleDto
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public bool IsActive { get; set; }
}