using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FullStackBrist.Server.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Author",
                columns: table => new
                {
                    id = table.Column<String>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    createdAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    deleteAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    subscribersCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Author", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "List<object>",
                columns: table => new
                {
                    Capacity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "dbCategories",
                columns: table => new
                {
                    id = table.Column<String>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    name = table.Column<String>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    description = table.Column<String>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    createdAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    deleteAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbCategories", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "dbCategoriesByAuthors",
                columns: table => new
                {
                    id = table.Column<String>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    name = table.Column<String>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    description = table.Column<String>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    image = table.Column<String>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    createdAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    deleteAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbCategoriesByAuthors", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "dbCategoriesByUsers",
                columns: table => new
                {
                    id = table.Column<String>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    name = table.Column<String>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    description = table.Column<String>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    createdAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    deleteAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbCategoriesByUsers", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "dbCategoriesForGame",
                columns: table => new
                {
                    id = table.Column<String>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    gameId = table.Column<String>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    categoryId = table.Column<String>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    createdAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    deleteAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbCategoriesForGame", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "dbDLCsInShop",
                columns: table => new
                {
                    id = table.Column<String>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    gameId = table.Column<String>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    name = table.Column<String>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    price = table.Column<float>(type: "float", nullable: false),
                    discount = table.Column<int>(type: "int", nullable: false),
                    previeImage = table.Column<String>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    gameImages = table.Column<String>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    dateOfRelease = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    developerId = table.Column<String>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    publisherId = table.Column<String>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    categoriesId = table.Column<String>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    languagesId = table.Column<String>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    systemRequirementsId = table.Column<String>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    urlForContent = table.Column<String>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    createdAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    deleteAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    categories = table.Column<String>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbDLCsInShop", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "dbDevelopers",
                columns: table => new
                {
                    id = table.Column<String>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    subscribersCount = table.Column<int>(type: "int", nullable: false),
                    name = table.Column<String>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    description = table.Column<String>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    avatar = table.Column<String>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    backgroundImage = table.Column<String>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    urlForNewsPage = table.Column<String>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    createdAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    deleteAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbDevelopers", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "dbGameGroups",
                columns: table => new
                {
                    id = table.Column<String>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    gameId = table.Column<String>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    createdAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    deleteAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbGameGroups", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "dbGamesInShops",
                columns: table => new
                {
                    id = table.Column<String>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    name = table.Column<String>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    price = table.Column<float>(type: "float", nullable: false),
                    discount = table.Column<int>(type: "int", nullable: false),
                    previeImage = table.Column<String>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    dateOfRelease = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    developerId = table.Column<String>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    publisherId = table.Column<String>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    urlForContent = table.Column<String>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    createdAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    deleteAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    categoriesId = table.Column<String>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    gameImages = table.Column<String>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    languagesId = table.Column<String>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    systemRequirementsId = table.Column<String>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbGamesInShops", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "dbGroups",
                columns: table => new
                {
                    id = table.Column<String>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    attachedId = table.Column<String>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    name = table.Column<String>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    description = table.Column<String>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    createdAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    deleteAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbGroups", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "dbLanguages",
                columns: table => new
                {
                    id = table.Column<String>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    name = table.Column<String>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    createdAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    deleteAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbLanguages", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "dbMaximumSystemRequirements",
                columns: table => new
                {
                    id = table.Column<String>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    gameId = table.Column<String>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    OS = table.Column<String>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    processor = table.Column<String>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    RAM = table.Column<String>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    video = table.Column<String>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    freeDiskSpace = table.Column<String>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    createdAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    deleteAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbMaximumSystemRequirements", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "dbMinimalSystemRequirements",
                columns: table => new
                {
                    id = table.Column<String>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    gameId = table.Column<String>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    OS = table.Column<String>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    processor = table.Column<String>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    RAM = table.Column<String>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    video = table.Column<String>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    freeDiskSpace = table.Column<String>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    createdAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    deleteAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbMinimalSystemRequirements", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "dbPublishers",
                columns: table => new
                {
                    id = table.Column<String>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    subscribersCount = table.Column<int>(type: "int", nullable: false),
                    name = table.Column<String>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    description = table.Column<String>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    avatar = table.Column<String>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    backgroundImage = table.Column<String>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    urlForNewsPage = table.Column<String>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    createdAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    deleteAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbPublishers", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "dbSystemRequirements",
                columns: table => new
                {
                    id = table.Column<String>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    gameId = table.Column<String>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    OS = table.Column<String>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    processor = table.Column<String>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    RAM = table.Column<String>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    video = table.Column<String>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    freeDiskSpace = table.Column<String>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    createdAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    deleteAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbSystemRequirements", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "dbUsers",
                columns: table => new
                {
                    id = table.Column<String>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    name = table.Column<String>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    passwordSalt = table.Column<String>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    salt = table.Column<String>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    email = table.Column<String>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    phone = table.Column<String>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    createdAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    deleteAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbUsers", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "dbGameGuides",
                columns: table => new
                {
                    id = table.Column<String>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    title = table.Column<String>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    description = table.Column<String>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    likesCount = table.Column<int>(type: "int", nullable: false),
                    dislikesCount = table.Column<int>(type: "int", nullable: false),
                    discussionId = table.Column<String>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    gameId = table.Column<String>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    authorId = table.Column<String>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    gameGroupId = table.Column<String>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    content = table.Column<String>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    createdAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    deleteAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbGameGuides", x => x.id);
                    table.ForeignKey(
                        name: "FK_dbGameGuides_dbGameGroups_gameGroupId",
                        column: x => x.gameGroupId,
                        principalTable: "dbGameGroups",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "dbGameNews",
                columns: table => new
                {
                    id = table.Column<String>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    title = table.Column<String>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    description = table.Column<String>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    likesCount = table.Column<int>(type: "int", nullable: false),
                    dislikesCount = table.Column<int>(type: "int", nullable: false),
                    discussionId = table.Column<String>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    gameId = table.Column<String>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    authorId = table.Column<String>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    content = table.Column<String>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    createdAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    deleteAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    gameGroupid = table.Column<String>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbGameNews", x => x.id);
                    table.ForeignKey(
                        name: "FK_dbGameNews_dbGameGroups_gameGroupid",
                        column: x => x.gameGroupid,
                        principalTable: "dbGameGroups",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "dbGameTopics",
                columns: table => new
                {
                    id = table.Column<String>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    attachedId = table.Column<String>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    name = table.Column<String>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    description = table.Column<String>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    createdAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    deleteAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    GameGroupid = table.Column<String>(type: "varchar(255)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbGameTopics", x => x.id);
                    table.ForeignKey(
                        name: "FK_dbGameTopics_dbGameGroups_GameGroupid",
                        column: x => x.GameGroupid,
                        principalTable: "dbGameGroups",
                        principalColumn: "id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "dbLanguagesInGame",
                columns: table => new
                {
                    id = table.Column<String>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    gameId = table.Column<String>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    languageId = table.Column<String>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    createdAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    deleteAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DLCInShopid = table.Column<String>(type: "varchar(255)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    GameInShopid = table.Column<String>(type: "varchar(255)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbLanguagesInGame", x => x.id);
                    table.ForeignKey(
                        name: "FK_dbLanguagesInGame_dbDLCsInShop_DLCInShopid",
                        column: x => x.DLCInShopid,
                        principalTable: "dbDLCsInShop",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_dbLanguagesInGame_dbGamesInShops_GameInShopid",
                        column: x => x.GameInShopid,
                        principalTable: "dbGamesInShops",
                        principalColumn: "id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "dbGroupComments",
                columns: table => new
                {
                    id = table.Column<String>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    groupId = table.Column<String>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    content = table.Column<String>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    userId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    createdAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    deleteAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbGroupComments", x => x.id);
                    table.ForeignKey(
                        name: "FK_dbGroupComments_dbGroups_groupId",
                        column: x => x.groupId,
                        principalTable: "dbGroups",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "dbTopics",
                columns: table => new
                {
                    id = table.Column<String>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    attachedId = table.Column<String>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    name = table.Column<String>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    description = table.Column<String>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    authorId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    createdAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    deleteAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Groupid = table.Column<String>(type: "varchar(255)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbTopics", x => x.id);
                    table.ForeignKey(
                        name: "FK_dbTopics_dbGroups_Groupid",
                        column: x => x.Groupid,
                        principalTable: "dbGroups",
                        principalColumn: "id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "GroupUser",
                columns: table => new
                {
                    groupsid = table.Column<String>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    usersid = table.Column<String>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
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

            migrationBuilder.CreateTable(
                name: "dbFriends",
                columns: table => new
                {
                    id = table.Column<String>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    userId = table.Column<String>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    friendId = table.Column<String>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    createdAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    deleteAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbFriends", x => x.id);
                    table.ForeignKey(
                        name: "FK_dbFriends_dbUsers_userId",
                        column: x => x.userId,
                        principalTable: "dbUsers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "dbOwnedGames",
                columns: table => new
                {
                    id = table.Column<String>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ownedGameId = table.Column<String>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    userId = table.Column<String>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    createdAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    deleteAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbOwnedGames", x => x.id);
                    table.ForeignKey(
                        name: "FK_dbOwnedGames_dbUsers_userId",
                        column: x => x.userId,
                        principalTable: "dbUsers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "dbScreenshots",
                columns: table => new
                {
                    id = table.Column<String>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    title = table.Column<String>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    description = table.Column<String>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    likesCount = table.Column<int>(type: "int", nullable: false),
                    dislikesCount = table.Column<int>(type: "int", nullable: false),
                    discussionId = table.Column<String>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    gameId = table.Column<String>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    authorId = table.Column<String>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    screenshotUrl = table.Column<String>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    createdAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    deleteAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    GameGroupid = table.Column<String>(type: "varchar(255)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Userid = table.Column<String>(type: "varchar(255)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbScreenshots", x => x.id);
                    table.ForeignKey(
                        name: "FK_dbScreenshots_dbGameGroups_GameGroupid",
                        column: x => x.GameGroupid,
                        principalTable: "dbGameGroups",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_dbScreenshots_dbUsers_Userid",
                        column: x => x.Userid,
                        principalTable: "dbUsers",
                        principalColumn: "id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "dbUserComments",
                columns: table => new
                {
                    id = table.Column<String>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    userId = table.Column<String>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    authorId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    content = table.Column<String>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    createdAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    deleteAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbUserComments", x => x.id);
                    table.ForeignKey(
                        name: "FK_dbUserComments_dbUsers_userId",
                        column: x => x.userId,
                        principalTable: "dbUsers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "dbVideos",
                columns: table => new
                {
                    id = table.Column<String>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    title = table.Column<String>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    description = table.Column<String>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    likesCount = table.Column<int>(type: "int", nullable: false),
                    dislikesCount = table.Column<int>(type: "int", nullable: false),
                    discussionId = table.Column<String>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    gameId = table.Column<String>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    authorId = table.Column<String>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    videoUrl = table.Column<String>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    createdAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    deleteAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    GameGroupid = table.Column<String>(type: "varchar(255)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Userid = table.Column<String>(type: "varchar(255)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbVideos", x => x.id);
                    table.ForeignKey(
                        name: "FK_dbVideos_dbGameGroups_GameGroupid",
                        column: x => x.GameGroupid,
                        principalTable: "dbGameGroups",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_dbVideos_dbUsers_Userid",
                        column: x => x.Userid,
                        principalTable: "dbUsers",
                        principalColumn: "id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "dbWishedGames",
                columns: table => new
                {
                    id = table.Column<String>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ownedGameId = table.Column<String>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    userId = table.Column<String>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    createdAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    deleteAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbWishedGames", x => x.id);
                    table.ForeignKey(
                        name: "FK_dbWishedGames_dbUsers_userId",
                        column: x => x.userId,
                        principalTable: "dbUsers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "dbGamePosts",
                columns: table => new
                {
                    id = table.Column<String>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    title = table.Column<String>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    description = table.Column<String>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    likesCount = table.Column<int>(type: "int", nullable: false),
                    dislikesCount = table.Column<int>(type: "int", nullable: false),
                    discussionId = table.Column<String>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    gameId = table.Column<String>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    authorId = table.Column<String>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    content = table.Column<String>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    createdAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    deleteAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    gameGroupid = table.Column<String>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    GameTopicid = table.Column<String>(type: "varchar(255)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbGamePosts", x => x.id);
                    table.ForeignKey(
                        name: "FK_dbGamePosts_dbGameGroups_gameGroupid",
                        column: x => x.gameGroupid,
                        principalTable: "dbGameGroups",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_dbGamePosts_dbGameTopics_GameTopicid",
                        column: x => x.GameTopicid,
                        principalTable: "dbGameTopics",
                        principalColumn: "id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "dbPosts",
                columns: table => new
                {
                    id = table.Column<String>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    title = table.Column<String>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    description = table.Column<String>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    likesCount = table.Column<int>(type: "int", nullable: false),
                    dislikesCount = table.Column<int>(type: "int", nullable: false),
                    discussionId = table.Column<String>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    gameId = table.Column<String>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    authorId = table.Column<String>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    content = table.Column<String>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    createdAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    deleteAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Topicid = table.Column<String>(type: "varchar(255)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbPosts", x => x.id);
                    table.ForeignKey(
                        name: "FK_dbPosts_dbTopics_Topicid",
                        column: x => x.Topicid,
                        principalTable: "dbTopics",
                        principalColumn: "id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "dbGameComments",
                columns: table => new
                {
                    id = table.Column<String>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    gamePostId = table.Column<String>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    content = table.Column<String>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    createdAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    deleteAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Authorid = table.Column<String>(type: "varchar(255)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    GamePostsid = table.Column<String>(type: "varchar(255)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbGameComments", x => x.id);
                    table.ForeignKey(
                        name: "FK_dbGameComments_Author_Authorid",
                        column: x => x.Authorid,
                        principalTable: "Author",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_dbGameComments_dbGamePosts_GamePostsid",
                        column: x => x.GamePostsid,
                        principalTable: "dbGamePosts",
                        principalColumn: "id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_GroupUser_usersid",
                table: "GroupUser",
                column: "usersid");

            migrationBuilder.CreateIndex(
                name: "IX_dbFriends_userId",
                table: "dbFriends",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_dbGameComments_Authorid",
                table: "dbGameComments",
                column: "Authorid");

            migrationBuilder.CreateIndex(
                name: "IX_dbGameComments_GamePostsid",
                table: "dbGameComments",
                column: "GamePostsid");

            migrationBuilder.CreateIndex(
                name: "IX_dbGameGuides_gameGroupId",
                table: "dbGameGuides",
                column: "gameGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_dbGameNews_gameGroupid",
                table: "dbGameNews",
                column: "gameGroupid");

            migrationBuilder.CreateIndex(
                name: "IX_dbGamePosts_GameTopicid",
                table: "dbGamePosts",
                column: "GameTopicid");

            migrationBuilder.CreateIndex(
                name: "IX_dbGamePosts_gameGroupid",
                table: "dbGamePosts",
                column: "gameGroupid");

            migrationBuilder.CreateIndex(
                name: "IX_dbGameTopics_GameGroupid",
                table: "dbGameTopics",
                column: "GameGroupid");

            migrationBuilder.CreateIndex(
                name: "IX_dbGroupComments_groupId",
                table: "dbGroupComments",
                column: "groupId");

            migrationBuilder.CreateIndex(
                name: "IX_dbLanguagesInGame_DLCInShopid",
                table: "dbLanguagesInGame",
                column: "DLCInShopid");

            migrationBuilder.CreateIndex(
                name: "IX_dbLanguagesInGame_GameInShopid",
                table: "dbLanguagesInGame",
                column: "GameInShopid");

            migrationBuilder.CreateIndex(
                name: "IX_dbOwnedGames_userId",
                table: "dbOwnedGames",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_dbPosts_Topicid",
                table: "dbPosts",
                column: "Topicid");

            migrationBuilder.CreateIndex(
                name: "IX_dbScreenshots_GameGroupid",
                table: "dbScreenshots",
                column: "GameGroupid");

            migrationBuilder.CreateIndex(
                name: "IX_dbScreenshots_Userid",
                table: "dbScreenshots",
                column: "Userid");

            migrationBuilder.CreateIndex(
                name: "IX_dbTopics_Groupid",
                table: "dbTopics",
                column: "Groupid");

            migrationBuilder.CreateIndex(
                name: "IX_dbUserComments_userId",
                table: "dbUserComments",
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
                name: "IX_dbWishedGames_userId",
                table: "dbWishedGames",
                column: "userId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GroupUser");

            migrationBuilder.DropTable(
                name: "List<object>");

            migrationBuilder.DropTable(
                name: "dbCategories");

            migrationBuilder.DropTable(
                name: "dbCategoriesByAuthors");

            migrationBuilder.DropTable(
                name: "dbCategoriesByUsers");

            migrationBuilder.DropTable(
                name: "dbCategoriesForGame");

            migrationBuilder.DropTable(
                name: "dbDevelopers");

            migrationBuilder.DropTable(
                name: "dbFriends");

            migrationBuilder.DropTable(
                name: "dbGameComments");

            migrationBuilder.DropTable(
                name: "dbGameGuides");

            migrationBuilder.DropTable(
                name: "dbGameNews");

            migrationBuilder.DropTable(
                name: "dbGroupComments");

            migrationBuilder.DropTable(
                name: "dbLanguages");

            migrationBuilder.DropTable(
                name: "dbLanguagesInGame");

            migrationBuilder.DropTable(
                name: "dbMaximumSystemRequirements");

            migrationBuilder.DropTable(
                name: "dbMinimalSystemRequirements");

            migrationBuilder.DropTable(
                name: "dbOwnedGames");

            migrationBuilder.DropTable(
                name: "dbPosts");

            migrationBuilder.DropTable(
                name: "dbPublishers");

            migrationBuilder.DropTable(
                name: "dbScreenshots");

            migrationBuilder.DropTable(
                name: "dbSystemRequirements");

            migrationBuilder.DropTable(
                name: "dbUserComments");

            migrationBuilder.DropTable(
                name: "dbVideos");

            migrationBuilder.DropTable(
                name: "dbWishedGames");

            migrationBuilder.DropTable(
                name: "Author");

            migrationBuilder.DropTable(
                name: "dbGamePosts");

            migrationBuilder.DropTable(
                name: "dbDLCsInShop");

            migrationBuilder.DropTable(
                name: "dbGamesInShops");

            migrationBuilder.DropTable(
                name: "dbTopics");

            migrationBuilder.DropTable(
                name: "dbUsers");

            migrationBuilder.DropTable(
                name: "dbGameTopics");

            migrationBuilder.DropTable(
                name: "dbGroups");

            migrationBuilder.DropTable(
                name: "dbGameGroups");
        }
    }
}
