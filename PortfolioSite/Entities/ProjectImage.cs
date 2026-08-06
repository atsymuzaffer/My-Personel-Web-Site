using System.ComponentModel.DataAnnotations;

namespace PortfolioSite.Entities;

public class ProjectImage : BaseEntity
{
    [Required, MaxLength(500)]
    public string ImagePath { get; set; } = string.Empty;
    
    [MaxLength(200)]
    public string? Caption { get; set; }
    
    public bool IsCover { get; set; } = false;
    
    public int ProjectId { get; set; }
    public Project? Project { get; set; }
}
