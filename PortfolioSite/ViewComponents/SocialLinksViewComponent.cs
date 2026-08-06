using Microsoft.AspNetCore.Mvc;
using PortfolioSite.Data;
using Microsoft.EntityFrameworkCore;

namespace PortfolioSite.ViewComponents;

public class SocialLinksViewComponent : ViewComponent
{
    private readonly ApplicationDbContext _db;
    public SocialLinksViewComponent(ApplicationDbContext db) => _db = db;

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var links = await _db.SocialLinks
            .AsNoTracking()
            .Where(s => s.IsActive && !s.IsDeleted)
            .OrderBy(s => s.SortOrder)
            .ToListAsync();
        return View(links);
    }
}
