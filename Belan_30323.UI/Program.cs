using Belan_30323.UI.Data;
using Belan_30323.UI.Services;
using Belan_30323.UI.Services.Abstraction;
using Belan_30323.UI.Services.ApiServices;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("SqliteConnetion") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(connectionString));

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(connectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<AppUser>(options =>
    {
        options.SignIn.RequireConfirmedAccount = true;
        options.Password.RequireDigit = false;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireLowercase = false;
        options.Password.RequireUppercase = false;
    }
)
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddAuthorization(opt =>
{
    opt.AddPolicy("admin", p =>
    p.RequireClaim(ClaimTypes.Role, "admin"));
});

builder.Services.AddSingleton<IEmailSender, NoOpEmailSender>();

////Зарегистрируйте сервис ICategoryService как scoped сервис в классе Program
//builder.Services.AddScoped<ICategoryService, MemoryCategoryService>();

////Зарегистрируйте сервис IProductService как scoped сервис в классе Program
//builder.Services.AddScoped<IProductService, MemoryProductService>();

//Зарегистрируйте сервис ApiCategoryService как scoped сервис в классе Program
builder.Services.AddHttpClient<ICategoryService, ApiCategoryService>(opt
=> opt.BaseAddress = new Uri("https://localhost:7005/api/categories/"));

//Зарегистрируйте сервис ApiProductService как scoped сервис в классе Program
builder.Services.AddHttpClient<IProductService, ApiProductService>(opt
=> opt.BaseAddress = new Uri("https://localhost:7005/api/dishes/"));

builder.Services.AddControllersWithViews();

//builder.Services.AddTransient<IEmailSender, NoOpEmailSender>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

await DbInit.SeedData(app);

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();
