using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PortfolioSite.Data;
using PortfolioSite.Interfaces;
using PortfolioSite.Services;

var builder = WebApplication.CreateBuilder(args);

// Database
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

// Identity
builder.Services.AddDefaultIdentity<PortfolioSite.Entities.ApplicationUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 8;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = false;
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
    options.Lockout.MaxFailedAccessAttempts = 5;
})
.AddRoles<IdentityRole>()
.AddEntityFrameworkStores<ApplicationDbContext>();

// Cookie
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Admin/Account/Login";
    options.LogoutPath = "/Admin/Account/Logout";
    options.AccessDeniedPath = "/Admin/Account/AccessDenied";
    options.Cookie.HttpOnly = true;
    options.Cookie.SameSite = SameSiteMode.Lax;
    options.ExpireTimeSpan = TimeSpan.FromHours(8);
});

// Services
builder.Services.AddScoped<IPortfolioService, PortfolioService>();
builder.Services.AddScoped<IFileStorageService, LocalFileStorageService>();

builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add<PortfolioSite.Filters.MustChangeCredentialsFilter>();
});
builder.Services.AddRazorPages();

// Anti-forgery
builder.Services.AddAntiforgery(options =>
{
    options.Cookie.Name = "XSRF-TOKEN";
    options.Cookie.HttpOnly = true;
});

var app = builder.Build();

// Auto-migrate and seed admin user on startup
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    db.Database.Migrate();

    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<PortfolioSite.Entities.ApplicationUser>>();
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

    // Create Admin role if not exists
    if (!await roleManager.RoleExistsAsync("Admin"))
        await roleManager.CreateAsync(new IdentityRole("Admin"));

    // Seed default demo admin user ONLY if NO users exist at all in database
    if (!await userManager.Users.AnyAsync())
    {
        var adminEmail = "admin@example.com";
        var adminUser = new PortfolioSite.Entities.ApplicationUser
        {
            UserName = adminEmail,
            Email = adminEmail,
            EmailConfirmed = true,
            MustChangeCredentials = true
        };
        var result = await userManager.CreateAsync(adminUser, "Admin@2025!");
        if (result.Succeeded)
            await userManager.AddToRoleAsync(adminUser, "Admin");
    }
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

// Admin area route
app.MapControllerRoute(
    name: "admin",
    pattern: "Admin/{controller=Dashboard}/{action=Index}/{id?}",
    defaults: new { area = "Admin" },
    constraints: new { area = "Admin" });

app.MapControllerRoute(
    name: "project",
    pattern: "projeler/{slug}",
    defaults: new { controller = "Home", action = "Project" });

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Dynamic Favicon route for browser tab requests (/favicon.ico)
app.MapGet("/favicon.ico", async (PortfolioSite.Interfaces.IPortfolioService portfolio, IWebHostEnvironment env) =>
{
    var profile = await portfolio.GetProfileAsync();
    if (!string.IsNullOrEmpty(profile?.FaviconPath))
    {
        var physicalPath = Path.Combine(env.WebRootPath, profile.FaviconPath.TrimStart('/'));
        if (File.Exists(physicalPath))
        {
            var ext = Path.GetExtension(physicalPath).ToLowerInvariant();
            var mime = ext == ".png" ? "image/png" : ext == ".svg" ? "image/svg+xml" : ext == ".webp" ? "image/webp" : "image/x-icon";
            return Results.File(physicalPath, mime);
        }
    }
    
    var name = profile?.FullName ?? "Muzaffer Atasoy";
    var initials = "MA";
    var parts = name.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);
    if (parts.Length == 1)
        initials = parts[0].Length >= 2 ? parts[0][..2].ToUpper() : parts[0].ToUpper();
    else if (parts.Length >= 2)
        initials = $"{parts[0][0]}{parts[^1][0]}".ToUpper();

    var fontSize = initials.Length > 2 ? "36" : "44";
    var svg = $@"<svg xmlns=""http://www.w3.org/2000/svg"" viewBox=""0 0 100 100"">
        <rect width=""100"" height=""100"" rx=""24"" fill=""#0F1115""/>
        <rect width=""94"" height=""94"" x=""3"" y=""3"" rx=""22"" fill=""none"" stroke=""#178A90"" stroke-width=""4"" opacity=""0.4""/>
        <text x=""50"" y=""55"" font-family=""-apple-system, BlinkMacSystemFont, sans-serif"" font-size=""{fontSize}"" font-weight=""800"" fill=""#178A90"" text-anchor=""middle"" dominant-baseline=""central"">{initials}</text>
    </svg>";
    return Results.Content(svg, "image/svg+xml");
});

app.MapRazorPages();

app.Run();
