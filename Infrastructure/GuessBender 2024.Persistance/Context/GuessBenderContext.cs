// AppDbContext.cs


using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using GuessBender_2024.Domain.Entities;
using System.Diagnostics.Metrics;
namespace GuessBender_2024.Persistance.Context
{
    public class GuessBenderContext : IdentityDbContext<ApplicationUser>
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=N1NWPLSK12SQL-v01.shr.prod.ams1.secureserver.net;Database=guessbender;User=guessbender_admin_database;Password=6K2tgi60?;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=True;Integrated security=false");
        }
        public DbSet<Match> Match { get; set; } = default!;
        public DbSet<Team> Team { get; set; } = default!;
        public DbSet<Prediction> Prediction { get; set; } = default!;
        public DbSet<League> League { get; set; } = default!;
        public DbSet<Country> Country { get; set; } = default!;
        public DbSet<LeagueData> LeagueData { get; set; } = default!;
        public DbSet<ApplicationUser> User { get; set; } = default!;
        public DbSet<Friend> Friend { get; set; } = default!;

    }
}