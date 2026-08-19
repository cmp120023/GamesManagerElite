using Microsoft.EntityFrameworkCore;
using Game_Manager_Elite.Models;

namespace Game_Manager_Elite.Models
{
    /// <summary>
    /// Database context managing the SQL Server persistence for the league entities.
    /// </summary>
    public class LeagueContext : DbContext
    {
        public LeagueContext(DbContextOptions<LeagueContext> options) : base(options)
        {
        }

        public DbSet<BcaTeam> BcaTeams { get; set; } = null!;
        public DbSet<BcaPlayer> BcaPlayers { get; set; } = null!;
        public DbSet<BcaMatch> BcaMatches { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure Home Team relationship to prevent circular cascade delete
            modelBuilder.Entity<BcaMatch>()
                .HasOne(m => m.HomeTeam)
                .WithMany(t => t.HomeMatches)
                .HasForeignKey(m => m.HomeTeamId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure Away Team relationship to prevent circular cascade delete
            modelBuilder.Entity<BcaMatch>()
                .HasOne(m => m.AwayTeam)
                .WithMany(t => t.AwayMatches)
                .HasForeignKey(m => m.AwayTeamId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}