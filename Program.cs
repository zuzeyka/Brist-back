using Microsoft.EntityFrameworkCore;
using Slush.DAO.GameInShopRepository;
using Slush.DAO.CategoriesRepository;
using Slush.Data;
using Slush.DAO.CreatorsRepository;
using Slush.DAO.GameGroupRepository;
using Slush.DAO.GroupRepository;
using Slush.DAO.LanguageRepository;
using Slush.DAO.ProfileRepository;
using Slush.DAO.RequirementsRepository;
using Slush.DAO.ChatRepository;
using Slush.DAO;
using Slush.Services.JWT;
using Slush.Services.RegistrationValidation;
using Slush.Services.Hash;
using Minio;
using Slush.Services.Minio;
using FullStackBrist.Server.Services.Email;
using FullStackBrist.Server.Services.Random;
using Slush.DAO.GroupRepository;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("MySqlDb") ?? throw new InvalidOperationException("Connection String 'MySqlDb' not found.");
builder.Services.AddDbContext<DataContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.Configure<JWTOptions>(builder.Configuration.GetSection(nameof(JWTOptions)));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSingleton<IMinioClient>(sp =>
{
    return new MinioClient()
        .WithEndpoint("172.16.10.22:9000")
        .WithCredentials("iLuBz5sTXKUSVwQkvigf", "SjlfQa6pJkEIvtcxCcSpa9l30Vq82G8uInJ6m0vT")
        .WithSSL(false)
        .Build();
});


builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<RegistrationService>();
builder.Services.AddScoped<HashPasswordService>();
builder.Services.AddScoped<RandomService>();
builder.Services.AddScoped<MinioService>();
builder.Services.AddScoped<JWTService>();
builder.Services.AddScoped<EmailService>();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "API",
        Version = "V1"
    });
});

builder.Services.AddRazorPages();

#region DAO

builder.Services.AddTransient<CategoriesRepository>();
builder.Services.AddTransient<CategoriesByAuthorRepository>();
builder.Services.AddTransient<CategoriesByUserRepository>();
builder.Services.AddTransient<CategoryForGameRepository>();

builder.Services.AddTransient<DeveloperRepository>();
builder.Services.AddTransient<PublisherRepository>();

builder.Services.AddTransient<GameCommentRepository>();
builder.Services.AddTransient<GameGroupRepository>();
builder.Services.AddTransient<GameGuideRepository>();
builder.Services.AddTransient<GameNewsRepository>();
builder.Services.AddTransient<GamePostsRepository>();
builder.Services.AddTransient<GameTopicRepository>();

builder.Services.AddTransient<DLCInShopRepository>();
builder.Services.AddTransient<GameInShopRepository>();

builder.Services.AddTransient<GroupCommentRepository>();
builder.Services.AddTransient<GroupRepository>();
builder.Services.AddTransient<PostRepository>();
builder.Services.AddTransient<TopicRepository>();

builder.Services.AddTransient<LanguageRepository>();
builder.Services.AddTransient<LanguageInGameRepository>();

builder.Services.AddTransient<FriendsRepository>();
builder.Services.AddTransient<OwnedGameRepository>();
builder.Services.AddTransient<OwnedDlcRepository>();
builder.Services.AddTransient<ScreenshotRepository>();
builder.Services.AddTransient<UserCommentRepository>();
builder.Services.AddTransient<UserRepository>();
builder.Services.AddTransient<VideoRepository>();
builder.Services.AddTransient<WishedGameRepository>();

builder.Services.AddTransient<MaximumSystemRequirementRepository>();
builder.Services.AddTransient<MinimalSystemRequirementRepository>();

builder.Services.AddTransient<MessageRepository>();
builder.Services.AddTransient<ChatRepository>();

builder.Services.AddTransient<DiscussionRepository>();

builder.Services.AddTransient<CategoryByUserForGameRepository>();
builder.Services.AddTransient<UserCategoryRepository>();

builder.Services.AddTransient<SettingsRepository>();
builder.Services.AddTransient<WalletTransactionsRepository>();

builder.Services.AddTransient<AchievementByUserRepository>();
builder.Services.AddTransient<AchievementRepository>();

builder.Services.AddTransient<GameBundleCollectionRepository>();
builder.Services.AddTransient<GameBundleRepository>();

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
