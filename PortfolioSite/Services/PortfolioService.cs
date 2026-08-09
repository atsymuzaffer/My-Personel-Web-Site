using Microsoft.EntityFrameworkCore;
using PortfolioSite.Data;
using PortfolioSite.Entities;
using PortfolioSite.Interfaces;

namespace PortfolioSite.Services;

public class PortfolioService : IPortfolioService
{
    private readonly ApplicationDbContext _db;

    public PortfolioService(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<SiteProfile?> GetProfileAsync(CancellationToken ct = default)
        => await _db.SiteProfiles.AsNoTracking().FirstOrDefaultAsync(ct);

    public async Task<List<SkillCategory>> GetSkillCategoriesWithSkillsAsync(CancellationToken ct = default)
        => await _db.SkillCategories
            .AsNoTracking()
            .Where(c => c.IsActive && !c.IsDeleted)
            .OrderBy(c => c.SortOrder)
            .Include(c => c.Skills.Where(s => s.IsActive && !s.IsDeleted))
            .ToListAsync(ct);

    public async Task<List<Skill>> GetSkillsByCategoryAsync(CancellationToken ct = default)
        => await _db.Skills
            .AsNoTracking()
            .Where(s => s.IsActive)
            .OrderBy(s => s.SortOrder)
            .Include(s => s.Category)
            .ToListAsync(ct);

    public async Task<List<Experience>> GetExperiencesAsync(CancellationToken ct = default)
        => await _db.Experiences
            .AsNoTracking()
            .Where(e => e.IsActive)
            .OrderByDescending(e => e.StartDate)
            .ToListAsync(ct);

    public async Task<List<Project>> GetFeaturedProjectsAsync(CancellationToken ct = default)
        => await _db.Projects
            .AsNoTracking()
            .Where(p => p.IsActive && p.IsFeatured)
            .OrderBy(p => p.SortOrder)
            .ToListAsync(ct);

    public async Task<List<Project>> GetAllProjectsAsync(CancellationToken ct = default)
        => await _db.Projects
            .AsNoTracking()
            .Where(p => p.IsActive)
            .OrderBy(p => p.SortOrder)
            .ToListAsync(ct);

    public async Task<Project?> GetProjectBySlugAsync(string slug, CancellationToken ct = default)
        => await _db.Projects
            .AsNoTracking()
            .Include(p => p.Images)
            .FirstOrDefaultAsync(p => p.Slug == slug && p.IsActive, ct);

    public async Task<List<Education>> GetEducationsAsync(CancellationToken ct = default)
        => await _db.Educations
            .AsNoTracking()
            .Where(e => e.IsActive)
            .OrderBy(e => e.SortOrder)
            .ThenByDescending(e => e.StartDate)
            .ToListAsync(ct);

    public async Task<List<Certificate>> GetCertificatesAsync(CancellationToken ct = default)
        => await _db.Certificates
            .AsNoTracking()
            .Where(c => c.IsActive)
            .OrderByDescending(c => c.IssuedDate)
            .ToListAsync(ct);

    public async Task<List<SocialLink>> GetSocialLinksAsync(CancellationToken ct = default)
        => await _db.SocialLinks
            .AsNoTracking()
            .Where(s => s.IsActive)
            .OrderBy(s => s.SortOrder)
            .ToListAsync(ct);

    public async Task<List<BlogPost>> GetPublishedBlogPostsAsync(int count = 3, CancellationToken ct = default)
        => await _db.BlogPosts
            .AsNoTracking()
            .Where(b => b.IsActive && b.IsPublished)
            .OrderByDescending(b => b.PublishedAt)
            .Take(count)
            .ToListAsync(ct);

    public async Task<string> IncrementCvDownloadAsync(CancellationToken ct = default)
    {
        var profile = await _db.SiteProfiles.FirstOrDefaultAsync(ct);
        if (profile == null) return string.Empty;
        profile.CvDownloadCount++;
        await _db.SaveChangesAsync(ct);
        return profile.CvFilePath ?? string.Empty;
    }
}
