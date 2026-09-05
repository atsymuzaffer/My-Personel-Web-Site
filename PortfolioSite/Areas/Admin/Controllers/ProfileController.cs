using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PortfolioSite.Data;
using PortfolioSite.Entities;
using PortfolioSite.Interfaces;

namespace PortfolioSite.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class ProfileController : Controller
{
    private readonly ApplicationDbContext _db;
    private readonly IFileStorageService _files;

    public ProfileController(ApplicationDbContext db, IFileStorageService files)
    {
        _db = db;
        _files = files;
    }

    public async Task<IActionResult> Index(CancellationToken ct)
    {
        var profile = await _db.SiteProfiles.FirstOrDefaultAsync(ct);
        return View(profile);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Index(SiteProfile model, IFormFile? profileImage, IFormFile? cvFile, IFormFile? faviconFile, IFormFile? logoFile, CancellationToken ct)
    {
        var profile = await _db.SiteProfiles.FirstOrDefaultAsync(ct);
        if (profile == null) { TempData["Error"] = "Profil bulunamadı."; return RedirectToAction(nameof(Index)); }

        profile.FullName = model.FullName;
        profile.Title = model.Title;
        profile.ShortBio = model.ShortBio;
        profile.AboutText = model.AboutText;
        profile.Email = model.Email;
        profile.GitHubUrl = model.GitHubUrl;
        profile.LinkedInUrl = model.LinkedInUrl;
        profile.WebsiteUrl = model.WebsiteUrl;
        profile.Location = model.Location;
        profile.SpecializationLabel = model.SpecializationLabel;
        profile.Specialization = model.Specialization;
        profile.FocusAreaLabel = model.FocusAreaLabel;
        profile.FocusArea = model.FocusArea;
        profile.PrimaryTechsLabel = model.PrimaryTechsLabel;
        profile.PrimaryTechs = model.PrimaryTechs;
        profile.DatabaseSkillsLabel = model.DatabaseSkillsLabel;
        profile.DatabaseSkills = model.DatabaseSkills;
        profile.BusinessProcessesLabel = model.BusinessProcessesLabel;
        profile.BusinessProcesses = model.BusinessProcesses;
        profile.IsAvailableForWork = model.IsAvailableForWork;
        profile.HeroBadgeText = model.HeroBadgeText;
        profile.MetaDescription = model.MetaDescription;
        profile.CompletedProjects = model.CompletedProjects;
        profile.YearsOfExperience = model.YearsOfExperience;
        profile.UpdatedAt = DateTime.UtcNow;

        if (profileImage != null && profileImage.Length > 0)
        {
            if (_files.ValidateImageFile(profileImage, out var imgError))
            {
                await _files.DeleteFileAsync(profile.ProfileImagePath);
                profile.ProfileImagePath = await _files.SaveImageAsync(profileImage, "profiles", ct);
            }
            else { TempData["Error"] = imgError; return View(profile); }
        }

        if (cvFile != null && cvFile.Length > 0)
        {
            if (_files.ValidatePdfFile(cvFile, out var pdfError))
            {
                await _files.DeleteFileAsync(profile.CvFilePath);
                profile.CvFilePath = await _files.SaveFileAsync(cvFile, "cv", [".pdf"], ct);
            }
            else { TempData["Error"] = pdfError; return View(profile); }
        }

        if (faviconFile != null && faviconFile.Length > 0)
        {
            if (_files.ValidateFaviconFile(faviconFile, out var favError))
            {
                await _files.DeleteFileAsync(profile.FaviconPath);
                profile.FaviconPath = await _files.SaveFileAsync(faviconFile, "branding", [".ico", ".png", ".svg", ".webp"], ct);
            }
            else { TempData["Error"] = favError; return View(profile); }
        }

        if (logoFile != null && logoFile.Length > 0)
        {
            if (_files.ValidateLogoFile(logoFile, out var logoError))
            {
                await _files.DeleteFileAsync(profile.LogoPath);
                profile.LogoPath = await _files.SaveFileAsync(logoFile, "branding", [".png", ".svg", ".jpg", ".jpeg", ".webp"], ct);
            }
            else { TempData["Error"] = logoError; return View(profile); }
        }

        await _db.SaveChangesAsync(ct);
        TempData["Success"] = "Profil başarıyla güncellendi!";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteCv(CancellationToken ct)
    {
        var profile = await _db.SiteProfiles.FirstOrDefaultAsync(ct);
        if (profile != null && !string.IsNullOrEmpty(profile.CvFilePath))
        {
            await _files.DeleteFileAsync(profile.CvFilePath);
            profile.CvFilePath = null;
            profile.UpdatedAt = DateTime.UtcNow;
            await _db.SaveChangesAsync(ct);
            TempData["Success"] = "CV dosyası başarıyla silindi!";
        }
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteProfileImage(CancellationToken ct)
    {
        var profile = await _db.SiteProfiles.FirstOrDefaultAsync(ct);
        if (profile != null && !string.IsNullOrEmpty(profile.ProfileImagePath))
        {
            await _files.DeleteFileAsync(profile.ProfileImagePath);
            profile.ProfileImagePath = null;
            profile.UpdatedAt = DateTime.UtcNow;
            await _db.SaveChangesAsync(ct);
            TempData["Success"] = "Profil fotoğrafı başarıyla silindi!";
        }
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteFavicon(CancellationToken ct)
    {
        var profile = await _db.SiteProfiles.FirstOrDefaultAsync(ct);
        if (profile != null && !string.IsNullOrEmpty(profile.FaviconPath))
        {
            await _files.DeleteFileAsync(profile.FaviconPath);
            profile.FaviconPath = null;
            profile.UpdatedAt = DateTime.UtcNow;
            await _db.SaveChangesAsync(ct);
            TempData["Success"] = "Favicon başarıyla silindi! Otomatik oluşturulan ikona dönüldü.";
        }
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteLogo(CancellationToken ct)
    {
        var profile = await _db.SiteProfiles.FirstOrDefaultAsync(ct);
        if (profile != null && !string.IsNullOrEmpty(profile.LogoPath))
        {
            await _files.DeleteFileAsync(profile.LogoPath);
            profile.LogoPath = null;
            profile.UpdatedAt = DateTime.UtcNow;
            await _db.SaveChangesAsync(ct);
            TempData["Success"] = "Logo başarıyla silindi! İsim ve soyisim metnine dönüldü.";
        }
        return RedirectToAction(nameof(Index));
    }
}
