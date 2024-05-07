using OurForum.Backend.Entities;
using OurForum.Backend.Utility;

namespace OurForum.Backend.Services;

public interface IUserService
{
    static User? Create(string email, string hashedPassword)
    {
        using var dbContext = new DatabaseContext();
        dbContext.Database.EnsureCreated();
        var user = new User { Email = email, HashedPassword = hashedPassword };
        dbContext.Users.Add(user);
        dbContext.SaveChanges();
        return user;
    }

    static User? Get(Guid id)
    {
        using var dbContext = new DatabaseContext();
        return dbContext.Users.FirstOrDefault(x => x.Id == id);
    }

    static User? GetByEmail(string email)
    {
        using var dbContext = new DatabaseContext();
        return dbContext.Users.FirstOrDefault(x => x.Email == email);
    }

    static IEnumerable<string>? GetPermissions(Guid userId)
    {
        var user = Get(userId);

        if (user is not null)
        {
            return user.Role?.Permissions.Split(";");
        }

        return [];
    }
}

public class UserService : IUserService
{
    public UserService() { }
}
