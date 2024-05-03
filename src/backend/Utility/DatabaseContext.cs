﻿using Microsoft.EntityFrameworkCore;
using OurForum.Backend.Entities;

namespace OurForum.Backend.Utility;

public class DatabaseContext : DbContext
{
    public DbSet<Role> Roles { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
        optionsBuilder.UseMySQL(EnvironmentVariables.MYSQL_CONNECTIONSTRING);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Board>(e =>
        {
            e.HasKey(x => x.Id);
        });

        modelBuilder.Entity<Post>(e =>
        {
            e.HasKey(x => x.Id);
        });

        modelBuilder.Entity<Role>(e =>
        {
            e.HasKey(x => x.Id);
        });

        modelBuilder.Entity<User>(e =>
        {
            e.HasKey(x => x.Id);
            e.Property(x => x.Alias).IsRequired();
            e.Property(x => x.Email).IsRequired();
            e.Property(x => x.EncryptedPassword).IsRequired();
            e.HasOne(x => x.Role).WithMany(x => x.Users);
        });
    }
}