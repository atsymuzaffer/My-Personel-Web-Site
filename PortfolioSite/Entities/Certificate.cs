using System.ComponentModel.DataAnnotations;

namespace PortfolioSite.Entities;

public class Certificate : BaseEntity
{
    [Required, MaxLength(300)]
    public string Name { get; set; } = string.Empty;
    
    [MaxLength(200)]
    public string? IssuingOrganization { get; set; }
    
    public DateTime IssuedDate { get; set; }
    public DateTime? ExpiryDate { get; set; }
    
    [MaxLength(500)]
    public string? CredentialId { get; set; }
    
    [MaxLength(500)]
    public string? VerificationUrl { get; set; }
    
    [MaxLength(500)]
    public string? ImagePath { get; set; }
    
    [MaxLength(500)]
    public string? RelatedSkills { get; set; } // Comma-separated
    
    public bool IsFeatured { get; set; } = false;
}
