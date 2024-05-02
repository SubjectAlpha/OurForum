namespace OurForum.Backend.Entities;

public class Role : Base
{
    public string Name { get; set; }
    public int PowerLevel { get; set; }
    public virtual ICollection<User> Users { get; set; }
}
