using GamemodeDatabase.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace GamemodeDatabase;

public class GamemodeContext : DbContext
{
    // Comment to be able to run EF Core migrations
    public GamemodeContext(DbContextOptions<GamemodeContext> options) : base(options)
    {
    }

    public DbSet<PlayerModel> Players { get; set; }

    // Only uncomment to able to run EF Core migrations
    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //{
    //    var databaseServerVersion = new MariaDbServerVersion("10.5");
    //    optionsBuilder.UseMySql("connectionString", databaseServerVersion);
    //}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<PlayerModel>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.PositionX)
                .HasDefaultValue(1685.8075);

            entity.Property(e => e.PositionY)
                .HasDefaultValue(-2239.2583);

            entity.Property(e => e.PositionZ)
                .HasDefaultValue(13.5469);

            entity.Property(e => e.FacingAngle)
                .HasDefaultValue(179.4454);

            entity.Property(e => e.JoinDate)
                .HasDefaultValue(DateTime.Now);
        });
    }
}
