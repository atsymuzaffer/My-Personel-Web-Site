using System.ComponentModel.DataAnnotations;

namespace PortfolioSite.Entities;

public class SkillCategory : BaseEntity
{
    [Required, MaxLength(100)]
    public string Name { get; set; } = string.Empty;
    
    [MaxLength(10)]
    public string? IconKey { get; set; }
    
    public ICollection<Skill> Skills { get; set; } = new List<Skill>();
}
