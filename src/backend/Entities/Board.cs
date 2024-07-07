namespace OurForum.Backend.Entities;

public class Board : Base
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int Visibility { get; set; } = (int)VisibilitySetting.Hidden;
    public virtual ICollection<Post>? Posts { get; set; }

    public enum VisibilitySetting
    {
        Hidden,
        Private,
        Public
    }
}
