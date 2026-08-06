using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PortfolioSite.Data;
using PortfolioSite.Entities;
using PortfolioSite.Helpers;
using PortfolioSite.Interfaces;

namespace PortfolioSite.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class ProjectsController : Controller
{
    private readonly ApplicationDbContext _db;
    private readonly IFileStorageService _files;

    public ProjectsController(ApplicationDbContext db, IFileStorageService files)
    {
        _db = db;
        _files = files;
    }

    public async Task<IActionResult> Index(CancellationToken ct)
        => View(await _db.Projects.OrderBy(p => p.SortOrder).ToListAsync(ct));

    [HttpGet] public IActionResult Create() => View(new Project());

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Project model, IFormFile? coverImage, CancellationToken ct)
    {
        ModelState.Remove("Slug");
        ModelState.Remove("Images");
        if (!ModelState.IsValid) return View(model);

        model.Slug = SlugHelper.Generate(model.Name);
        // Ensure unique slug
        var slugExists = await _db.Projects.AnyAsync(p => p.Slug == model.Slug, ct);
        if (slugExists) model.Slug = $"{model.Slug}-{DateTime.UtcNow.Ticks}";

        if (coverImage != null && coverImage.Length > 0)
            model.CoverImagePath = await _files.SaveImageAsync(coverImage, "projects", ct);

        model.CreatedAt = model.UpdatedAt = DateTime.UtcNow;
        _db.Projects.Add(model);
        await _db.SaveChangesAsync(ct);
        TempData["Success"] = "Proje başarıyla eklendi!";
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id, CancellationToken ct)
    {
        var project = await _db.Projects.FindAsync([id], ct);
        if (project == null) return NotFound();
        return View(project);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Project model, IFormFile? coverImage, CancellationToken ct)
    {
        var project = await _db.Projects.FindAsync([id], ct);
        if (project == null) return NotFound();

        project.Name = model.Name;
        project.Summary = model.Summary;
        project.Description = model.Description;
        project.ProblemStatement = model.ProblemStatement;
        project.Solution = model.Solution;
        project.MyRole = model.MyRole;
        project.Technologies = model.Technologies;
        project.GitHubUrl = model.GitHubUrl;
        project.LiveUrl = model.LiveUrl;
        project.Status = model.Status;
        project.IsFeatured = model.IsFeatured;
        project.IsActive = model.IsActive;
        project.SortOrder = model.SortOrder;
        project.UpdatedAt = DateTime.UtcNow;

        if (coverImage != null && coverImage.Length > 0)
        {
            await _files.DeleteFileAsync(project.CoverImagePath);
            project.CoverImagePath = await _files.SaveImageAsync(coverImage, "projects", ct);
        }

        await _db.SaveChangesAsync(ct);
        TempData["Success"] = "Proje güncellendi!";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id, CancellationToken ct)
    {
        var project = await _db.Projects.FindAsync([id], ct);
        if (project == null) return NotFound();
        project.IsDeleted = true;
        project.UpdatedAt = DateTime.UtcNow;
        await _db.SaveChangesAsync(ct);
        TempData["Success"] = "Proje silindi!";
        return RedirectToAction(nameof(Index));
    }
}
