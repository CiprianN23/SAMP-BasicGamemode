﻿using System;
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

                entity.Property(e => e.PositionX)
                    .HasDefaultValue(1685.8075);

                entity.Property(e => e.PositionY)
                    .HasDefaultValue(-2239.2583);

                entity.Property(e => e.PositionZ)
                    .HasDefaultValue(13.5469);

                entity.Property(e => e.FacingAngle)
                    .HasDefaultValue(179.4454);
            });
        }
    }
}