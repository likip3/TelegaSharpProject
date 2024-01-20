using Microsoft.EntityFrameworkCore;
using TelegaSharpProject.Infrastructure.Models;

namespace TelegaSharpProject.Infrastructure.Data;

public class TelegaSharpProjectContext : DbContext
{
    //public TelegaSharpProjectContext (DbContextOptions<TelegaSharpProjectContext> options)
    //    : base(options)
    //{
    //}

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(
            connectionString: "Server=localhost;Port=5432;User Id=postgres;Password=solver123;Database=solverdb;"
        );
            base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        base.OnModelCreating(modelBuilder);
    }

    public DbSet<User> Users => Set<User>();
    //public DbSet<Discipline> Disciplines => Set<Discipline>();
    //public DbSet<Rating> Ratings => Set<Rating>();
    public DbSet<Work> Works => Set<Work>();
    public DbSet<Comment> Comments => Set<Comment>();
    //public DbSet<Dialog> Dialogs => Set<Dialog>();
}