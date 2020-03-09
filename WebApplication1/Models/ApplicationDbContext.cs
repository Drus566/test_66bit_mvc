using Microsoft.EntityFrameworkCore;
using System;

namespace WebApplication1.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Player> Players { get; set; }
        public DbSet<Team> Teams { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Player>()
                .Property(p => p.Gender)
                .HasConversion(
                    v => v.ToString(),
                    v => (Gender)Enum.Parse(typeof(Gender), v));

            modelBuilder
                .Entity<Player>()
                .Property(p => p.Country)
                .HasConversion(
                    v => v.ToString(),
                    v => (Country)Enum.Parse(typeof(Country), v));
        }
    }
}