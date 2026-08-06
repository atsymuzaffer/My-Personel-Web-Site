using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PortfolioSite.Data;
using PortfolioSite.Entities;
using PortfolioSite.Interfaces;

namespace PortfolioSite.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class CertificatesController : Controller
{
    private readonly ApplicationDbContext _db;
    private readonly IFileStorageService _files;
    public CertificatesController(ApplicationDbContext db, IFileStorageService files) { _db = db; _files = files; }

    public async Task<IActionResult> Index(CancellationToken ct)
        => View(await _db.Certificates.OrderByDescending(c => c.IssuedDate).ToListAsync(ct));

    [HttpGet] public IActionResult Create() => View(new Certificate());

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Certificate model, IFormFile? image, CancellationToken ct)
    {
        if (!ModelState.IsValid) return View(model);
        if (image != null && image.Length > 0)
            model.ImagePath = await _files.SaveImageAsync(image, "certificates", ct);
        model.CreatedAt = model.UpdatedAt = DateTime.UtcNow;
        _db.Certificates.Add(model);
        await _db.SaveChangesAsync(ct);
        TempData["Success"] = "Sertifika eklendi!";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id, CancellationToken ct)
    {
        var cert = await _db.Certificates.FindAsync([id], ct);
        if (cert != null) { cert.IsDeleted = true; await _db.SaveChangesAsync(ct); }
        TempData["Success"] = "Sertifika silindi!"; return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id, CancellationToken ct)
    {
        var cert = await _db.Certificates.FindAsync([id], ct);
        if (cert == null) return NotFound();
        return View(cert);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Certificate model, IFormFile? image, CancellationToken ct)
    {
        if (id != model.Id) return NotFound();
        if (!ModelState.IsValid) return View(model);

        var existingCert = await _db.Certificates.FindAsync([id], ct);
        if (existingCert == null) return NotFound();

        existingCert.Name = model.Name;
        existingCert.IssuingOrganization = model.IssuingOrganization;
        existingCert.IssuedDate = model.IssuedDate;
        existingCert.ExpiryDate = model.ExpiryDate;
        existingCert.CredentialId = model.CredentialId;
        existingCert.VerificationUrl = model.VerificationUrl;
        existingCert.IsActive = model.IsActive;
        existingCert.UpdatedAt = DateTime.UtcNow;

        if (image != null && image.Length > 0)
        {
            if (!string.IsNullOrEmpty(existingCert.ImagePath))
                await _files.DeleteFileAsync(existingCert.ImagePath);
            
            existingCert.ImagePath = await _files.SaveImageAsync(image, "certificates", ct);
        }

        await _db.SaveChangesAsync(ct);
        TempData["Success"] = "Sertifika güncellendi!";
        return RedirectToAction(nameof(Index));
    }
}
