using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Slush.Migrations
{
    /// <inheritdoc />
    public partial class fixgamedlc : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "description",
                table: "dbGamesInShops",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<DateTime>(
                name: "discountFinish",
                table: "dbGamesInShops",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "description",
                table: "dbDLCsInShop",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<DateTime>(
                name: "discountFinish",
                table: "dbDLCsInShop",
                type: "datetime(6)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "description",
                table: "dbGamesInShops");

            migrationBuilder.DropColumn(
                name: "discountFinish",
                table: "dbGamesInShops");

            migrationBuilder.DropColumn(
                name: "description",
                table: "dbDLCsInShop");

            migrationBuilder.DropColumn(
                name: "discountFinish",
                table: "dbDLCsInShop");
        }
    }
}
