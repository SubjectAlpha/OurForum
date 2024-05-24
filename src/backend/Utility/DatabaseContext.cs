using Microsoft.EntityFrameworkCore;
using OurForum.Backend.Entities;
using OurForum.Backend.Extensions;

namespace OurForum.Backend.Utility;

public class DatabaseContext : DbContext
{
    public DbSet<Board> Boards { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        switch (EnvironmentVariables.SQL_CONNECTOR.ToLowerInvariant())
        {
            case "mssql":
                optionsBuilder.UseSqlServer(EnvironmentVariables.SQL_CONNECTIONSTRING);
                break;
            case "mysql":
            default:
                optionsBuilder.UseMySQL(EnvironmentVariables.SQL_CONNECTIONSTRING);
                break;
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder) =>
        modelBuilder.ApplySoftDeleteQueryFilter();
}
