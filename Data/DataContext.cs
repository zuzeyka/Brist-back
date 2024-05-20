using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using Slush.Data.Entity.Community.GameGroup;
using Slush.Data.Entity.Community;
using Slush.Entity.Profile;
using Slush.Data.Entity.Profile;
using Slush.Data.Entity;
using Slush.Entity.Store.Product.Creators;
using Slush.Entity.Store.Product;
using Slush.Entity.Chat;

namespace Slush.Data
{
    public class DataContext : DbContext
    {
        #region GameGroup
        public DbSet<GameGroup>                dbGameGroups                { get; set; }
        public DbSet<GameComment>              dbGameComments              { get; set; }
        public DbSet<GameGuide>                dbGameGuides                { get; set; }
        public DbSet<GameNews>                 dbGameNews                  { get; set; }
        public DbSet<GamePosts>                dbGamePosts                 { get; set; }
        public DbSet<GameTopic>                dbGameTopics                { get; set; }
        #endregion
        #region Group
        public DbSet<Group>                    dbGroups                    { get; set; }
        public DbSet<GroupComment>             dbGroupComments             { get; set; }
        public DbSet<Post>                     dbPosts                     { get; set; }
        public DbSet<Topic>                    dbTopics                    { get; set; }
        #endregion
        #region Profile
        public DbSet<Friends>                  dbFriends                   { get; set; }
        public DbSet<Video>                    dbVideos                    { get; set; }
        public DbSet<Screenshot>               dbScreenshots               { get; set; }
        public DbSet<User>                     dbUsers                     { get; set; }
        public DbSet<UserComment>              dbUserComments              { get; set; }
        public DbSet<OwnedGame>                dbOwnedGames                { get; set; }
        public DbSet<WishedGame>               dbWishedGames               { get; set; }
        public DbSet<CategoryByUserForGame>    dbCategoryByUserForGames    { get; set; }
        public DbSet<UserCategory>             dbUserCategories            { get; set; }
        #endregion
        #region Categories
        public DbSet<Categories>               dbCategories                { get; set; }
        public DbSet<CategoryByAuthor>         dbCategoriesByAuthors       { get; set; }
        public DbSet<CategoryByUser>           dbCategoriesByUsers         { get; set; }
        public DbSet<CategoryForGame>          dbCategoriesForGame         { get; set; }
        #endregion
        #region Author
        public DbSet<Developer>                dbDevelopers                { get; set; }
        public DbSet<Publisher>                dbPublishers                { get; set; }
        #endregion
        #region Language
        public DbSet<Language>                 dbLanguages                 { get; set; }
        public DbSet<LanguageInGame>           dbLanguagesInGame           { get; set; }
        #endregion
        #region Requirement
        public DbSet<MaximumSystemRequirement> dbMaximumSystemRequirements { get; set; }
        public DbSet<MinimalSystemRequirement> dbMinimalSystemRequirements { get; set; }
        public DbSet<SystemRequirement>        dbSystemRequirements        { get; set; }
        #endregion
        #region Game
        public DbSet<DLCInShop>                dbDLCsInShop                { get; set; }
        public DbSet<GameInShop>               dbGamesInShops              { get; set; }
        #endregion
        #region Chat
        public DbSet<Chat>                     dbChats                     { get; set; }
        public DbSet<Message>                  dbMessages                  { get; set; }
        #endregion
        public DataContext(DbContextOptions options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<List<object>>().HasNoKey();
        }
    }
}
