namespace OurForum.Backend.Entities
{
    public class Board : Base
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public virtual ICollection<Post>? Posts { get; set; }
    }
}
