using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PortfolioSite.Data;
using PortfolioSite.Entities;

namespace PortfolioSite.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class ExperiencesController : Controller
{
    private readonly ApplicationDbContext _db;
    public ExperiencesController(ApplicationDbContext db) => _db = db;

    public async Task<IActionResult> Index(CancellationToken ct)
        => View(await _db.Experiences.OrderByDescending(e => e.StartDate).ToListAsync(ct));

    [HttpGet] public IActionResult Create() => View(new Experience());

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Experience model, CancellationToken ct)
    {
        if (!ModelState.IsValid) return View(model);
        model.CreatedAt = model.UpdatedAt = DateTime.UtcNow;
        _db.Experiences.Add(model);
        await _db.SaveChangesAsync(ct);
        TempData["Success"] = "Deneyim eklendi!";
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id, CancellationToken ct)
    {
        var exp = await _db.Experiences.FindAsync([id], ct);
        if (exp == null) return NotFound();
        return View(exp);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Experience model, CancellationToken ct)
    {
        var exp = await _db.Experiences.FindAsync([id], ct);
        if (exp == null) return NotFound();
        exp.Position = model.Position; exp.Company = model.Company;
        exp.HideCompanyName = model.HideCompanyName; exp.Location = model.Location;
        exp.StartDate = model.StartDate; exp.EndDate = model.EndDate;
        exp.IsCurrentJob = model.IsCurrentJob; exp.Description = model.Description;
        exp.Technologies = model.Technologies; exp.IsActive = model.IsActive;
        exp.UpdatedAt = DateTime.UtcNow;
        await _db.SaveChangesAsync(ct);
        TempData["Success"] = "Deneyim güncellendi!";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id, CancellationToken ct)
    {
        var exp = await _db.Experiences.FindAsync([id], ct);
        if (exp != null) { exp.IsDeleted = true; await _db.SaveChangesAsync(ct); }
        TempData["Success"] = "Deneyim silindi!"; return RedirectToAction(nameof(Index));
    }
}
