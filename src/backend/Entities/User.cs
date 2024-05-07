namespace OurForum.Backend.Entities;

public class User : Base
{
    public string Alias { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string HashedPassword { get; set; } = string.Empty;
    public virtual Role? Role { get; set; } = new();
}
