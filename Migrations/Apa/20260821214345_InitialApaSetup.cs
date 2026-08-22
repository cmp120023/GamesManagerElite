using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Game_Manager_Elite.Migrations.Apa
{
    /// <inheritdoc />
    public partial class InitialApaSetup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ApaTeams",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TeamName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Format = table.Column<int>(type: "int", nullable: false),
                    DivisionNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    HostLocation = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Session = table.Column<int>(type: "int", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    TotalPoints = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApaTeams", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ApaPlayers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MembershipNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EightBallSkillLevel = table.Column<int>(type: "int", nullable: false),
                    NineBallSkillLevel = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    ApaTeamId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApaPlayers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApaPlayers_ApaTeams_ApaTeamId",
                        column: x => x.ApaTeamId,
                        principalTable: "ApaTeams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "ApaTeamStandings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApaTeamId = table.Column<int>(type: "int", nullable: false),
                    DivisionNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Session = table.Column<int>(type: "int", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    CurrentRank = table.Column<int>(type: "int", nullable: false),
                    TotalPoints = table.Column<int>(type: "int", nullable: false),
                    WeeksPlayed = table.Column<int>(type: "int", nullable: false),
                    MatchesWon = table.Column<int>(type: "int", nullable: false),
                    MatchesLost = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApaTeamStandings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApaTeamStandings_ApaTeams_ApaTeamId",
                        column: x => x.ApaTeamId,
                        principalTable: "ApaTeams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ApaMatches",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MatchDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Format = table.Column<int>(type: "int", nullable: false),
                    HomeTeamId = table.Column<int>(type: "int", nullable: false),
                    AwayTeamId = table.Column<int>(type: "int", nullable: false),
                    HomePlayerId = table.Column<int>(type: "int", nullable: false),
                    HomeSkillLevel = table.Column<int>(type: "int", nullable: false),
                    HomeMatchPoints = table.Column<int>(type: "int", nullable: false),
                    HomeDefensiveShots = table.Column<int>(type: "int", nullable: false),
                    AwayPlayerId = table.Column<int>(type: "int", nullable: false),
                    AwaySkillLevel = table.Column<int>(type: "int", nullable: false),
                    AwayMatchPoints = table.Column<int>(type: "int", nullable: false),
                    AwayDefensiveShots = table.Column<int>(type: "int", nullable: false),
                    IsPlayoff = table.Column<bool>(type: "bit", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApaMatches", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApaMatches_ApaPlayers_AwayPlayerId",
                        column: x => x.AwayPlayerId,
                        principalTable: "ApaPlayers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ApaMatches_ApaPlayers_HomePlayerId",
                        column: x => x.HomePlayerId,
                        principalTable: "ApaPlayers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ApaMatches_ApaTeams_AwayTeamId",
                        column: x => x.AwayTeamId,
                        principalTable: "ApaTeams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ApaMatches_ApaTeams_HomeTeamId",
                        column: x => x.HomeTeamId,
                        principalTable: "ApaTeams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApaMatches_AwayPlayerId",
                table: "ApaMatches",
                column: "AwayPlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_ApaMatches_AwayTeamId",
                table: "ApaMatches",
                column: "AwayTeamId");

            migrationBuilder.CreateIndex(
                name: "IX_ApaMatches_HomePlayerId",
                table: "ApaMatches",
                column: "HomePlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_ApaMatches_HomeTeamId",
                table: "ApaMatches",
                column: "HomeTeamId");

            migrationBuilder.CreateIndex(
                name: "IX_ApaPlayers_ApaTeamId",
                table: "ApaPlayers",
                column: "ApaTeamId");

            migrationBuilder.CreateIndex(
                name: "IX_ApaTeamStandings_ApaTeamId",
                table: "ApaTeamStandings",
                column: "ApaTeamId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApaMatches");

            migrationBuilder.DropTable(
                name: "ApaTeamStandings");

            migrationBuilder.DropTable(
                name: "ApaPlayers");

            migrationBuilder.DropTable(
                name: "ApaTeams");
        }
    }
}
