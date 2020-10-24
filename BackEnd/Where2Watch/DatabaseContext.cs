using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Where2Watch.Models;

namespace Where2Watch {
    public class DatabaseContext : DbContext {

        private static string _connectionString = "server=127.0.0.1;port=3306;database=w2w;user=root;password=root";

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Title> Titles { get; set; }
        public DbSet<TitleName> TitleNames { get; set; }
        public DbSet<TitleLike> TitleLikes { get; set; }
        public DbSet<TitleAvailability> TitleAvailabilities { get; set; }
        public DbSet<Platform> Platforms { get; set; }
        public DbSet<Season> Seasons { get; set; }
        public DbSet<Episode> Episodes { get; set; }
        public DbSet<EpisodeName> EpisodeNames { get; set; }

        public static void SetConnectionString(string connectionString) {
            _connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            optionsBuilder.UseMySql(_connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Account>(entity => {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedNever();
                entity.HasIndex(e => e.Guid).IsUnique();
                entity.HasIndex(e => e.Email).IsUnique();
                entity.HasIndex(e => e.Username).IsUnique();
            });

            modelBuilder.Entity<Title>(entity => {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedNever();
                entity.HasIndex(e => e.IMDbId);
            });

            modelBuilder.Entity<TitleName>(entity => {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedNever();
                entity.HasIndex(e => e.TitleId);
                entity.HasIndex(e => e.Country);
            });

            modelBuilder.Entity<TitleLike>(entity => {
                entity.HasKey(e => new { e.AccountId, e.TitleId });
                entity.HasIndex(e => e.AccountId);
                entity.HasIndex(e => e.TitleId);
            });

            modelBuilder.Entity<TitleAvailability>(entity => {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedNever();
                entity.HasIndex(e => e.TitleId);
                entity.HasIndex(e => e.Country);
            });

            modelBuilder.Entity<Platform>(entity => {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedNever();
                entity.HasIndex(e => e.Country);
            });

            modelBuilder.Entity<Season>(entity => {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedNever();
                entity.HasIndex(e => e.TitleId);
            });

            modelBuilder.Entity<Episode>(entity => {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedNever();
                entity.HasIndex(e => e.TitleId);
                entity.HasIndex(e => e.SeasonId);
                entity.HasIndex(e => e.IMDbId);
            });

            modelBuilder.Entity<EpisodeName>(entity => {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedNever();
                entity.HasIndex(e => e.EpisodeId);
                entity.HasIndex(e => e.Country);
            });
        }
    }
}
