using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Slush.Migrations
{
    /// <inheritdoc />
    public partial class relationsbundlesfix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_dbGameBundleCollections_dbDLCsInShop_gameId",
                table: "dbGameBundleCollections");

            migrationBuilder.AddColumn<Guid>(
                name: "dlcId",
                table: "dbGameBundleCollections",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.CreateIndex(
                name: "IX_dbGameBundleCollections_dlcId",
                table: "dbGameBundleCollections",
                column: "dlcId");

            migrationBuilder.AddForeignKey(
                name: "FK_dbGameBundleCollections_dbDLCsInShop_dlcId",
                table: "dbGameBundleCollections",
                column: "dlcId",
                principalTable: "dbDLCsInShop",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_dbGameBundleCollections_dbGamesInShops_gameId",
                table: "dbGameBundleCollections",
                column: "gameId",
                principalTable: "dbGamesInShops",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_dbGameBundleCollections_dbDLCsInShop_dlcId",
                table: "dbGameBundleCollections");

            migrationBuilder.DropForeignKey(
                name: "FK_dbGameBundleCollections_dbGamesInShops_gameId",
                table: "dbGameBundleCollections");

            migrationBuilder.DropIndex(
                name: "IX_dbGameBundleCollections_dlcId",
                table: "dbGameBundleCollections");

            migrationBuilder.DropColumn(
                name: "dlcId",
                table: "dbGameBundleCollections");

            migrationBuilder.AddForeignKey(
                name: "FK_dbGameBundleCollections_dbDLCsInShop_gameId",
                table: "dbGameBundleCollections",
                column: "gameId",
                principalTable: "dbDLCsInShop",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
