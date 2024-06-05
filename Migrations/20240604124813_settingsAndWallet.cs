using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Slush.Migrations
{
    /// <inheritdoc />
    public partial class settingsAndWallet : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "description",
                table: "dbUsers",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "dbGameBundleCollections",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    gameId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    bundleId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    createdAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    deletedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbGameBundleCollections", x => x.id);
                    table.ForeignKey(
                        name: "FK_dbGameBundleCollections_dbDLCsInShop_id",
                        column: x => x.id,
                        principalTable: "dbDLCsInShop",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_dbGameBundleCollections_dbGamesInShops_id",
                        column: x => x.id,
                        principalTable: "dbGamesInShops",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "dbGameBundles",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    description = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    price = table.Column<float>(type: "float", nullable: false),
                    discount = table.Column<float>(type: "float", nullable: false),
                    createdAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    deletedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbGameBundles", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "dbSettings",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    attachedUserId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    bigSaleNotification = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    saleFromWishlistNotification = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    newCommentNotification = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    friendRequestNotification = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    approvedFriendRequest = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    declinedFriendRequest = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    createdAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    deletedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbSettings", x => x.id);
                    table.ForeignKey(
                        name: "FK_dbSettings_dbUsers_attachedUserId",
                        column: x => x.attachedUserId,
                        principalTable: "dbUsers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "dbWalletTransactions",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    userId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    transactionObj = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    currency = table.Column<float>(type: "float", nullable: false),
                    createdAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    deletedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbWalletTransactions", x => x.id);
                    table.ForeignKey(
                        name: "FK_dbWalletTransactions_dbDLCsInShop_transactionObj",
                        column: x => x.transactionObj,
                        principalTable: "dbDLCsInShop",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_dbWalletTransactions_dbGamesInShops_transactionObj",
                        column: x => x.transactionObj,
                        principalTable: "dbGamesInShops",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_dbWalletTransactions_dbUsers_userId",
                        column: x => x.userId,
                        principalTable: "dbUsers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_dbSettings_attachedUserId",
                table: "dbSettings",
                column: "attachedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_dbWalletTransactions_transactionObj",
                table: "dbWalletTransactions",
                column: "transactionObj");

            migrationBuilder.CreateIndex(
                name: "IX_dbWalletTransactions_userId",
                table: "dbWalletTransactions",
                column: "userId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "dbGameBundleCollections");

            migrationBuilder.DropTable(
                name: "dbGameBundles");

            migrationBuilder.DropTable(
                name: "dbSettings");

            migrationBuilder.DropTable(
                name: "dbWalletTransactions");

            migrationBuilder.DropColumn(
                name: "description",
                table: "dbUsers");
        }
    }
}
