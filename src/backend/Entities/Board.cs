namespace OurForum.Backend.Entities
{
    public class Board : Base
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
    }
}
