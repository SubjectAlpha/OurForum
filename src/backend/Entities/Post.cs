namespace OurForum.Backend.Entities
{
    public class Post : Base
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public virtual Board Board { get; set; }
    }
}
