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
        return user;
    }

    static User? Get(string email)
    {
        using var dbContext = new DatabaseContext();
        return dbContext.Users.FirstOrDefault(x => x.Email == email);
    }
}

public class UserService : IUserService
{
    public UserService() { }
}
