using Microsoft.EntityFrameworkCore;
using EventEase.Server.Models;

namespace EventEase.Server.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Event> Events => Set<Event>();
    public DbSet<Registration> Registrations => Set<Registration>();

    public static void Seed(AppDbContext ctx)
    {
        if (ctx.Events.Any()) return;

        ctx.Events.AddRange(
            new Event { Title = "Blazor Workshop", Description = "Learn Blazor basics.", Date = DateTime.Now.AddDays(7), Location = "Online", Capacity = 50 },
            new Event { Title = "DotNet Conference", Description = "Annual .NET conf.", Date = DateTime.Now.AddDays(30), Location = "Bangkok", Capacity = 200 },
            new Event { Title = "Community Meetup", Description = "Local dev meetup.", Date = DateTime.Now.AddDays(14), Location = "Cafe", Capacity = 30 }
        );
        ctx.SaveChanges();
    }
}
