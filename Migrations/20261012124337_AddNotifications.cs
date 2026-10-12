using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Slush.Migrations
{
    /// <inheritdoc />
    public partial class AddNotifications : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "dbNotifications",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    userId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    type = table.Column<int>(type: "int", nullable: false),
                    actorId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    isRead = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    createdAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    deletedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbNotifications", x => x.id);
                    table.ForeignKey(
                        name: "FK_dbNotifications_dbUsers_actorId",
                        column: x => x.actorId,
                        principalTable: "dbUsers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_dbNotifications_dbUsers_userId",
                        column: x => x.userId,
                        principalTable: "dbUsers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_dbNotifications_actorId",
                table: "dbNotifications",
                column: "actorId");

            migrationBuilder.CreateIndex(
                name: "IX_dbNotifications_userId",
                table: "dbNotifications",
                column: "userId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "dbNotifications");
        }
    }
}
