using Game_Manager_Elite.Models;
using Microsoft.EntityFrameworkCore;

namespace Game_Manager_Elite.Models
{
    /// <summary>
    /// Database context managing the SQL Server persistence for the APA league.
    /// </summary>
    public class ApaContext : DbContext
    {
        public ApaContext(DbContextOptions<ApaContext> options) : base(options)
        {
        }

        //creating communication line from Entitry Framework, to the corresponding tables in the database, to be used later on.
        public DbSet<ApaTeam> ApaTeams { get; set; } = null!;
        public DbSet<ApaPlayer> ApaPlayers { get; set; } = null!;
        public DbSet<ApaMatch> ApaMatches { get; set; } = null!;
        public DbSet<ApaPlayerMatch> ApaPlayerMatches { get; set; } = null!;
        public DbSet<ApaTeamStanding> ApaTeamStandings { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApaPlayer>()
                .HasOne(p => p.Team)
                .WithMany(t => t.Roster)
                .HasForeignKey(p => p.ApaTeamId)
                .OnDelete(DeleteBehavior.SetNull);//players can exist without teams. "floating" until a home is found. cannot play in matches





            //builds relationship rule inside the Apa database to determine how
            //a match connects to the team roster when acting as the home team.
            modelBuilder.Entity<ApaMatch>()//method that reads the ApaMatch class, and determines how it relates to the database
                .HasOne(m => m.HomeTeam)// there can only be one home team.
                .WithMany(t => t.HomeMatches)// a team can play as the home team many times in a session
                .HasForeignKey(m => m.HomeTeamId)//this defines the relationship between the match and the home team via the teams fk
                .OnDelete(DeleteBehavior.Restrict);// this throws a SQL exception error, if a team is deleted, preventing it from deleting everything it touches. I.E. Circular Cascade Delete

            //build the away team relationship to the match.
            modelBuilder.Entity<ApaMatch>()
                .HasOne(m => m.AwayTeam)
                .WithMany(t => t.AwayMatches)
                .HasForeignKey(m => m.AwayTeamId)
                .OnDelete(DeleteBehavior.Restrict);

            // This is defining the relationship between a player game, and the teams match. 
            modelBuilder.Entity<ApaPlayerMatch>()
                .HasOne(pm => pm.ParentMatch)
                .WithMany(m => m.PlayerMatches)
                .HasForeignKey(pm => pm.ApaMatchId)
                .OnDelete(DeleteBehavior.Cascade);

            // This is defining the relationship between the home player and the match.
            modelBuilder.Entity<ApaPlayerMatch>()
                .HasOne(pm => pm.HomePlayer)
                .WithMany()
                .HasForeignKey(pm => pm.HomePlayerId)
                .OnDelete(DeleteBehavior.Restrict);

            // this is defining the relationship between the away player and the match.
            modelBuilder.Entity<ApaPlayerMatch>()
                .HasOne(pm => pm.AwayPlayer)
                .WithMany()
                .HasForeignKey(pm => pm.AwayPlayerId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
