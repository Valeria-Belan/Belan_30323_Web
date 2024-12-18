using Belan_30323.Api.Data;
using Belan_30323.UI.Services;
using Belan_30323.UI.Services.ApiServices;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var conString = builder.Configuration.GetConnectionString("Default");

// Add services to the container.

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlite(conString);
});



////Зарегистрируйте сервис ApiCategoryService как scoped сервис в классе Program
//builder.Services.AddHttpClient<IApiCategoryService, ApiCategoryService>(opt
//=> opt.BaseAddress = new Uri("https://localhost:7005/api/categories/"));

////Зарегистрируйте сервис ApiProductService как scoped сервис в классе Program
//builder.Services.AddHttpClient<IApiProductService, ApiProductService>(opt
//=> opt.BaseAddress = new Uri("https://localhost:7005/api/dishes/"));

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// Получение контекста БД
using var scope = app.Services.CreateScope();
var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

// Выполнение миграций
await context.Database.MigrateAsync();

await DbInitializer.SeedData(app);

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAuthorization();

app.Run();
