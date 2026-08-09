using System.ComponentModel.DataAnnotations;

namespace PortfolioSite.Entities;

// ANTIGRAVITY DEĞİŞİKLİĞİ: Eski HomePage modelini genişleterek tam portföy profili oluşturuldu
public class SiteProfile : BaseEntity
{
    [Required, MaxLength(100)]
    public string FullName { get; set; } = "John Doe";
    
    [MaxLength(200)]
    public string Title { get; set; } = "Software Engineer & Backend Developer";
    
    [MaxLength(1000)]
    public string ShortBio { get; set; } = "Kurumsal iş süreçleri, SQL Server ve ASP.NET Core tabanlı sürdürülebilir yüksek performanslı uygulamalar geliştiriyorum.";
    
    [MaxLength(5000)]
    public string AboutText { get; set; } = string.Empty;
    
    [MaxLength(255)]
    public string? Email { get; set; }
    
    [MaxLength(500)]
    public string? GitHubUrl { get; set; }
    
    [MaxLength(500)]
    public string? LinkedInUrl { get; set; }
    
    [MaxLength(500)]
    public string? WebsiteUrl { get; set; } = "https://example.com";
    
    [MaxLength(500)]
    public string? ProfileImagePath { get; set; }
    
    [MaxLength(500)]
    public string? CvFilePath { get; set; }
    
    public int CvDownloadCount { get; set; } = 0;
    
    [MaxLength(200)]
    public string? Location { get; set; }
    
    [MaxLength(200)]
    public string? Specialization { get; set; } = "SQL Server / C#";
    
    [MaxLength(200)]
    public string? FocusArea { get; set; } = "Backend & Veritabanı Mimarisi";
    
    [MaxLength(300)]
    public string? PrimaryTechs { get; set; } = "C#, ASP.NET Core, EF Core, SQL Server";
    
    [MaxLength(300)]
    public string? DatabaseSkills { get; set; } = "T-SQL, Stored Procedure, Trigger, CTE, JSON";
    
    [MaxLength(300)]
    public string? BusinessProcesses { get; set; } = "ERP Entegrasyonu, İş Akışları, Muhasebe";
    
    public bool IsAvailableForWork { get; set; } = true;
    
    [MaxLength(200)]
    public string? HeroBadgeText { get; set; } = "Yeni fırsatlara açık";
    
    // Hero section stats
    public int? CompletedProjects { get; set; }
    public int? TechnologiesUsed { get; set; }
    public int? CertificatesCount { get; set; }
    public int? YearsOfExperience { get; set; }
    
    [MaxLength(200)]
    public string? MetaDescription { get; set; }
    
    [MaxLength(500)]
    public string? MetaKeywords { get; set; }
}
