using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Slush.Migrations
{
    /// <inheritdoc />
    public partial class achievementsrelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_dbAchievementByUser_achievementId",
                table: "dbAchievementByUser",
                column: "achievementId");

            migrationBuilder.CreateIndex(
                name: "IX_dbAchievementByUser_userId",
                table: "dbAchievementByUser",
                column: "userId");

            migrationBuilder.AddForeignKey(
                name: "FK_dbAchievementByUser_dbAchievements_achievementId",
                table: "dbAchievementByUser",
                column: "achievementId",
                principalTable: "dbAchievements",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_dbAchievementByUser_dbUsers_userId",
                table: "dbAchievementByUser",
                column: "userId",
                principalTable: "dbUsers",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_dbAchievementByUser_dbAchievements_achievementId",
                table: "dbAchievementByUser");

            migrationBuilder.DropForeignKey(
                name: "FK_dbAchievementByUser_dbUsers_userId",
                table: "dbAchievementByUser");

            migrationBuilder.DropIndex(
                name: "IX_dbAchievementByUser_achievementId",
                table: "dbAchievementByUser");

            migrationBuilder.DropIndex(
                name: "IX_dbAchievementByUser_userId",
                table: "dbAchievementByUser");
        }
    }
}
