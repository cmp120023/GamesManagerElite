using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Game_Manager_Elite.Migrations
{
    /// <inheritdoc />
    public partial class AddBcaEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BcaTeams",
                columns: table => new
                {
                    TeamId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TeamName = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    Division = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BcaTeams", x => x.TeamId);
                });

            migrationBuilder.CreateTable(
                name: "BcaMatches",
                columns: table => new
                {
                    MatchId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MatchDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HomeTeamId = table.Column<int>(type: "int", nullable: false),
                    AwayTeamId = table.Column<int>(type: "int", nullable: false),
                    HomeTeamScore = table.Column<int>(type: "int", nullable: false),
                    AwayTeamScore = table.Column<int>(type: "int", nullable: false),
                    IsFinalized = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BcaMatches", x => x.MatchId);
                    table.ForeignKey(
                        name: "FK_BcaMatches_BcaTeams_AwayTeamId",
                        column: x => x.AwayTeamId,
                        principalTable: "BcaTeams",
                        principalColumn: "TeamId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BcaMatches_BcaTeams_HomeTeamId",
                        column: x => x.HomeTeamId,
                        principalTable: "BcaTeams",
                        principalColumn: "TeamId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BcaPlayers",
                columns: table => new
                {
                    PlayerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HandicapRating = table.Column<int>(type: "int", nullable: false),
                    TeamId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BcaPlayers", x => x.PlayerId);
                    table.ForeignKey(
                        name: "FK_BcaPlayers_BcaTeams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "BcaTeams",
                        principalColumn: "TeamId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_BcaMatches_AwayTeamId",
                table: "BcaMatches",
                column: "AwayTeamId");

            migrationBuilder.CreateIndex(
                name: "IX_BcaMatches_HomeTeamId",
                table: "BcaMatches",
                column: "HomeTeamId");

            migrationBuilder.CreateIndex(
                name: "IX_BcaPlayers_TeamId",
                table: "BcaPlayers",
                column: "TeamId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BcaMatches");

            migrationBuilder.DropTable(
                name: "BcaPlayers");

            migrationBuilder.DropTable(
                name: "BcaTeams");
        }
    }
}
