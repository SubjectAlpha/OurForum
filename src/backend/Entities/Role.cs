using System.Text.Json.Serialization;

namespace OurForum.Backend.Entities;

public class Role : Base
{
    public string Name { get; set; } = string.Empty;
    public int PowerLevel { get; set; }
    public string Permissions { get; set; } = string.Empty;

    [JsonIgnore]
    public virtual ICollection<User>? Users { get; set; }
}
