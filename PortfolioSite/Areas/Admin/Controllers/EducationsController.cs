using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PortfolioSite.Data;
using PortfolioSite.Entities;

namespace PortfolioSite.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class EducationsController : Controller
{
    private readonly ApplicationDbContext _db;
    public EducationsController(ApplicationDbContext db) => _db = db;

    public async Task<IActionResult> Index(CancellationToken ct)
        => View(await _db.Educations.OrderByDescending(e => e.StartDate).ToListAsync(ct));

    [HttpGet] 
    public IActionResult Create() => View(new Education { StartDate = DateTime.Today });

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Education model, CancellationToken ct)
    {
        if (!ModelState.IsValid) return View(model);
        model.CreatedAt = model.UpdatedAt = DateTime.UtcNow;
        _db.Educations.Add(model);
        await _db.SaveChangesAsync(ct);
        TempData["Success"] = "Eğitim bilgisi eklendi!";
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id, CancellationToken ct)
    {
        var edu = await _db.Educations.FindAsync([id], ct);
        if (edu == null) return NotFound();
        return View(edu);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Education model, CancellationToken ct)
    {
        var edu = await _db.Educations.FindAsync([id], ct);
        if (edu == null) return NotFound();
        edu.School = model.School;
        edu.Department = model.Department;
        edu.Degree = model.Degree;
        edu.StartDate = model.StartDate;
        edu.EndDate = model.EndDate;
        edu.IsCurrent = model.IsCurrent;
        edu.Description = model.Description;
        edu.IsActive = model.IsActive;
        edu.SortOrder = model.SortOrder;
        edu.UpdatedAt = DateTime.UtcNow;
        await _db.SaveChangesAsync(ct);
        TempData["Success"] = "Eğitim bilgisi güncellendi!";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id, CancellationToken ct)
    {
        var edu = await _db.Educations.FindAsync([id], ct);
        if (edu != null) { edu.IsDeleted = true; await _db.SaveChangesAsync(ct); }
        TempData["Success"] = "Eğitim bilgisi silindi!";
        return RedirectToAction(nameof(Index));
    }
}
