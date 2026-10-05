using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Slush.Migrations
{
    /// <inheritdoc />
    public partial class AddGameEventsAndCategoryKind : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "kind",
                table: "dbCategories",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "dbGameEvents",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    name = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    description = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    startAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    endAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    createdAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    deleteAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbGameEvents", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "dbGameEventsForGame",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    gameId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    eventId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    createdAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    deleteAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbGameEventsForGame", x => x.id);
                    table.ForeignKey(
                        name: "FK_dbGameEventsForGame_dbGameEvents_eventId",
                        column: x => x.eventId,
                        principalTable: "dbGameEvents",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_dbGameEventsForGame_dbGamesInShops_gameId",
                        column: x => x.gameId,
                        principalTable: "dbGamesInShops",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_dbGameEventsForGame_eventId",
                table: "dbGameEventsForGame",
                column: "eventId");

            migrationBuilder.CreateIndex(
                name: "IX_dbGameEventsForGame_gameId",
                table: "dbGameEventsForGame",
                column: "gameId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "dbGameEventsForGame");

            migrationBuilder.DropTable(
                name: "dbGameEvents");

            migrationBuilder.DropColumn(
                name: "kind",
                table: "dbCategories");
        }
    }
}
