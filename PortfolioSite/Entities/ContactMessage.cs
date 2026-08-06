using System.ComponentModel.DataAnnotations;

namespace PortfolioSite.Entities;

public class ContactMessage
{
    public int Id { get; set; }
    
    [Required, MaxLength(100)]
    public string FullName { get; set; } = string.Empty;
    
    [Required, MaxLength(200)]
    public string Email { get; set; } = string.Empty;
    
    [MaxLength(300)]
    public string? Subject { get; set; }
    
    [Required, MaxLength(5000)]
    public string Message { get; set; } = string.Empty;
    
    public DateTime SentAt { get; set; } = DateTime.UtcNow;
    public bool IsRead { get; set; } = false;
    public bool IsDeleted { get; set; } = false;
    
    [MaxLength(50)]
    public string? IpAddress { get; set; }
    
    [MaxLength(500)]
    public string? UserAgent { get; set; }
}
