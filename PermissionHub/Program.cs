using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Connections;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

builder.Services.AddAuthentication("Cookies")
.AddCookie("Cookies", options =>
{
    options.LoginPath = "/Account/Login";
    options.AccessDeniedPath = "/Account/Denied";
});

builder.Services.AddAuthorization();

// Dapper
builder.Services.AddScoped<PermissionHub.IDbConnectionFactory, PermissionHub.SqlConnectionFactory>();

builder.Services.AddScoped<PermissionHub.UserRepository>();
builder.Services.AddScoped<PermissionHub.PermissionService>();

var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();