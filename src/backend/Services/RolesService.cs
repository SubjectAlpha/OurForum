using Microsoft.EntityFrameworkCore;
using OurForum.Backend.Entities;
using OurForum.Backend.Utility;

namespace OurForum.Backend.Services;

public interface IRolesService
{
    public Task<IList<string>?> GetPermissions(Guid roleId);
    public Task<Role?> Get(string name);
    public Task<Role?> Create(string name, string permissions, int powerLevel);
    public Task<IList<Role>?> GetAll();
}

public class RolesService(DatabaseContext context) : IRolesService
{
    private readonly DatabaseContext _context = context;

    public async Task<IList<string>?> GetPermissions(Guid roleId)
    {
        var roles = await _context.Roles.FirstOrDefaultAsync(x => x.Id == roleId);
        return roles?.Permissions.Split(";");
    }

    public async Task<Role?> Get(string name)
    {
        return await _context.Roles.FirstOrDefaultAsync(x => x.Name == name);
    }

    public async Task<Role?> Create(string name, string permissions, int powerLevel)
    {
        _context.Database.EnsureCreated();
        var r = new Role
        {
            Name = name,
            Permissions = permissions,
            PowerLevel = powerLevel,
        };
        _context.Roles.Add(r);
        _context.SaveChanges();
        return await _context.Roles.FirstOrDefaultAsync(x => x.Name == r.Name);
    }

    public async Task<IList<Role>?> GetAll()
    {
        _context.Database.EnsureCreated();
        return await _context.Roles.ToListAsync();
    }
}
