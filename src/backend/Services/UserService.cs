﻿using OurForum.Backend.Entities;
using OurForum.Backend.Utility;

namespace OurForum.Backend.Services;

public interface IUserService
{
    public User? Create(string alias, string email, string hashedPassword, Role? role);

    public User? Get(Guid id);

    public User? GetByEmail(string email);

    public IEnumerable<string>? GetPermissions(Guid userId);
}

public class UserService(DatabaseContext context) : IUserService
{
    private readonly DatabaseContext _context = context;

    public User? Create(string alias, string email, string hashedPassword, Role role)
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
        _context.SaveChanges();

        return _context.Users.FirstOrDefault(x => x.Email == email);
    }

    public User? Get(Guid id)
    {
        return _context.Users.FirstOrDefault(x => x.Id == id);
    }

    public User? GetByEmail(string email)
    {
        return _context.Users.FirstOrDefault(x => x.Email == email);
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
