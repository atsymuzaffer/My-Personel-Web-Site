using PortfolioSite.Interfaces;

namespace PortfolioSite.Services;

// ANTIGRAVITY DEĞİŞİKLİĞİ: Eski text-input dosya yolu sistemi yerine gerçek dosya yükleme servisi
public class LocalFileStorageService : IFileStorageService
{
    private readonly IWebHostEnvironment _env;
    private readonly ILogger<LocalFileStorageService> _logger;
    private const long MaxImageSizeBytes = 5 * 1024 * 1024; // 5MB
    private const long MaxFileSizeBytes = 10 * 1024 * 1024; // 10MB
    private static readonly string[] AllowedImageExtensions = [".jpg", ".jpeg", ".png", ".webp"];
    private static readonly string[] AllowedImageMimeTypes = ["image/jpeg", "image/png", "image/webp"];

    public LocalFileStorageService(IWebHostEnvironment env, ILogger<LocalFileStorageService> logger)
    {
        _env = env;
        _logger = logger;
    }

    public async Task<string?> SaveImageAsync(IFormFile file, string subfolder, CancellationToken ct = default)
    {
        if (!ValidateImageFile(file, out var error))
        {
            _logger.LogWarning("Invalid image file: {Error}", error);
            return null;
        }
        return await SaveFileAsync(file, subfolder, AllowedImageExtensions, ct);
    }

    public async Task<string?> SaveFileAsync(IFormFile file, string subfolder, string[] allowedExtensions, CancellationToken ct = default)
    {
        try
        {
            var ext = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (!allowedExtensions.Contains(ext)) return null;

            var uploadsDir = Path.Combine(_env.WebRootPath, "uploads", subfolder);
            Directory.CreateDirectory(uploadsDir);

            var fileName = $"{Guid.NewGuid()}{ext}";
            var filePath = Path.Combine(uploadsDir, fileName);

            await using var stream = new FileStream(filePath, FileMode.Create);
            await file.CopyToAsync(stream, ct);

            return $"/uploads/{subfolder}/{fileName}";
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "File save error");
            return null;
        }
    }

    public Task DeleteFileAsync(string? relativePath)
    {
        if (string.IsNullOrEmpty(relativePath)) return Task.CompletedTask;
        try
        {
            var fullPath = Path.Combine(_env.WebRootPath, relativePath.TrimStart('/'));
            if (File.Exists(fullPath)) File.Delete(fullPath);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "File delete error: {Path}", relativePath);
        }
        return Task.CompletedTask;
    }

    public bool ValidateImageFile(IFormFile file, out string? error)
    {
        error = null;
        if (file.Length > MaxImageSizeBytes) { error = "Dosya boyutu 5MB'dan büyük olamaz."; return false; }
        var ext = Path.GetExtension(file.FileName).ToLowerInvariant();
        if (!AllowedImageExtensions.Contains(ext)) { error = "Yalnızca JPG, PNG ve WebP dosyaları kabul edilmektedir."; return false; }
        if (!AllowedImageMimeTypes.Contains(file.ContentType.ToLowerInvariant())) { error = "Geçersiz dosya türü."; return false; }
        return true;
    }

    public bool ValidatePdfFile(IFormFile file, out string? error)
    {
        error = null;
        if (file.Length > MaxFileSizeBytes) { error = "CV dosyası 10MB'dan büyük olamaz."; return false; }
        var ext = Path.GetExtension(file.FileName).ToLowerInvariant();
        if (ext != ".pdf") { error = "Yalnızca PDF dosyaları kabul edilmektedir."; return false; }
        if (!file.ContentType.Equals("application/pdf", StringComparison.OrdinalIgnoreCase)) { error = "Geçersiz dosya türü."; return false; }
        return true;
    }

    public bool ValidateFaviconFile(IFormFile file, out string? error)
    {
        error = null;
        if (file.Length > 2 * 1024 * 1024) { error = "Favicon dosyası 2MB'dan büyük olamaz."; return false; }
        var ext = Path.GetExtension(file.FileName).ToLowerInvariant();
        string[] allowed = [".ico", ".png", ".svg", ".webp"];
        if (!allowed.Contains(ext)) { error = "Favicon için yalnızca ICO, PNG, SVG veya WebP dosyaları kabul edilir."; return false; }
        return true;
    }

    public bool ValidateLogoFile(IFormFile file, out string? error)
    {
        error = null;
        if (file.Length > MaxImageSizeBytes) { error = "Logo dosyası 5MB'dan büyük olamaz."; return false; }
        var ext = Path.GetExtension(file.FileName).ToLowerInvariant();
        string[] allowed = [".png", ".svg", ".jpg", ".jpeg", ".webp"];
        if (!allowed.Contains(ext)) { error = "Logo için yalnızca PNG, SVG, JPG veya WebP dosyaları kabul edilir."; return false; }
        return true;
    }
}
