using System.ComponentModel.DataAnnotations;

namespace OurForum.Backend.Entities;

public class Base
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTime Created { get; set; } = DateTime.Now;
    public DateTime Updated { get; set; } = DateTime.Now;
    public Guid Creator { get; set; }
    public Guid Updator { get; set; }
    public bool IsDeleted { get; set; }
}
