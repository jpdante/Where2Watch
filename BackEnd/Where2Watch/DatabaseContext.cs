using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Where2Watch.Models;

namespace Where2Watch {
    public class DatabaseContext : DbContext {

        private static string _connectionString = "server=127.0.0.1;port=3306;database=w2w;user=root;password=root";

        public DbSet<Title> Titles { get; set; }
        public DbSet<TitleName> TitleNames { get; set; }

        public static void SetConnectionString(string connectionString) {
            _connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            optionsBuilder.UseMySql(_connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);


        }
    }
}
