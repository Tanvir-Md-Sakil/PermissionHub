using Microsoft.AspNetCore.Authentication.Cookies;
using PermissionHub;
using PermissionHub.seed;
using PermissionHub;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages(options =>
{
    options.Conventions.AuthorizeFolder("/");
    options.Conventions.AllowAnonymousToFolder("/Account");
});

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
builder.Services.AddScoped<PermissionHub.RoleRepository>();
builder.Services.AddScoped<AdminSeeder>();
builder.Services.AddScoped<CompanyRepository>();
builder.Services.AddScoped<UserRoleRepository>();
builder.Services.AddScoped<ModuleRepository>();
builder.Services.AddScoped<CompanyModuleRepository>();
builder.Services.AddScoped<BudgetRepository>();
builder.Services.AddScoped<PermissionService>();
builder.Services.AddScoped<RolePermissionRepository>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var seeder = scope.ServiceProvider.GetRequiredService<AdminSeeder>();
    await seeder.SeedAsync();
}

app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();