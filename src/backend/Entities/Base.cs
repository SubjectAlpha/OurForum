using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace OurForum.Backend.Entities;

public class Base
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTime Created { get; set; } = DateTime.Now;
    public DateTime Updated { get; set; } = DateTime.Now;
    public Guid Creator { get; set; }
    public Guid Updator { get; set; }

    [JsonIgnore]
    public bool IsDeleted { get; set; }
}
