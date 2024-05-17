<<<<<<< HEAD
using Slush.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
=======
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using Slush.DAO.GameInShopDao;
using Slush.DAO.CategoriesDao;
using Slush.Data;
using Slush.DAO.CreatorsDao;
using Slush.DAO.GameGroupDao;
using Slush.DAO.GroupDao;
using Slush.Data.Entity;
using Slush.DAO.LanguageDao;
using Slush.Entity.Profile;
using Slush.DAO.ProfileDao;
using Slush.DAO.RequirementsDao;
using Slush.DAO.ChatDao;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("MsSqlDb") ?? throw new InvalidOperationException("Connection String 'MsSqlDb' not found.");
builder.Services.AddDbContext<DataContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

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


builder.Services.AddCors(p => p.AddPolicy("corsapp", builder =>
{
    builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));

var app = builder.Build();

app.UseCors("corsapp");

app.UseDefaultFiles();
app.UseStaticFiles();


>>>>>>> development_branch
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
<<<<<<< HEAD
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
=======
>>>>>>> development_branch
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
