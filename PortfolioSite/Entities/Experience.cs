using System.ComponentModel.DataAnnotations;

namespace PortfolioSite.Entities;

public class Experience : BaseEntity
{
    [Required, MaxLength(200)]
    public string Position { get; set; } = string.Empty;
    
    [MaxLength(200)]
    public string? Company { get; set; }
    
    public bool HideCompanyName { get; set; } = false;
    
    [MaxLength(200)]
    public string? Location { get; set; }
    
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public bool IsCurrentJob { get; set; } = false;
    
    [MaxLength(3000)]
    public string? Description { get; set; }
    
    [MaxLength(1000)]
    public string? Technologies { get; set; } // Comma-separated
}
