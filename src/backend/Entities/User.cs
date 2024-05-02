namespace OurForum.Backend.Entities;

public class User : Base
{
    public string Alias { get; set; }
    public string Email { get; set; }
    public string EncryptedPassword { get; set; }
    public virtual Role Role { get; set; }
}
