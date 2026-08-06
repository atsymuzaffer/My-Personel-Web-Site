using System.ComponentModel.DataAnnotations;

namespace PortfolioSite.Entities;

public class Education : BaseEntity
{
    [Required, MaxLength(200)]
    public string School { get; set; } = string.Empty;
    
    [MaxLength(200)]
    public string? Department { get; set; }
    
    [MaxLength(100)]
    public string? Degree { get; set; }
    
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public bool IsCurrent { get; set; } = false;
    
    [MaxLength(2000)]
    public string? Description { get; set; }
    
    [MaxLength(500)]
    public string? LogoPath { get; set; }
}
