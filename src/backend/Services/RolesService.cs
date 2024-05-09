﻿using OurForum.Backend.Entities;
using OurForum.Backend.Utility;

namespace OurForum.Backend.Services
{
    public interface IRolesService
    {
        static IList<string>? GetPermissions(Guid roleId)
        {
            using var dbContext = new DatabaseContext();
            return dbContext.Roles.FirstOrDefault(x => x.Id == roleId)?.Permissions.Split(";");
        }

        static Role? Get(string name)
        {
            using var db = new DatabaseContext();
            return db.Roles.FirstOrDefault(x => x.Name == name);
        }

        static Role? Create(string name, string permissions, int powerLevel)
        {
            using var dbContext = new DatabaseContext();
            dbContext.Database.EnsureCreated();
            var r = new Role
            {
                Name = name,
                Permissions = permissions,
                PowerLevel = powerLevel
            };
            dbContext.Roles.Add(r);
            dbContext.SaveChanges();
            return dbContext.Roles.FirstOrDefault(x => x.Name == r.Name);
        }
    }

    public class RolesService : IRolesService
    {
        public RolesService() { }
    }
}
