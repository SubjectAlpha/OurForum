using Microsoft.EntityFrameworkCore;
using OurForum.Backend.Entities;
using OurForum.Backend.Utility;

namespace OurForum.Backend.Services;

public interface IUserService
{
    public Task<User?> Create(string alias, string email, string hashedPassword, Role? role);
    public Task<User?> Update(User u);

    public Task<User?> Get(Guid id);

    public Task<User?> GetByEmail(string email);

    public Task<IEnumerable<string>?> GetPermissions(Guid userId);
}

public class UserService(DatabaseContext context) : IUserService
{
    private readonly DatabaseContext _context = context;

    public async Task<User?> Create(string alias, string email, string hashedPassword, Role? role)
    {
        _context.Database.EnsureCreated();
        var user = new User
        {
            Alias = alias,
            Email = email,
            HashedPassword = hashedPassword,
            Role = role
        };
        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return await _context.Users.Include(x => x.Role).FirstOrDefaultAsync(x => x.Email == email);
    }

    public async Task<User?> Update(User u)
    {
        if (u.Id != Guid.Empty)
        {
            _context.Users.Update(u);
            await _context.SaveChangesAsync();

            return await Get(u.Id);
        }

        return null;
    }

    public async Task<bool> Delete(User u)
    {
        if (u.Id != Guid.Empty)
        {
            u.IsDeleted = true;
            _context.Update(u);
            await _context.SaveChangesAsync();
            return true;
        }
        return false;
    }

    public async Task<User?> Get(Guid id)
    {
        return await _context.Users.Include(x => x.Role).FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<User?> GetByEmail(string email)
    {
        return await _context.Users.Include(x => x.Role).FirstOrDefaultAsync(x => x.Email == email);
    }

    public async Task<IEnumerable<string>?> GetPermissions(Guid userId)
    {
        var user = await Get(userId);

        if (user is not null)
        {
            return user.Role?.Permissions.Split(";");
        }

        return [];
    }
}
