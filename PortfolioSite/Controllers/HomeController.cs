using Microsoft.AspNetCore.Mvc;
using PortfolioSite.Entities;
using PortfolioSite.Interfaces;
using PortfolioSite.Data;
using PortfolioSite.ViewModels;

namespace PortfolioSite.Controllers;

// ANTIGRAVITY DEĞİŞİKLİĞİ: Tüm public sayfa controller'ı yeniden yazıldı
public class HomeController : Controller
{
    private readonly IPortfolioService _portfolio;
    private readonly ApplicationDbContext _db;
    private readonly IWebHostEnvironment _env;
    private readonly ILogger<HomeController> _logger;

    public HomeController(
        IPortfolioService portfolio,
        ApplicationDbContext db,
        IWebHostEnvironment env,
        ILogger<HomeController> logger)
    {
        _portfolio = portfolio;
        _db = db;
        _env = env;
        _logger = logger;
    }

    public async Task<IActionResult> Index(CancellationToken ct)
    {
        var vm = new HomeViewModel
        {
            Profile = await _portfolio.GetProfileAsync(ct),
            SkillCategories = await _portfolio.GetSkillCategoriesWithSkillsAsync(ct),
            Experiences = await _portfolio.GetExperiencesAsync(ct),
            FeaturedProjects = await _portfolio.GetFeaturedProjectsAsync(ct),
            Educations = await _portfolio.GetEducationsAsync(ct),
            Certificates = await _portfolio.GetCertificatesAsync(ct),
            SocialLinks = await _portfolio.GetSocialLinksAsync(ct),
            RecentBlogPosts = await _portfolio.GetPublishedBlogPostsAsync(3, ct)
        };
        return View(vm);
    }

    public async Task<IActionResult> Project(string slug, CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(slug)) return NotFound();
        var project = await _portfolio.GetProjectBySlugAsync(slug, ct);
        if (project == null) return NotFound();
        return View(project);
    }

    public async Task<IActionResult> Projects(CancellationToken ct)
    {
        var projects = await _portfolio.GetAllProjectsAsync(ct);
        var profile = await _portfolio.GetProfileAsync(ct);
        ViewBag.Profile = profile;
        return View(projects);
    }

    [HttpGet]
    public IActionResult Contact()
    {
        return View(new ContactViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Contact(ContactViewModel vm, CancellationToken ct)
    {
        // Honeypot check
        if (!string.IsNullOrEmpty(vm.Website))
            return RedirectToAction(nameof(Index));

        if (!ModelState.IsValid)
            return View(vm);

        var message = new ContactMessage
        {
            FullName = vm.FullName,
            Email = vm.Email,
            Subject = vm.Subject,
            Message = vm.Message,
            SentAt = DateTime.UtcNow,
            IpAddress = HttpContext.Connection.RemoteIpAddress?.ToString(),
            UserAgent = Request.Headers.UserAgent.ToString()
        };

        _db.ContactMessages.Add(message);
        await _db.SaveChangesAsync(ct);

        TempData["Success"] = "Mesajınız başarıyla gönderildi. En kısa sürede dönüş yapacağım!";
        return RedirectToAction(nameof(Contact));
    }

    public async Task<IActionResult> DownloadCv(CancellationToken ct)
    {
        var cvPath = await _portfolio.IncrementCvDownloadAsync(ct);
        if (string.IsNullOrEmpty(cvPath))
        {
            TempData["Error"] = "Henüz bir CV dosyası yüklenmemiş. Lütfen daha sonra tekrar deneyin veya İletişim bölümünden mesaj gönderin.";
            return RedirectToAction(nameof(Index));
        }

        var fullPath = Path.Combine(_env.WebRootPath, cvPath.TrimStart('/'));
        if (!System.IO.File.Exists(fullPath))
        {
            TempData["Error"] = "CV dosyası sunucuda bulunamadı. Lütfen Admin panelinden yeni bir CV yükleyin.";
            return RedirectToAction(nameof(Index));
        }

        var bytes = await System.IO.File.ReadAllBytesAsync(fullPath, ct);
        return File(bytes, "application/pdf", "CV.pdf");
    }

    [Route("favicon.ico")]
    [Route("favicon.svg")]
    [ResponseCache(Duration = 3600, Location = ResponseCacheLocation.Any)]
    public async Task<IActionResult> Favicon(CancellationToken ct)
    {
        var profile = await _portfolio.GetProfileAsync(ct);
        var name = profile?.FullName?.Trim() ?? "MA";
        
        string initials = "MA";
        if (!string.IsNullOrWhiteSpace(name))
        {
            var parts = name.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length == 1)
            {
                initials = parts[0].Length >= 2 
                    ? parts[0].Substring(0, 2).ToUpperInvariant() 
                    : parts[0].ToUpperInvariant();
            }
            else if (parts.Length >= 2)
            {
                initials = (parts[0][0].ToString() + parts[^1][0].ToString()).ToUpperInvariant();
            }
        }

        var fontSize = initials.Length > 2 ? "36" : "44";

        var svg = $@"<svg xmlns=""http://www.w3.org/2000/svg"" viewBox=""0 0 100 100"">
  <rect width=""100"" height=""100"" rx=""24"" fill=""#0B0D10""/>
  <rect width=""94"" height=""94"" x=""3"" y=""3"" rx=""22"" fill=""none"" stroke=""#397BFF"" stroke-width=""4"" opacity=""0.4""/>
  <text x=""50"" y=""55"" font-family=""-apple-system, BlinkMacSystemFont, 'Segoe UI', Roboto, sans-serif"" font-size=""{fontSize}"" font-weight=""800"" fill=""#397BFF"" text-anchor=""middle"" dominant-baseline=""central"">{initials}</text>
</svg>";

        return Content(svg, "image/svg+xml", System.Text.Encoding.UTF8);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View();
    }
}
