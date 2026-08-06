using System.ComponentModel.DataAnnotations;

namespace PortfolioSite.Entities;

public class BlogPost : BaseEntity
{
    [Required, MaxLength(300)]
    public string Title { get; set; } = string.Empty;
    
    [Required, MaxLength(300)]
    public string Slug { get; set; } = string.Empty;
    
    [MaxLength(500)]
    public string? Summary { get; set; }
    
    public string? Content { get; set; }
    
    [MaxLength(500)]
    public string? CoverImagePath { get; set; }
    
    [MaxLength(100)]
    public string? Category { get; set; }
    
    [MaxLength(500)]
    public string? Tags { get; set; } // Comma-separated
    
    public DateTime? PublishedAt { get; set; }
    public bool IsPublished { get; set; } = false;
    public int ReadingTimeMinutes { get; set; } = 5;
    public int ViewCount { get; set; } = 0;
}
