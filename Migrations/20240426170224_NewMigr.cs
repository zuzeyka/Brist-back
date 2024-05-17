using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Slush.Migrations
{
    /// <inheritdoc />
    public partial class NewMigr : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_dbLanguagesInGame_dbGamesInShops_GameInShopid",
                table: "dbLanguagesInGame");

            migrationBuilder.DropIndex(
                name: "IX_dbLanguagesInGame_GameInShopid",
                table: "dbLanguagesInGame");

            migrationBuilder.DropColumn(
                name: "GameInShopid",
                table: "dbLanguagesInGame");

            migrationBuilder.DropColumn(
                name: "categoriesId",
                table: "dbGamesInShops");

            migrationBuilder.DropColumn(
                name: "gameImages",
                table: "dbGamesInShops");

            migrationBuilder.DropColumn(
                name: "languagesId",
                table: "dbGamesInShops");

            migrationBuilder.DropColumn(
                name: "systemRequirementsId",
                table: "dbGamesInShops");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "GameInShopid",
                table: "dbLanguagesInGame",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<string>(
                name: "categoriesId",
                table: "dbGamesInShops",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "gameImages",
                table: "dbGamesInShops",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "languagesId",
                table: "dbGamesInShops",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "systemRequirementsId",
                table: "dbGamesInShops",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_dbLanguagesInGame_GameInShopid",
                table: "dbLanguagesInGame",
                column: "GameInShopid");

            migrationBuilder.AddForeignKey(
                name: "FK_dbLanguagesInGame_dbGamesInShops_GameInShopid",
                table: "dbLanguagesInGame",
                column: "GameInShopid",
                principalTable: "dbGamesInShops",
                principalColumn: "id");
        }
    }
}
