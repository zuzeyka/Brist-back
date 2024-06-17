using Microsoft.EntityFrameworkCore;
using Slush.DAO.GameInShopDao;
using Slush.DAO.CategoriesDao;
using Slush.Data;
using Slush.DAO.CreatorsDao;
using Slush.DAO.GameGroupDao;
using Slush.DAO.GroupDao;
using Slush.DAO.LanguageDao;
using Slush.DAO.ProfileDao;
using Slush.DAO.RequirementsDao;
using Slush.DAO.ChatDao;
using Slush.DAO;
using Slush.Services.JWT;
using Slush.Services.RegistrationValidation;
using Slush.Services.Hash;
using Minio;
using Slush.Services.Minio;

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
builder.Services.AddScoped<MinioService>();
builder.Services.AddScoped<JWTService>();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "API",
        Version = "V1"
    });
});

builder.Services.AddRazorPages();

builder.Services.AddTransient<CategoriesDAO>();
builder.Services.AddTransient<CategoriesByAuthorDao>();
builder.Services.AddTransient<CategoriesByUserDao>();
builder.Services.AddTransient<CategoryForGameDao>();

builder.Services.AddTransient<DeveloperDao>();
builder.Services.AddTransient<PublisherDao>();

builder.Services.AddTransient<GameCommentDao>();
builder.Services.AddTransient<GameGroupDao>();
builder.Services.AddTransient<GameGuideDao>();
builder.Services.AddTransient<GameNewsDao>();
builder.Services.AddTransient<GamePostsDao>();
builder.Services.AddTransient<GameTopicDao>();

builder.Services.AddTransient<DLCInShopDao>();
builder.Services.AddTransient<GameInShopDao>();

builder.Services.AddTransient<GroupCommentDao>();
builder.Services.AddTransient<GroupDao>();
builder.Services.AddTransient<PostDao>();
builder.Services.AddTransient<TopicDao>();

builder.Services.AddTransient<LanguageDao>();
builder.Services.AddTransient<LanguageInGameDao>();

builder.Services.AddTransient<FriendsDao>();
builder.Services.AddTransient<OwnedGameDao>();
builder.Services.AddTransient<ScreenshotDao>();
builder.Services.AddTransient<UserCommentDao>();
builder.Services.AddTransient<UserDao>();
builder.Services.AddTransient<VideoDao>();
builder.Services.AddTransient<WishedGameDao>();

builder.Services.AddTransient<MaximumSystemRequirementDao>();
builder.Services.AddTransient<MinimalSystemRequirementDao>();

builder.Services.AddTransient<MessageDao>();
builder.Services.AddTransient<ChatDao>();

builder.Services.AddTransient<DiscussionDao>();

builder.Services.AddTransient<CategoryByUserForGameDao>();
builder.Services.AddTransient<UserCategoryDao>();

builder.Services.AddTransient<SettingsDao>();
builder.Services.AddTransient<WalletTransactionsDao>();

builder.Services.AddTransient<AchievementByUserDao>();
builder.Services.AddTransient<AchievementDao>();

builder.Services.AddTransient<GameBundleCollectionDao>();
builder.Services.AddTransient<GameBundleDao>();


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
