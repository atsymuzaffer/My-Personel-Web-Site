using System.ComponentModel.DataAnnotations;

namespace PortfolioSite.Entities;

public enum SkillLevel { Başlangıç = 1, Orta = 2, İleri = 3, Uzman = 4 }

public class Skill : BaseEntity
{
    [Required, MaxLength(100)]
    public string Name { get; set; } = string.Empty;
    
    [MaxLength(500)]
    public string? Description { get; set; }
    
    [MaxLength(500)]
    public string? IconUrl { get; set; }
    
    public SkillLevel Level { get; set; } = SkillLevel.Orta;
    public bool IsFeatured { get; set; } = false;
    
    public int SkillCategoryId { get; set; }
    public SkillCategory? Category { get; set; }
}
