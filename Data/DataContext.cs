using Microsoft.EntityFrameworkCore;
using Slush.Data.Entity.Community.GameGroup;
using Slush.Data.Entity.Community;
using Slush.Entity.Profile;
using Slush.Data.Entity.Profile;
using Slush.Data.Entity;
using Slush.Entity.Store.Product.Creators;
using Slush.Entity.Store.Product;
using Slush.Entity.Chat;
using Slush.Entity;

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
        public DbSet<Settings>                 dbSettings                  { get; set; }
        public DbSet<WalletTransactions>       dbWalletTransactions        { get; set; }
        public DbSet<Achievement>              dbAchievements              { get; set; }
        public DbSet<AchievementByUser>        dbAchievementByUser         { get; set; }
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
        public DbSet<GameBundle>               dbGameBundles               { get; set; }
        public DbSet<GameBundleCollection>     dbGameBundleCollections     { get; set; }
        #endregion
        #region Chat
        public DbSet<Chat>                     dbChats                     { get; set; }
        public DbSet<Message>                  dbMessages                  { get; set; }
        #endregion
        #region Discussion
        public DbSet<Discussion>               dbDiscussions               { get; set; }
        #endregion
        public DataContext(DbContextOptions options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Message>()
                 .HasOne<Chat>()
                 .WithMany()
                 .HasForeignKey(m => m.chatId)
                 .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Message>()
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(m => m.senderId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Chat>()
               .HasOne<User>()
               .WithMany()
               .HasForeignKey(c => c.firstUser)
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Chat>()
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(c => c.secondUser)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<GameGroup>()
                .HasOne<GameInShop>()
                .WithMany()
                .HasForeignKey(gg => gg.gameId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<GameGuide>()
               .HasOne<GameGroup>()
               .WithMany()
               .HasForeignKey(gg => gg.gameGroupId)
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<GameGuide>()
                .HasOne<GameInShop>()
                .WithMany()
                .HasForeignKey(gg => gg.gameId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<GameNews>()
                .HasOne<GameGroup>()
                .WithMany()
                .HasForeignKey(gn => gn.gameGroupId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<GameNews>()
                .HasOne<GameInShop>()
                .WithMany()
                .HasForeignKey(gn => gn.gameId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<GameGuide>()
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(gg => gg.authorId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<GameNews>()
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(gg => gg.authorId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<GameTopic>()
                .HasOne<GameGroup>()
                .WithMany()
                .HasForeignKey(gt => gt.attachedId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<GamePosts>()
                .HasOne<GameInShop>()
                .WithMany()
                .HasForeignKey(gp => gp.gameId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<GamePosts>()
                .HasOne<GameTopic>()
                .WithMany()
                .HasForeignKey(gp => gp.gameTopicId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<GamePosts>()
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(gp => gp.authorId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<GameComment>()
                .HasOne<GamePosts>()
                .WithMany()
                .HasForeignKey(gc => gc.gamePostId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<GameComment>()
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(gc => gc.authorId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Group>()
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(g => g.attachedId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Topic>()
                .HasOne<Group>()
                .WithMany()
                .HasForeignKey(g => g.attachedId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Topic>()
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(g => g.authorId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Post>()
                .HasOne<Topic>()
                .WithMany()
                .HasForeignKey(t => t.discussionId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Post>()
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(u => u.authorId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<GroupComment>()
                .HasOne<Group>()
                .WithMany()
                .HasForeignKey(g => g.groupId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<GroupComment>()
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(g => g.userId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Friends>()
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(u => u.userId)
                .OnDelete(DeleteBehavior.Cascade);
            
            modelBuilder.Entity<Friends>()
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(u => u.friendId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<OwnedGame>()
                .HasOne<GameInShop>()
                .WithMany()
                .HasForeignKey(g => g.ownedGameId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<OwnedGame>()
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(g => g.userId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Screenshot>()
                .HasOne<GameInShop>()
                .WithMany()
                .HasForeignKey(g => g.gameId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Screenshot>()
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(g => g.authorId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<UserCategory>()
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(u => u.userId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<UserCategory>()
                .HasOne<OwnedGame>()
                .WithMany()
                .HasForeignKey(u => u.ownedGameId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<UserCategory>()
                .HasOne<CategoryByUserForGame>()
                .WithMany()
                .HasForeignKey(u => u.categoryId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<UserComment>()
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(u => u.userId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<UserComment>()
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(u => u.authorId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Video>()
                .HasOne<GameInShop>()
                .WithMany()
                .HasForeignKey(g => g.gameId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Video>()
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(u => u.authorId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<WishedGame>()
                .HasOne<GameInShop>()
                .WithMany()
                .HasForeignKey(w => w.ownedGameId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<WishedGame>()
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(w => w.userId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<CategoryForGame>()
                .HasOne<GameInShop>()
                .WithMany()
                .HasForeignKey(g => g.gameId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<CategoryForGame>()
                .HasOne<Categories>()
                .WithMany()
                .HasForeignKey(g => g.categoryId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<LanguageInGame>()
                .HasOne<Language>()
                .WithMany()
                .HasForeignKey(g => g.languageId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<LanguageInGame>()
                .HasOne<GameInShop>()
                .WithMany()
                .HasForeignKey(g => g.gameId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<GameInShop>()
                .HasOne<Developer>()
                .WithMany()
                .HasForeignKey(g => g.developerId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<GameInShop>()
                .HasOne<Publisher>()
                .WithMany()
                .HasForeignKey(g => g.publisherId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<DLCInShop>()
                .HasOne<GameInShop>()
                .WithMany()
                .HasForeignKey(g => g.gameId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<DLCInShop>()
                .HasOne<Publisher>()
                .WithMany()
                .HasForeignKey(g => g.publisherId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<DLCInShop>()
                .HasOne<Developer>()
                .WithMany()
                .HasForeignKey(g => g.developerId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<CategoryByAuthor>()
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(g => g.authorId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<GameBundleCollection>()
                .HasOne<GameInShop>()
                .WithMany()
                .HasForeignKey(d => d.gameId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<GameBundleCollection>()
                .HasOne<DLCInShop>()
                .WithMany()
                .HasForeignKey(d => d.dlcId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<GameBundleCollection>()
                .HasOne<GameBundle>()
                .WithMany()
                .HasForeignKey(d => d.bundleId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Settings>()
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(g => g.attachedUserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<WalletTransactions>()
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(g => g.userId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<WalletTransactions>()
                .HasOne<GameInShop>()
                .WithMany()
                .HasForeignKey(g => g.transactionObj)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<WalletTransactions>()
                .HasOne<DLCInShop>()
                .WithMany()
                .HasForeignKey(g => g.transactionObj)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<AchievementByUser>()
                .HasOne<Achievement>()
                .WithMany()
                .HasForeignKey(a => a.achievementId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<AchievementByUser>()
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(u => u.userId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<List<object>>().HasNoKey();
        }
    }
}
