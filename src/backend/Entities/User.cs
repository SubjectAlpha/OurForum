using System.ComponentModel.DataAnnotations.Schema;

namespace OurForum.Backend.Entities;

public class User : Base
{
    public string Alias { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string HashedPassword { get; set; } = string.Empty;
    public Guid? RoleId { get; set; }
    [ForeignKey(nameof(RoleId))]
    public virtual Role? Role { get; set; } = new();
}
