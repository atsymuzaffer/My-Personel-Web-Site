using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PortfolioSite.Data;
using PortfolioSite.Entities;
using PortfolioSite.Interfaces;

namespace PortfolioSite.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class EducationsController : Controller
{
    private readonly ApplicationDbContext _db;
    private readonly IFileStorageService _fileStorage;

    public EducationsController(ApplicationDbContext db, IFileStorageService fileStorage)
    {
        _db = db;
        _fileStorage = fileStorage;
    }

    public async Task<IActionResult> Index(CancellationToken ct)
        => View(await _db.Educations
            .OrderBy(e => e.SortOrder)
            .ThenByDescending(e => e.StartDate)
            .ToListAsync(ct));

    [HttpGet]
    public IActionResult Create() => View(new Education { StartDate = DateTime.Today });

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Education model, IFormFile? logoFile, CancellationToken ct)
    {
        // Business Rule 1: Force EndDate = null if IsCurrent is true
        if (model.IsCurrent)
        {
            model.EndDate = null;
        }
        else if (model.EndDate.HasValue && model.EndDate.Value < model.StartDate)
        {
            ModelState.AddModelError("EndDate", "Bitiş tarihi başlangıç tarihinden önce olamaz.");
        }

        if (!ModelState.IsValid) return View(model);

        // Upload Logo Image if provided
        if (logoFile != null && logoFile.Length > 0)
        {
            var logoPath = await _fileStorage.SaveImageAsync(logoFile, "educations", ct);
            if (!string.IsNullOrEmpty(logoPath))
                model.LogoPath = logoPath;
        }

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
    public async Task<IActionResult> Edit(int id, Education model, IFormFile? logoFile, CancellationToken ct)
    {
        var edu = await _db.Educations.FindAsync([id], ct);
        if (edu == null) return NotFound();

        // Business Rule 1: Force EndDate = null if IsCurrent is true
        if (model.IsCurrent)
        {
            model.EndDate = null;
        }
        else if (model.EndDate.HasValue && model.EndDate.Value < model.StartDate)
        {
            ModelState.AddModelError("EndDate", "Bitiş tarihi başlangıç tarihinden önce olamaz.");
        }

        if (!ModelState.IsValid) return View(model);

        // Upload new Logo Image if provided
        if (logoFile != null && logoFile.Length > 0)
        {
            var logoPath = await _fileStorage.SaveImageAsync(logoFile, "educations", ct);
            if (!string.IsNullOrEmpty(logoPath))
            {
                if (!string.IsNullOrEmpty(edu.LogoPath))
                    await _fileStorage.DeleteFileAsync(edu.LogoPath);

                edu.LogoPath = logoPath;
            }
        }

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
    public async Task<IActionResult> DeleteLogo(int id, CancellationToken ct)
    {
        var edu = await _db.Educations.FindAsync([id], ct);
        if (edu != null && !string.IsNullOrEmpty(edu.LogoPath))
        {
            await _fileStorage.DeleteFileAsync(edu.LogoPath);
            edu.LogoPath = null;
            await _db.SaveChangesAsync(ct);
            TempData["Success"] = "Okul logosu silindi!";
        }
        return RedirectToAction(nameof(Edit), new { id });
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id, CancellationToken ct)
    {
        var edu = await _db.Educations.FindAsync([id], ct);
        if (edu != null)
        {
            edu.IsDeleted = true;
            await _db.SaveChangesAsync(ct);
        }
        TempData["Success"] = "Eğitim bilgisi silindi!";
        return RedirectToAction(nameof(Index));
    }
}
