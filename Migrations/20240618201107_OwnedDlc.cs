using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Slush.Migrations
{
    /// <inheritdoc />
    public partial class OwnedDlc : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "dbOwnedDlcs",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    ownedDlcId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    userId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    createdAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    deleteAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbOwnedDlcs", x => x.id);
                    table.ForeignKey(
                        name: "FK_dbOwnedDlcs_dbDLCsInShop_ownedDlcId",
                        column: x => x.ownedDlcId,
                        principalTable: "dbDLCsInShop",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_dbOwnedDlcs_dbUsers_userId",
                        column: x => x.userId,
                        principalTable: "dbUsers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_dbOwnedDlcs_ownedDlcId",
                table: "dbOwnedDlcs",
                column: "ownedDlcId");

            migrationBuilder.CreateIndex(
                name: "IX_dbOwnedDlcs_userId",
                table: "dbOwnedDlcs",
                column: "userId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "dbOwnedDlcs");
        }
    }
}
