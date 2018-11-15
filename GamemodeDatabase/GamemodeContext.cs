using System;
using GamemodeDatabase.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace GamemodeDatabase
{
    public class GamemodeContext : DbContext
    {
        public DbSet<PlayerModel> Players { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql("Server=localhost;Database=gamemode;Uid=root;Pwd=;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<PlayerModel>(entity =>
            {
                entity.HasKey(e => e.Id);

            });
        }
    }
}