using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Slush.Migrations
{
    /// <inheritdoc />
    public partial class relationsbundles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_dbGameBundleCollections_dbDLCsInShop_id",
                table: "dbGameBundleCollections");

            migrationBuilder.DropForeignKey(
                name: "FK_dbGameBundleCollections_dbGamesInShops_id",
                table: "dbGameBundleCollections");

            migrationBuilder.CreateIndex(
                name: "IX_dbGameBundleCollections_bundleId",
                table: "dbGameBundleCollections",
                column: "bundleId");

            migrationBuilder.CreateIndex(
                name: "IX_dbGameBundleCollections_gameId",
                table: "dbGameBundleCollections",
                column: "gameId");

            migrationBuilder.AddForeignKey(
                name: "FK_dbGameBundleCollections_dbDLCsInShop_gameId",
                table: "dbGameBundleCollections",
                column: "gameId",
                principalTable: "dbDLCsInShop",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_dbGameBundleCollections_dbGameBundles_bundleId",
                table: "dbGameBundleCollections",
                column: "bundleId",
                principalTable: "dbGameBundles",
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
                name: "FK_dbGameBundleCollections_dbDLCsInShop_gameId",
                table: "dbGameBundleCollections");

            migrationBuilder.DropForeignKey(
                name: "FK_dbGameBundleCollections_dbGameBundles_bundleId",
                table: "dbGameBundleCollections");

            migrationBuilder.DropForeignKey(
                name: "FK_dbGameBundleCollections_dbGamesInShops_gameId",
                table: "dbGameBundleCollections");

            migrationBuilder.DropIndex(
                name: "IX_dbGameBundleCollections_bundleId",
                table: "dbGameBundleCollections");

            migrationBuilder.DropIndex(
                name: "IX_dbGameBundleCollections_gameId",
                table: "dbGameBundleCollections");

            migrationBuilder.AddForeignKey(
                name: "FK_dbGameBundleCollections_dbDLCsInShop_id",
                table: "dbGameBundleCollections",
                column: "id",
                principalTable: "dbDLCsInShop",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_dbGameBundleCollections_dbGamesInShops_id",
                table: "dbGameBundleCollections",
                column: "id",
                principalTable: "dbGamesInShops",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
