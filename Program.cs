using CrmAssistant.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

string connString = builder.Configuration.GetConnectionString("DefaultConnection");
//ApplicationDbContext
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseMySql(connString, ServerVersion.AutoDetect(connString)));

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
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
