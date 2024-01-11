using Microsoft.EntityFrameworkCore;
using TelegaSharpProject.Infrastructure.Models;

namespace TelegaSharpProject.Infrastructure.Data;

public class TelegaSharpProjectContext : DbContext
{
    public TelegaSharpProjectContext (DbContextOptions<TelegaSharpProjectContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users => Set<User>();
    public DbSet<Discipline> Disciplines => Set<Discipline>();
    public DbSet<Rating> Ratings => Set<Rating>();
    public DbSet<Work> Works => Set<Work>();
    public DbSet<Message> Messages => Set<Message>();
    public DbSet<Dialog> Dialogs => Set<Dialog>();
}