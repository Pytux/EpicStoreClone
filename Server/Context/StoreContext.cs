using Entities;
using Microsoft.EntityFrameworkCore;

namespace Server.Context;

public class StoreContext : DbContext
{
    public StoreContext(DbContextOptions<StoreContext> options) : base(options)
    {
    }

    public DbSet<Game> Games { get; set; }
    public DbSet<News> News { get; set; }
    public DbSet<UserProfile> UserProfiles { get; set; }
    public DbSet<UserGames> UserGames { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}