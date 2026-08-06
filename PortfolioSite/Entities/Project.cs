using System.ComponentModel.DataAnnotations;

namespace PortfolioSite.Entities;

public enum ProjectStatus { Geliştiriliyor, Tamamlandı, Arşivlendi }

public class Project : BaseEntity
{
    [Required, MaxLength(200)]
    public string Name { get; set; } = string.Empty;
    
    [Required, MaxLength(200)]
    public string Slug { get; set; } = string.Empty;
    
    [MaxLength(500)]
    public string? Summary { get; set; }
    
    [MaxLength(5000)]
    public string? Description { get; set; }
    
    [MaxLength(3000)]
    public string? ProblemStatement { get; set; }
    
    [MaxLength(3000)]
    public string? Solution { get; set; }
    
    [MaxLength(3000)]
    public string? MyRole { get; set; }
    
    [MaxLength(3000)]
    public string? TechDetails { get; set; }
    
    [MaxLength(500)]
    public string? CoverImagePath { get; set; }
    
    [MaxLength(500)]
    public string? GitHubUrl { get; set; }
    
    [MaxLength(500)]
    public string? LiveUrl { get; set; }
    
    [MaxLength(500)]
    public string? Technologies { get; set; } // Comma-separated for display
    
    public ProjectStatus Status { get; set; } = ProjectStatus.Tamamlandı;
    public bool IsFeatured { get; set; } = false;
    
    public ICollection<ProjectImage> Images { get; set; } = new List<ProjectImage>();
}
