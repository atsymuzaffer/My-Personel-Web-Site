using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PortfolioSite.Data;
using PortfolioSite.Entities;

namespace PortfolioSite.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class SkillsController : Controller
{
    private readonly ApplicationDbContext _db;
    public SkillsController(ApplicationDbContext db) => _db = db;

    public async Task<IActionResult> Index(CancellationToken ct)
        => View(await _db.Skills.Include(s => s.Category).OrderBy(s => s.SortOrder).ToListAsync(ct));

    [HttpGet]
    public async Task<IActionResult> Create(CancellationToken ct)
    {
        ViewBag.Categories = await _db.SkillCategories.Where(c => c.IsActive).ToListAsync(ct);
        return View(new Skill());
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Skill model, CancellationToken ct)
    {
        ModelState.Remove("Category");
        if (!ModelState.IsValid)
        {
            ViewBag.Categories = await _db.SkillCategories.Where(c => c.IsActive).ToListAsync(ct);
            return View(model);
        }
        model.CreatedAt = model.UpdatedAt = DateTime.UtcNow;
        _db.Skills.Add(model);
        await _db.SaveChangesAsync(ct);
        TempData["Success"] = "Yetenek eklendi!";
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id, CancellationToken ct)
    {
        var skill = await _db.Skills.FindAsync([id], ct);
        if (skill == null) return NotFound();
        ViewBag.Categories = await _db.SkillCategories.Where(c => c.IsActive).ToListAsync(ct);
        return View(skill);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Skill model, CancellationToken ct)
    {
        var skill = await _db.Skills.FindAsync([id], ct);
        if (skill == null) return NotFound();
        skill.Name = model.Name; skill.Description = model.Description;
        skill.Level = model.Level; skill.IsFeatured = model.IsFeatured;
        skill.SkillCategoryId = model.SkillCategoryId; skill.SortOrder = model.SortOrder;
        skill.IsActive = model.IsActive; skill.UpdatedAt = DateTime.UtcNow;
        await _db.SaveChangesAsync(ct);
        TempData["Success"] = "Yetenek güncellendi!";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id, CancellationToken ct)
    {
        var skill = await _db.Skills.FindAsync([id], ct);
        if (skill != null) { skill.IsDeleted = true; await _db.SaveChangesAsync(ct); }
        TempData["Success"] = "Yetenek silindi!"; return RedirectToAction(nameof(Index));
    }
}
