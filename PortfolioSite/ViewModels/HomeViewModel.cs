using PortfolioSite.Entities;

namespace PortfolioSite.ViewModels;

public class HomeViewModel
{
    public SiteProfile? Profile { get; set; }
    public List<SkillCategory> SkillCategories { get; set; } = new();
    public List<Experience> Experiences { get; set; } = new();
    public List<Project> FeaturedProjects { get; set; } = new();
    public List<Education> Educations { get; set; } = new();
    public List<Certificate> Certificates { get; set; } = new();
    public List<SocialLink> SocialLinks { get; set; } = new();
    public List<BlogPost> RecentBlogPosts { get; set; } = new();
}
