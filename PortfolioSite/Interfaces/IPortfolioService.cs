using PortfolioSite.Entities;

namespace PortfolioSite.Interfaces;

public interface IPortfolioService
{
    Task<SiteProfile?> GetProfileAsync(CancellationToken ct = default);
    Task<List<Skill>> GetSkillsByCategoryAsync(CancellationToken ct = default);
    Task<List<SkillCategory>> GetSkillCategoriesWithSkillsAsync(CancellationToken ct = default);
    Task<List<Experience>> GetExperiencesAsync(CancellationToken ct = default);
    Task<List<Project>> GetFeaturedProjectsAsync(CancellationToken ct = default);
    Task<List<Project>> GetAllProjectsAsync(CancellationToken ct = default);
    Task<Project?> GetProjectBySlugAsync(string slug, CancellationToken ct = default);
    Task<List<Education>> GetEducationsAsync(CancellationToken ct = default);
    Task<List<Certificate>> GetCertificatesAsync(CancellationToken ct = default);
    Task<List<SocialLink>> GetSocialLinksAsync(CancellationToken ct = default);
    Task<List<BlogPost>> GetPublishedBlogPostsAsync(int count = 3, CancellationToken ct = default);
    Task<string> IncrementCvDownloadAsync(CancellationToken ct = default);
}
