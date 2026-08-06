using System.ComponentModel.DataAnnotations;

namespace PortfolioSite.Entities;

public class AuditLog
{
    public int Id { get; set; }
    
    [MaxLength(256)]
    public string? UserId { get; set; }
    
    [MaxLength(256)]
    public string? UserName { get; set; }
    
    [MaxLength(100)]
    public string Action { get; set; } = string.Empty;
    
    [MaxLength(100)]
    public string? EntityName { get; set; }
    
    public string? EntityId { get; set; }
    
    [MaxLength(50)]
    public string? IpAddress { get; set; }
    
    [MaxLength(1000)]
    public string? Description { get; set; }
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
