using OurForum.Backend.Entities;
using OurForum.Backend.Utility;

namespace OurForum.Backend.Services;

public interface IRolesService
{
    public IList<string>? GetPermissions(Guid roleId);
    public Role? Get(string name);
    public Role? Create(string name, string permissions, int powerLevel);
}

public class RolesService(DatabaseContext context) : IRolesService
{
    private readonly DatabaseContext _context = context;
    public IList<string>? GetPermissions(Guid roleId)
    {
        return _context.Roles.FirstOrDefault(x => x.Id == roleId)?.Permissions.Split(";");
    }

    public Role? Get(string name)
    {
        return _context.Roles.FirstOrDefault(x => x.Name == name);
    }

    public Role? Create(string name, string permissions, int powerLevel)
    {
        _context.Database.EnsureCreated();
        var r = new Role
        {
            Name = name,
            Permissions = permissions,
            PowerLevel = powerLevel
        };
        _context.Roles.Add(r);
        _context.SaveChanges();
        return _context.Roles.FirstOrDefault(x => x.Name == r.Name);
    }
}
