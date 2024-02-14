using CrossCutting.Configuration;
using Service;
using Repository;
using Microsoft.AspNetCore.Authentication.Cookies;
using CrossCutting;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

IConfiguration configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables()
    .Build();

var settings = builder.Configuration.Get<AppSettings>();
builder.Services.AddSingleton(settings);

builder.Services.AddCrossCutting();
builder.Services.AddServices();
builder.Services.AddRepositories();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(option =>
    {
        option.LoginPath = "/Login/Index";
        option.ExpireTimeSpan = TimeSpan.FromMinutes(60);
    });

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
