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

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View();
    }
}
