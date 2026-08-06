using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PortfolioSite.Data;

namespace PortfolioSite.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class DashboardController : Controller
{
    private readonly ApplicationDbContext _db;

    public DashboardController(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<IActionResult> Index(CancellationToken ct)
    {
        ViewBag.TotalProjects = await _db.Projects.CountAsync(ct);
        ViewBag.ActiveProjects = await _db.Projects.CountAsync(p => p.IsActive, ct);
        ViewBag.TotalBlogPosts = await _db.BlogPosts.CountAsync(ct);
        ViewBag.UnreadMessages = await _db.ContactMessages.CountAsync(m => !m.IsRead && !m.IsDeleted, ct);
        ViewBag.TotalCertificates = await _db.Certificates.CountAsync(ct);
        ViewBag.TotalSkills = await _db.Skills.CountAsync(ct);
        ViewBag.RecentMessages = await _db.ContactMessages
            .Where(m => !m.IsDeleted)
            .OrderByDescending(m => m.SentAt)
            .Take(5)
            .ToListAsync(ct);
        return View();
    }
}
