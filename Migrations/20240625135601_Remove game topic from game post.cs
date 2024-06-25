using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Slush.Migrations
{
    /// <inheritdoc />
    public partial class Removegametopicfromgamepost : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_dbGamePosts_dbGameTopics_gameTopicId",
                table: "dbGamePosts");

            migrationBuilder.DropIndex(
                name: "IX_dbGamePosts_gameTopicId",
                table: "dbGamePosts");

            migrationBuilder.DropColumn(
                name: "gameTopicId",
                table: "dbGamePosts");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "gameTopicId",
                table: "dbGamePosts",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.CreateIndex(
                name: "IX_dbGamePosts_gameTopicId",
                table: "dbGamePosts",
                column: "gameTopicId");

            migrationBuilder.AddForeignKey(
                name: "FK_dbGamePosts_dbGameTopics_gameTopicId",
                table: "dbGamePosts",
                column: "gameTopicId",
                principalTable: "dbGameTopics",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
