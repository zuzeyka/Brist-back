using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Slush.Migrations
{
    /// <inheritdoc />
    public partial class relationsbundlesашч : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_dbGameBundleCollections_dbGamesInShops_gameId",
                table: "dbGameBundleCollections");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddForeignKey(
                name: "FK_dbGameBundleCollections_dbGamesInShops_gameId",
                table: "dbGameBundleCollections",
                column: "gameId",
                principalTable: "dbGamesInShops",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
