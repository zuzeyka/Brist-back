using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Slush.Migrations
{
    /// <inheritdoc />
    public partial class fixrelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_dbFriends_dbUsers_userId",
                table: "dbFriends");

            migrationBuilder.DropForeignKey(
                name: "FK_dbGameComments_Author_Authorid",
                table: "dbGameComments");

            migrationBuilder.DropForeignKey(
                name: "FK_dbGameComments_dbGamePosts_GamePostsid",
                table: "dbGameComments");

            migrationBuilder.DropForeignKey(
                name: "FK_dbGameGuides_dbGameGroups_gameGroupId",
                table: "dbGameGuides");

            migrationBuilder.DropForeignKey(
                name: "FK_dbGameNews_dbGameGroups_gameGroupid",
                table: "dbGameNews");

            migrationBuilder.DropForeignKey(
                name: "FK_dbGamePosts_dbGameGroups_gameGroupid",
                table: "dbGamePosts");

            migrationBuilder.DropForeignKey(
                name: "FK_dbGamePosts_dbGameTopics_GameTopicid",
                table: "dbGamePosts");

            migrationBuilder.DropForeignKey(
                name: "FK_dbGameTopics_dbGameGroups_GameGroupid",
                table: "dbGameTopics");

            migrationBuilder.DropForeignKey(
                name: "FK_dbGroupComments_dbGroups_groupId",
                table: "dbGroupComments");

            migrationBuilder.DropForeignKey(
                name: "FK_dbLanguagesInGame_dbDLCsInShop_DLCInShopid",
                table: "dbLanguagesInGame");

            migrationBuilder.DropForeignKey(
                name: "FK_dbOwnedGames_dbUsers_userId",
                table: "dbOwnedGames");

            migrationBuilder.DropForeignKey(
                name: "FK_dbPosts_dbTopics_Topicid",
                table: "dbPosts");

            migrationBuilder.DropForeignKey(
                name: "FK_dbScreenshots_dbGameGroups_GameGroupid",
                table: "dbScreenshots");

            migrationBuilder.DropForeignKey(
                name: "FK_dbScreenshots_dbUsers_Userid",
                table: "dbScreenshots");

            migrationBuilder.DropForeignKey(
                name: "FK_dbTopics_dbGroups_Groupid",
                table: "dbTopics");

            migrationBuilder.DropForeignKey(
                name: "FK_dbUserComments_dbUsers_userId",
                table: "dbUserComments");

            migrationBuilder.DropForeignKey(
                name: "FK_dbVideos_dbGameGroups_GameGroupid",
                table: "dbVideos");

            migrationBuilder.DropForeignKey(
                name: "FK_dbVideos_dbUsers_Userid",
                table: "dbVideos");

            migrationBuilder.DropForeignKey(
                name: "FK_dbWishedGames_dbUsers_userId",
                table: "dbWishedGames");

            migrationBuilder.DropTable(
                name: "Author");

            migrationBuilder.DropTable(
                name: "GroupUser");

            migrationBuilder.DropIndex(
                name: "IX_dbWishedGames_userId",
                table: "dbWishedGames");

            migrationBuilder.DropIndex(
                name: "IX_dbVideos_GameGroupid",
                table: "dbVideos");

            migrationBuilder.DropIndex(
                name: "IX_dbVideos_Userid",
                table: "dbVideos");

            migrationBuilder.DropIndex(
                name: "IX_dbUserComments_userId",
                table: "dbUserComments");

            migrationBuilder.DropIndex(
                name: "IX_dbTopics_Groupid",
                table: "dbTopics");

            migrationBuilder.DropIndex(
                name: "IX_dbScreenshots_GameGroupid",
                table: "dbScreenshots");

            migrationBuilder.DropIndex(
                name: "IX_dbScreenshots_Userid",
                table: "dbScreenshots");

            migrationBuilder.DropIndex(
                name: "IX_dbPosts_Topicid",
                table: "dbPosts");

            migrationBuilder.DropIndex(
                name: "IX_dbOwnedGames_userId",
                table: "dbOwnedGames");

            migrationBuilder.DropIndex(
                name: "IX_dbLanguagesInGame_DLCInShopid",
                table: "dbLanguagesInGame");

            migrationBuilder.DropIndex(
                name: "IX_dbGroupComments_groupId",
                table: "dbGroupComments");

            migrationBuilder.DropIndex(
                name: "IX_dbGameTopics_GameGroupid",
                table: "dbGameTopics");

            migrationBuilder.DropIndex(
                name: "IX_dbGamePosts_gameGroupid",
                table: "dbGamePosts");

            migrationBuilder.DropIndex(
                name: "IX_dbGamePosts_GameTopicid",
                table: "dbGamePosts");

            migrationBuilder.DropIndex(
                name: "IX_dbGameNews_gameGroupid",
                table: "dbGameNews");

            migrationBuilder.DropIndex(
                name: "IX_dbGameGuides_gameGroupId",
                table: "dbGameGuides");

            migrationBuilder.DropIndex(
                name: "IX_dbGameComments_Authorid",
                table: "dbGameComments");

            migrationBuilder.DropIndex(
                name: "IX_dbGameComments_GamePostsid",
                table: "dbGameComments");

            migrationBuilder.DropIndex(
                name: "IX_dbFriends_userId",
                table: "dbFriends");

            migrationBuilder.DropColumn(
                name: "GameGroupid",
                table: "dbVideos");

            migrationBuilder.DropColumn(
                name: "Userid",
                table: "dbVideos");

            migrationBuilder.DropColumn(
                name: "Groupid",
                table: "dbTopics");

            migrationBuilder.DropColumn(
                name: "GameGroupid",
                table: "dbScreenshots");

            migrationBuilder.DropColumn(
                name: "Userid",
                table: "dbScreenshots");

            migrationBuilder.DropColumn(
                name: "Topicid",
                table: "dbPosts");

            migrationBuilder.DropColumn(
                name: "DLCInShopid",
                table: "dbLanguagesInGame");

            migrationBuilder.DropColumn(
                name: "GameGroupid",
                table: "dbGameTopics");

            migrationBuilder.DropColumn(
                name: "gameGroupid",
                table: "dbGamePosts");

            migrationBuilder.DropColumn(
                name: "GamePostsid",
                table: "dbGameComments");

            migrationBuilder.DropColumn(
                name: "categories",
                table: "dbDLCsInShop");

            migrationBuilder.DropColumn(
                name: "categoriesId",
                table: "dbDLCsInShop");

            migrationBuilder.DropColumn(
                name: "languagesId",
                table: "dbDLCsInShop");

            migrationBuilder.DropColumn(
                name: "systemRequirementsId",
                table: "dbDLCsInShop");

            migrationBuilder.RenameColumn(
                name: "GameTopicid",
                table: "dbGamePosts",
                newName: "gameTopicId");

            migrationBuilder.RenameColumn(
                name: "gameGroupid",
                table: "dbGameNews",
                newName: "gameGroupId");

            migrationBuilder.RenameColumn(
                name: "Authorid",
                table: "dbGameComments",
                newName: "authorId");

            migrationBuilder.AddColumn<Guid>(
                name: "postId",
                table: "dbTopics",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.AlterColumn<Guid>(
                name: "gameTopicId",
                table: "dbGamePosts",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)",
                oldNullable: true)
                .OldAnnotation("Relational:Collation", "ascii_general_ci");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "postId",
                table: "dbTopics");

            migrationBuilder.RenameColumn(
                name: "gameTopicId",
                table: "dbGamePosts",
                newName: "GameTopicid");

            migrationBuilder.RenameColumn(
                name: "gameGroupId",
                table: "dbGameNews",
                newName: "gameGroupid");

            migrationBuilder.RenameColumn(
                name: "authorId",
                table: "dbGameComments",
                newName: "Authorid");

            migrationBuilder.AddColumn<Guid>(
                name: "GameGroupid",
                table: "dbVideos",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<Guid>(
                name: "Userid",
                table: "dbVideos",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<Guid>(
                name: "Groupid",
                table: "dbTopics",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<Guid>(
                name: "GameGroupid",
                table: "dbScreenshots",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<Guid>(
                name: "Userid",
                table: "dbScreenshots",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<Guid>(
                name: "Topicid",
                table: "dbPosts",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<Guid>(
                name: "DLCInShopid",
                table: "dbLanguagesInGame",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<Guid>(
                name: "GameGroupid",
                table: "dbGameTopics",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AlterColumn<Guid>(
                name: "GameTopicid",
                table: "dbGamePosts",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)")
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.AddColumn<Guid>(
                name: "gameGroupid",
                table: "dbGamePosts",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<Guid>(
                name: "GamePostsid",
                table: "dbGameComments",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<string>(
                name: "categories",
                table: "dbDLCsInShop",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "categoriesId",
                table: "dbDLCsInShop",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "languagesId",
                table: "dbDLCsInShop",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "systemRequirementsId",
                table: "dbDLCsInShop",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Author",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    createdAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    deleteAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    subscribersCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Author", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "GroupUser",
                columns: table => new
                {
                    groupsid = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    usersid = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupUser", x => new { x.groupsid, x.usersid });
                    table.ForeignKey(
                        name: "FK_GroupUser_dbGroups_groupsid",
                        column: x => x.groupsid,
                        principalTable: "dbGroups",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GroupUser_dbUsers_usersid",
                        column: x => x.usersid,
                        principalTable: "dbUsers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_dbWishedGames_userId",
                table: "dbWishedGames",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_dbVideos_GameGroupid",
                table: "dbVideos",
                column: "GameGroupid");

            migrationBuilder.CreateIndex(
                name: "IX_dbVideos_Userid",
                table: "dbVideos",
                column: "Userid");

            migrationBuilder.CreateIndex(
                name: "IX_dbUserComments_userId",
                table: "dbUserComments",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_dbTopics_Groupid",
                table: "dbTopics",
                column: "Groupid");

            migrationBuilder.CreateIndex(
                name: "IX_dbScreenshots_GameGroupid",
                table: "dbScreenshots",
                column: "GameGroupid");

            migrationBuilder.CreateIndex(
                name: "IX_dbScreenshots_Userid",
                table: "dbScreenshots",
                column: "Userid");

            migrationBuilder.CreateIndex(
                name: "IX_dbPosts_Topicid",
                table: "dbPosts",
                column: "Topicid");

            migrationBuilder.CreateIndex(
                name: "IX_dbOwnedGames_userId",
                table: "dbOwnedGames",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_dbLanguagesInGame_DLCInShopid",
                table: "dbLanguagesInGame",
                column: "DLCInShopid");

            migrationBuilder.CreateIndex(
                name: "IX_dbGroupComments_groupId",
                table: "dbGroupComments",
                column: "groupId");

            migrationBuilder.CreateIndex(
                name: "IX_dbGameTopics_GameGroupid",
                table: "dbGameTopics",
                column: "GameGroupid");

            migrationBuilder.CreateIndex(
                name: "IX_dbGamePosts_gameGroupid",
                table: "dbGamePosts",
                column: "gameGroupid");

            migrationBuilder.CreateIndex(
                name: "IX_dbGamePosts_GameTopicid",
                table: "dbGamePosts",
                column: "GameTopicid");

            migrationBuilder.CreateIndex(
                name: "IX_dbGameNews_gameGroupid",
                table: "dbGameNews",
                column: "gameGroupid");

            migrationBuilder.CreateIndex(
                name: "IX_dbGameGuides_gameGroupId",
                table: "dbGameGuides",
                column: "gameGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_dbGameComments_Authorid",
                table: "dbGameComments",
                column: "Authorid");

            migrationBuilder.CreateIndex(
                name: "IX_dbGameComments_GamePostsid",
                table: "dbGameComments",
                column: "GamePostsid");

            migrationBuilder.CreateIndex(
                name: "IX_dbFriends_userId",
                table: "dbFriends",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupUser_usersid",
                table: "GroupUser",
                column: "usersid");

            migrationBuilder.AddForeignKey(
                name: "FK_dbFriends_dbUsers_userId",
                table: "dbFriends",
                column: "userId",
                principalTable: "dbUsers",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_dbGameComments_Author_Authorid",
                table: "dbGameComments",
                column: "Authorid",
                principalTable: "Author",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_dbGameComments_dbGamePosts_GamePostsid",
                table: "dbGameComments",
                column: "GamePostsid",
                principalTable: "dbGamePosts",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_dbGameGuides_dbGameGroups_gameGroupId",
                table: "dbGameGuides",
                column: "gameGroupId",
                principalTable: "dbGameGroups",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_dbGameNews_dbGameGroups_gameGroupid",
                table: "dbGameNews",
                column: "gameGroupid",
                principalTable: "dbGameGroups",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_dbGamePosts_dbGameGroups_gameGroupid",
                table: "dbGamePosts",
                column: "gameGroupid",
                principalTable: "dbGameGroups",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_dbGamePosts_dbGameTopics_GameTopicid",
                table: "dbGamePosts",
                column: "GameTopicid",
                principalTable: "dbGameTopics",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_dbGameTopics_dbGameGroups_GameGroupid",
                table: "dbGameTopics",
                column: "GameGroupid",
                principalTable: "dbGameGroups",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_dbGroupComments_dbGroups_groupId",
                table: "dbGroupComments",
                column: "groupId",
                principalTable: "dbGroups",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_dbLanguagesInGame_dbDLCsInShop_DLCInShopid",
                table: "dbLanguagesInGame",
                column: "DLCInShopid",
                principalTable: "dbDLCsInShop",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_dbOwnedGames_dbUsers_userId",
                table: "dbOwnedGames",
                column: "userId",
                principalTable: "dbUsers",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_dbPosts_dbTopics_Topicid",
                table: "dbPosts",
                column: "Topicid",
                principalTable: "dbTopics",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_dbScreenshots_dbGameGroups_GameGroupid",
                table: "dbScreenshots",
                column: "GameGroupid",
                principalTable: "dbGameGroups",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_dbScreenshots_dbUsers_Userid",
                table: "dbScreenshots",
                column: "Userid",
                principalTable: "dbUsers",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_dbTopics_dbGroups_Groupid",
                table: "dbTopics",
                column: "Groupid",
                principalTable: "dbGroups",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_dbUserComments_dbUsers_userId",
                table: "dbUserComments",
                column: "userId",
                principalTable: "dbUsers",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_dbVideos_dbGameGroups_GameGroupid",
                table: "dbVideos",
                column: "GameGroupid",
                principalTable: "dbGameGroups",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_dbVideos_dbUsers_Userid",
                table: "dbVideos",
                column: "Userid",
                principalTable: "dbUsers",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_dbWishedGames_dbUsers_userId",
                table: "dbWishedGames",
                column: "userId",
                principalTable: "dbUsers",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
