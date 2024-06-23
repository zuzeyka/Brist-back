using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Slush.Migrations
{
    /// <inheritdoc />
    public partial class Addcontenturltorequiredplaces : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "videoUrl",
                table: "dbVideos",
                newName: "contentUrl");

            migrationBuilder.RenameColumn(
                name: "screenshotUrl",
                table: "dbScreenshots",
                newName: "contentUrl");

            migrationBuilder.AddColumn<string>(
                name: "contentUrl",
                table: "dbPosts",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "contentUrl",
                table: "dbGamePosts",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "contentUrl",
                table: "dbGameNews",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "contentUrl",
                table: "dbGameGuides",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "contentUrl",
                table: "dbPosts");

            migrationBuilder.DropColumn(
                name: "contentUrl",
                table: "dbGamePosts");

            migrationBuilder.DropColumn(
                name: "contentUrl",
                table: "dbGameNews");

            migrationBuilder.DropColumn(
                name: "contentUrl",
                table: "dbGameGuides");

            migrationBuilder.RenameColumn(
                name: "contentUrl",
                table: "dbVideos",
                newName: "videoUrl");

            migrationBuilder.RenameColumn(
                name: "contentUrl",
                table: "dbScreenshots",
                newName: "screenshotUrl");
        }
    }
}
