using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Slush.Migrations
{
    /// <inheritdoc />
    public partial class achievements : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_dbGameGroups_dbGamesInShops_gameId",
                table: "dbGameGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_dbGameGuides_dbGameGroups_gameGroupId",
                table: "dbGameGuides");

            migrationBuilder.DropForeignKey(
                name: "FK_dbGameGuides_dbGamesInShops_gameId",
                table: "dbGameGuides");

            migrationBuilder.DropForeignKey(
                name: "FK_dbGameNews_dbGameGroups_gameGroupId",
                table: "dbGameNews");

            migrationBuilder.DropForeignKey(
                name: "FK_dbMessages_dbChats_chatId",
                table: "dbMessages");

            migrationBuilder.DropForeignKey(
                name: "FK_dbMessages_dbUsers_senderId",
                table: "dbMessages");

            migrationBuilder.AlterColumn<DateTime>(
                name: "createdAt",
                table: "dbMessages",
                type: "datetime(6)",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "createdAt",
                table: "dbChats",
                type: "datetime(6)",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "createdAt",
                table: "dbCategoryByUserForGames",
                type: "datetime(6)",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)");

            migrationBuilder.CreateTable(
                name: "dbAchievementByUser",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    userId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    achievementId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    createdAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    deletedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbAchievementByUser", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "dbAchievements",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    urlForImage = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    description = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    amountOfExperience = table.Column<int>(type: "int", nullable: false),
                    awardTime = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    createdAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    deletedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbAchievements", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddForeignKey(
                name: "FK_dbGameGroups_dbGamesInShops_gameId",
                table: "dbGameGroups",
                column: "gameId",
                principalTable: "dbGamesInShops",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_dbGameGuides_dbGameGroups_gameGroupId",
                table: "dbGameGuides",
                column: "gameGroupId",
                principalTable: "dbGameGroups",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_dbGameGuides_dbGamesInShops_gameId",
                table: "dbGameGuides",
                column: "gameId",
                principalTable: "dbGamesInShops",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_dbGameNews_dbGameGroups_gameGroupId",
                table: "dbGameNews",
                column: "gameGroupId",
                principalTable: "dbGameGroups",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_dbMessages_dbChats_chatId",
                table: "dbMessages",
                column: "chatId",
                principalTable: "dbChats",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_dbMessages_dbUsers_senderId",
                table: "dbMessages",
                column: "senderId",
                principalTable: "dbUsers",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_dbGameGroups_dbGamesInShops_gameId",
                table: "dbGameGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_dbGameGuides_dbGameGroups_gameGroupId",
                table: "dbGameGuides");

            migrationBuilder.DropForeignKey(
                name: "FK_dbGameGuides_dbGamesInShops_gameId",
                table: "dbGameGuides");

            migrationBuilder.DropForeignKey(
                name: "FK_dbGameNews_dbGameGroups_gameGroupId",
                table: "dbGameNews");

            migrationBuilder.DropForeignKey(
                name: "FK_dbMessages_dbChats_chatId",
                table: "dbMessages");

            migrationBuilder.DropForeignKey(
                name: "FK_dbMessages_dbUsers_senderId",
                table: "dbMessages");

            migrationBuilder.DropTable(
                name: "dbAchievementByUser");

            migrationBuilder.DropTable(
                name: "dbAchievements");

            migrationBuilder.AlterColumn<DateTime>(
                name: "createdAt",
                table: "dbMessages",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "createdAt",
                table: "dbChats",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "createdAt",
                table: "dbCategoryByUserForGames",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_dbGameGroups_dbGamesInShops_gameId",
                table: "dbGameGroups",
                column: "gameId",
                principalTable: "dbGamesInShops",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_dbGameGuides_dbGameGroups_gameGroupId",
                table: "dbGameGuides",
                column: "gameGroupId",
                principalTable: "dbGameGroups",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_dbGameGuides_dbGamesInShops_gameId",
                table: "dbGameGuides",
                column: "gameId",
                principalTable: "dbGamesInShops",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_dbGameNews_dbGameGroups_gameGroupId",
                table: "dbGameNews",
                column: "gameGroupId",
                principalTable: "dbGameGroups",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_dbMessages_dbChats_chatId",
                table: "dbMessages",
                column: "chatId",
                principalTable: "dbChats",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_dbMessages_dbUsers_senderId",
                table: "dbMessages",
                column: "senderId",
                principalTable: "dbUsers",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
