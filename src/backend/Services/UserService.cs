using OurForum.Backend.Entities;
using OurForum.Backend.Utility;

namespace OurForum.Backend.Services;

public interface IUserService
{
    public User? Create(string alias, string email, string hashedPassword, Role role);

    public User? Get(Guid id);

    public User? GetByEmail(string email);

    public IEnumerable<string>? GetPermissions(Guid userId);
}

public class UserService : IUserService
{
    public UserService() { }

    public User? Create(string alias, string email, string hashedPassword, Role role)
    {
        using var dbContext = new DatabaseContext();
        dbContext.Database.EnsureCreated();
        var user = new User
        {
            Alias = alias,
            Email = email,
            HashedPassword = hashedPassword,
            Role = role
        };
        dbContext.Users.Add(user);
        dbContext.SaveChanges();
        return dbContext.Users.FirstOrDefault(x => x.Email == email);
    }

    public User? Get(Guid id)
    {
        using var dbContext = new DatabaseContext();
        return dbContext.Users.FirstOrDefault(x => x.Id == id);
    }

    public User? GetByEmail(string email)
    {
        using var dbContext = new DatabaseContext();
        return dbContext.Users.FirstOrDefault(x => x.Email == email);
    }

    public IEnumerable<string>? GetPermissions(Guid userId)
    {
        var user = Get(userId);

        if (user is not null)
        {
            return user.Role?.Permissions.Split(";");
        }

        return [];
    }
}
