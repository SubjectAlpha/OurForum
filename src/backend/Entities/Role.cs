namespace OurForum.Backend.Entities;

public class Role : Base
{
    public string Name { get; set; } = string.Empty;
    public int PowerLevel { get; set; }
    public string Claims { get; set; } = string.Empty;
    public virtual ICollection<User>? Users { get; set; }
}
