using System.ComponentModel.DataAnnotations;

namespace PortfolioSite.Entities;

public enum SocialPlatform
{
    GitHub, LinkedIn, Email, Instagram, X, Medium, YouTube, Website, Other
}

public class SocialLink : BaseEntity
{
    public SocialPlatform Platform { get; set; }
    
    [MaxLength(100)]
    public string DisplayName { get; set; } = string.Empty;
    
    [Required, MaxLength(500)]
    public string Url { get; set; } = string.Empty;
    
    public bool OpenInNewTab { get; set; } = true;
}
