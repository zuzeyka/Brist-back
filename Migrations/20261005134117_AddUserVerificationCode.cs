using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Slush.Migrations
{
    /// <inheritdoc />
    public partial class AddUserVerificationCode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "verificationCode",
                table: "dbUsers",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<DateTime>(
                name: "verificationCodeExpiresAt",
                table: "dbUsers",
                type: "datetime(6)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "verificationCode",
                table: "dbUsers");

            migrationBuilder.DropColumn(
                name: "verificationCodeExpiresAt",
                table: "dbUsers");
        }
    }
}
