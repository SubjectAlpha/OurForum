using System.ComponentModel.DataAnnotations.Schema;

namespace OurForum.Backend.Entities;

public class Post : Base
{
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public Guid BoardId { get; set; }

    [ForeignKey(nameof(BoardId))]
    public virtual Board? Board { get; set; }
}
