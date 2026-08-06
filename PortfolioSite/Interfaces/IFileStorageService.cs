namespace PortfolioSite.Interfaces;

public interface IFileStorageService
{
    Task<string?> SaveImageAsync(IFormFile file, string subfolder, CancellationToken ct = default);
    Task<string?> SaveFileAsync(IFormFile file, string subfolder, string[] allowedExtensions, CancellationToken ct = default);
    Task DeleteFileAsync(string? relativePath);
    bool ValidateImageFile(IFormFile file, out string? error);
    bool ValidatePdfFile(IFormFile file, out string? error);
}
