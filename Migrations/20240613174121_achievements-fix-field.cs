using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Slush.Migrations
{
    /// <inheritdoc />
    public partial class achievementsfixfield : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "awardTime",
                table: "dbAchievements");

            migrationBuilder.AddColumn<DateTime>(
                name: "awardTime",
                table: "dbAchievementByUser",
                type: "datetime(6)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "awardTime",
                table: "dbAchievementByUser");

            migrationBuilder.AddColumn<DateTime>(
                name: "awardTime",
                table: "dbAchievements",
                type: "datetime(6)",
                nullable: true);
        }
    }
}
