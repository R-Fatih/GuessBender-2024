// AppDbContext.cs


using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using GuessBender_2024.Domain.Entities;
using System.Diagnostics.Metrics;
namespace GuessBender_2024.Persistance.Context
{
    public class GuessBenderContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("...");
        }
        public DbSet<Match> Match { get; set; } = default!;
        public DbSet<Team> Team { get; set; } = default!;
        public DbSet<Prediction> Prediction { get; set; } = default!;
        public DbSet<League> League { get; set; } = default!;
        public DbSet<Country> Country { get; set; } = default!;
        public DbSet<User> Users { get; set; } = default!;
        public DbSet<Role> Roles { get; set; } = default!;
        public DbSet<UserRole> UserRoles { get; set; } = default!;
        public DbSet<Friend> Friend { get; set; } = default!;

    }
}