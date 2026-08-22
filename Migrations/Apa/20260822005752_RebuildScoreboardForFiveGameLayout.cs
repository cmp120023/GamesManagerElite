using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Game_Manager_Elite.Migrations.Apa
{
    /// <inheritdoc />
    public partial class RebuildScoreboardForFiveGameLayout : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApaMatches_ApaPlayers_AwayPlayerId",
                table: "ApaMatches");

            migrationBuilder.DropForeignKey(
                name: "FK_ApaMatches_ApaPlayers_HomePlayerId",
                table: "ApaMatches");

            migrationBuilder.DropIndex(
                name: "IX_ApaMatches_AwayPlayerId",
                table: "ApaMatches");

            migrationBuilder.DropIndex(
                name: "IX_ApaMatches_HomePlayerId",
                table: "ApaMatches");

            migrationBuilder.DropColumn(
                name: "AwayDefensiveShots",
                table: "ApaMatches");

            migrationBuilder.DropColumn(
                name: "AwayPlayerId",
                table: "ApaMatches");

            migrationBuilder.DropColumn(
                name: "AwaySkillLevel",
                table: "ApaMatches");

            migrationBuilder.DropColumn(
                name: "HomeDefensiveShots",
                table: "ApaMatches");

            migrationBuilder.DropColumn(
                name: "HomePlayerId",
                table: "ApaMatches");

            migrationBuilder.DropColumn(
                name: "HomeSkillLevel",
                table: "ApaMatches");

            migrationBuilder.CreateTable(
                name: "ApaPlayerMatches",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApaMatchId = table.Column<int>(type: "int", nullable: false),
                    GameNumber = table.Column<int>(type: "int", nullable: false),
                    HomePlayerId = table.Column<int>(type: "int", nullable: false),
                    HomeSkillLevel = table.Column<int>(type: "int", nullable: false),
                    HomePointsEarned = table.Column<int>(type: "int", nullable: false),
                    HomeDefensiveShots = table.Column<int>(type: "int", nullable: false),
                    AwayPlayerId = table.Column<int>(type: "int", nullable: false),
                    AwaySkillLevel = table.Column<int>(type: "int", nullable: false),
                    AwayPointsEarned = table.Column<int>(type: "int", nullable: false),
                    AwayDefensiveShots = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApaPlayerMatches", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApaPlayerMatches_ApaMatches_ApaMatchId",
                        column: x => x.ApaMatchId,
                        principalTable: "ApaMatches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApaPlayerMatches_ApaPlayers_AwayPlayerId",
                        column: x => x.AwayPlayerId,
                        principalTable: "ApaPlayers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ApaPlayerMatches_ApaPlayers_HomePlayerId",
                        column: x => x.HomePlayerId,
                        principalTable: "ApaPlayers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApaPlayerMatches_ApaMatchId",
                table: "ApaPlayerMatches",
                column: "ApaMatchId");

            migrationBuilder.CreateIndex(
                name: "IX_ApaPlayerMatches_AwayPlayerId",
                table: "ApaPlayerMatches",
                column: "AwayPlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_ApaPlayerMatches_HomePlayerId",
                table: "ApaPlayerMatches",
                column: "HomePlayerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApaPlayerMatches");

            migrationBuilder.AddColumn<int>(
                name: "AwayDefensiveShots",
                table: "ApaMatches",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AwayPlayerId",
                table: "ApaMatches",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AwaySkillLevel",
                table: "ApaMatches",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "HomeDefensiveShots",
                table: "ApaMatches",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "HomePlayerId",
                table: "ApaMatches",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "HomeSkillLevel",
                table: "ApaMatches",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ApaMatches_AwayPlayerId",
                table: "ApaMatches",
                column: "AwayPlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_ApaMatches_HomePlayerId",
                table: "ApaMatches",
                column: "HomePlayerId");

            migrationBuilder.AddForeignKey(
                name: "FK_ApaMatches_ApaPlayers_AwayPlayerId",
                table: "ApaMatches",
                column: "AwayPlayerId",
                principalTable: "ApaPlayers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ApaMatches_ApaPlayers_HomePlayerId",
                table: "ApaMatches",
                column: "HomePlayerId",
                principalTable: "ApaPlayers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
