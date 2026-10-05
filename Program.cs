using Microsoft.EntityFrameworkCore;
using Slush.Repositories.GameInShopRepository;
using Slush.Repositories.CategoriesRepository;
using Slush.Data;
using Slush.Repositories.CreatorsRepository;
using Slush.Repositories.GameGroupRepository;
using Slush.Repositories.GroupRepository;
using Slush.Repositories.LanguageRepository;
using Slush.Repositories.ProfileRepository;
using Slush.Repositories.RequirementsRepository;
using Slush.Repositories.ChatRepository;
using Slush.Repositories;
using Slush.Services.JWT;
using Slush.Services.RegistrationValidation;
using Slush.Services.Hash;
using Slush.Services.FileStorage;
using FullStackBrist.Server.Services.Email;
using FullStackBrist.Server.Services.Random;
using Slush.Repositories.IRepository;
using Slush.Services.Email;
using Slush.Extensions;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("MySqlDb");
if (string.IsNullOrWhiteSpace(connectionString))
{
    throw new InvalidOperationException("ConnectionStrings:MySqlDb not configured. Set it via the ConnectionStrings__MySqlDb environment variable or user-secrets.");
}
builder.Services.AddDbContext<DataContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.Configure<JWTOptions>(builder.Configuration.GetSection(nameof(JWTOptions)));

var jwtOptions = builder.Configuration.GetSection(nameof(JWTOptions)).Get<JWTOptions>();
if (jwtOptions == null || string.IsNullOrWhiteSpace(jwtOptions.SecretKey))
{
    throw new InvalidOperationException("JWTOptions:SecretKey not configured. Set it via the JWTOptions__SecretKey environment variable or user-secrets.");
}
builder.Services.ApiAuth(Options.Create(jwtOptions));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();


builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<IRegistrationService, RegistrationService>();
builder.Services.AddScoped<IHashPasswordService, HashPasswordService>();
builder.Services.AddScoped<IRandomService, RandomService>();
builder.Services.AddScoped<IFileStorageService, LocalFileStorageService>();
builder.Services.AddScoped<IJWTService, JWTService>();
builder.Services.AddScoped<IEmailService, EmailService>();

builder.Services.AddRazorPages();

#region Repositories

builder.Services.AddScoped<ICategoriesRepository, CategoriesRepository>();
builder.Services.AddScoped<ICategoriesByAuthorRepository, CategoriesByAuthorRepository>();
builder.Services.AddScoped<ICategoriesByUserRepository, CategoriesByUserRepository>();
builder.Services.AddScoped<ICategoryForGameRepository, CategoryForGameRepository>();

builder.Services.AddScoped<IDeveloperRepository, DeveloperRepository>();
builder.Services.AddScoped<IPublisherRepository, PublisherRepository>();

builder.Services.AddScoped<IGameCommentRepository, GameCommentRepository>();
builder.Services.AddScoped<IGameGroupRepository, GameGroupRepository>();
builder.Services.AddScoped<IGameGuideRepository, GameGuideRepository>();
builder.Services.AddScoped<IGameNewsRepository, GameNewsRepository>();
builder.Services.AddScoped<IGamePostsRepository, GamePostsRepository>();
builder.Services.AddScoped<IGameTopicRepository, GameTopicRepository>();

builder.Services.AddScoped<IDLCInShopRepository, DLCInShopRepository>();
builder.Services.AddScoped<IGameInShopRepository, GameInShopRepository>();

builder.Services.AddScoped<IGroupCommentRepository, GroupCommentRepository>();
builder.Services.AddScoped<IGroupRepository, GroupRepository>();
builder.Services.AddScoped<IPostRepository, PostRepository>();
builder.Services.AddScoped<ITopicRepository, TopicRepository>();

builder.Services.AddScoped<ILanguageRepository, LanguageRepository>();
builder.Services.AddScoped<ILanguageInGameRepository, LanguageInGameRepository>();

builder.Services.AddScoped<IFriendsRepository, FriendsRepository>();
builder.Services.AddScoped<IOwnedGameRepository, OwnedGameRepository>();
builder.Services.AddScoped<IOwnedDlcRepository, OwnedDlcRepository>();
builder.Services.AddScoped<IScreenshotRepository, ScreenshotRepository>();
builder.Services.AddScoped<IUserCommentRepository, UserCommentRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IVideoRepository, VideoRepository>();
builder.Services.AddScoped<IWishedGameRepository, WishedGameRepository>();

builder.Services.AddScoped<IMaximumSystemRequirementRepository, MaximumSystemRequirementRepository>();
builder.Services.AddScoped<IMinimalSystemRequirementRepository, MinimalSystemRequirementRepository>();

builder.Services.AddScoped<IMessageRepository, MessageRepository>();
builder.Services.AddScoped<IChatRepository, ChatRepository>();

builder.Services.AddScoped<IDiscussionRepository, DiscussionRepository>();

builder.Services.AddScoped<ICategoryByUserForGameRepository, CategoryByUserForGameRepository>();
builder.Services.AddScoped<IUserCategoryRepository, UserCategoryRepository>();

builder.Services.AddScoped<ISettingsRepository, SettingsRepository>();
builder.Services.AddScoped<IWalletTransactions, WalletTransactionsRepository>();

builder.Services.AddScoped<IAchievementByUserRepository, AchievementByUserRepository>();
builder.Services.AddScoped<IAchievementRepository, AchievementRepository>();

builder.Services.AddScoped<IGameBundleCollectionRepository, GameBundleCollectionRepository>();
builder.Services.AddScoped<IGameBundleRepository, GameBundleRepository>();

#endregion

builder.Services.AddCors(options =>
{
    options.AddPolicy("corsapp", builder =>
    {
        builder.WithOrigins("http://localhost:5173")
               .AllowAnyMethod()
               .AllowAnyHeader()
               .AllowCredentials();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    using var scope = app.Services.CreateScope();
    var context = scope.ServiceProvider.GetRequiredService<DataContext>();
    // The committed migration history assumes tables that an earlier (now-missing)
    // migration created, so replaying it against a fresh local DB fails partway.
    // Build the schema straight from the current model instead.
    await context.Database.EnsureCreatedAsync();
    await DbSeeder.SeedAsync(context);
}

app.UseCors("corsapp");

app.UseCookiePolicy(new CookiePolicyOptions
{
    MinimumSameSitePolicy = SameSiteMode.Strict,
    HttpOnly = Microsoft.AspNetCore.CookiePolicy.HttpOnlyPolicy.Always,
    Secure = CookieSecurePolicy.Always
});

app.UseDefaultFiles();

if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
