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

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
        optionsBuilder.UseSqlServer(EnvironmentVariables.SQL_CONNECTIONSTRING);

    protected override void OnModelCreating(ModelBuilder modelBuilder) =>
        modelBuilder.ApplySoftDeleteQueryFilter();
}
