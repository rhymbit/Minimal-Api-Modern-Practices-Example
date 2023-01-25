using Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Database;

public class DbContext : Microsoft.EntityFrameworkCore.DbContext
{
    public DbSet<User> Users { get; }

    public string DbPath { get; }

    public DbContext() =>
        DbPath = Path.Combine(Environment.CurrentDirectory, "database.db");

    public DbContext(DbContextOptions<DbContext> options) :
        base(options) =>
        DbPath = Path.Combine(Environment.CurrentDirectory, "database.db");

    protected override void OnConfiguring(DbContextOptionsBuilder options) =>
      options.UseSqlite($"Data Source={DbPath}");

    protected override void OnModelCreating(ModelBuilder modelBuilder) =>
      modelBuilder.Entity<User>().ToTable("Users")
        .HasKey(u => u.Id)
        .HasName("PK_Users");
}
