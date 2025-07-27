
using Application.Interfaces;
using Application.Services;
using Microsoft.EntityFrameworkCore;
using Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
// Connection String
builder.Services.AddDbContext<AdminDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("WalidConnection")));
//Dependency Injection
builder.Services.AddScoped<IAdmin, AdminService>();
builder.Services.AddScoped<ICategory, CategoryServices>();
builder.Services.AddScoped<ISubCategory, SubCategroyServices>();
builder.Services.AddScoped<IProduct, ProductServices>();
builder.Services.AddScoped<IImageService, ImageServices>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Admin}/{action=Login}/{id?}");

app.Run();
