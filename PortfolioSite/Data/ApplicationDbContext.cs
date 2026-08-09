using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PortfolioSite.Entities;

namespace PortfolioSite.Data;

// ANTIGRAVITY DEĞİŞİKLİĞİ: Eski EF6 Context tamamen yenilendi, ASP.NET Core Identity entegre edildi
public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<SiteProfile> SiteProfiles => Set<SiteProfile>();
    public DbSet<SkillCategory> SkillCategories => Set<SkillCategory>();
    public DbSet<Skill> Skills => Set<Skill>();
    public DbSet<Experience> Experiences => Set<Experience>();
    public DbSet<Project> Projects => Set<Project>();
    public DbSet<ProjectImage> ProjectImages => Set<ProjectImage>();
    public DbSet<Education> Educations => Set<Education>();
    public DbSet<Certificate> Certificates => Set<Certificate>();
    public DbSet<SocialLink> SocialLinks => Set<SocialLink>();
    public DbSet<BlogPost> BlogPosts => Set<BlogPost>();
    public DbSet<ContactMessage> ContactMessages => Set<ContactMessage>();
    public DbSet<AuditLog> AuditLogs => Set<AuditLog>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Soft delete global filters
        builder.Entity<Project>().HasQueryFilter(p => !p.IsDeleted);
        builder.Entity<Skill>().HasQueryFilter(s => !s.IsDeleted);
        builder.Entity<Experience>().HasQueryFilter(e => !e.IsDeleted);
        builder.Entity<Certificate>().HasQueryFilter(c => !c.IsDeleted);
        builder.Entity<BlogPost>().HasQueryFilter(b => !b.IsDeleted);
        builder.Entity<Education>().HasQueryFilter(e => !e.IsDeleted);
        builder.Entity<SocialLink>().HasQueryFilter(s => !s.IsDeleted);

        // Indexes
        builder.Entity<Project>().HasIndex(p => p.Slug).IsUnique();
        builder.Entity<BlogPost>().HasIndex(b => b.Slug).IsUnique();
        builder.Entity<Project>().HasIndex(p => new { p.IsActive, p.IsDeleted, p.SortOrder });
        builder.Entity<Skill>().HasIndex(s => new { s.IsActive, s.IsDeleted, s.SortOrder });

        // Seed initial data
        SeedData(builder);
    }

    private static void SeedData(ModelBuilder builder)
    {
        builder.Entity<SiteProfile>().HasData(new SiteProfile
        {
            Id = 1,
            FullName = "John Doe",
            Title = "Software Engineer & Backend Developer",
            ShortBio = "Kurumsal iş süreçleri, SQL Server ve ASP.NET Core tabanlı sürdürülebilir yüksek performanslı uygulamalar geliştiriyorum.",
            AboutText = "Yazılım geliştirme alanında backend ve veritabanı sistemleri üzerine uzmanlaşmış bir geliştiriciyim. C#, ASP.NET Core MVC, Entity Framework Core ve Microsoft SQL Server teknolojilerini kullanarak kurumsal düzeyde ölçeklenebilir uygulamalar geliştiriyorum.\n\nKurumsal ERP entegrasyonları, iş akışı ve onay süreçleri, veritabanı performans optimizasyonları ve REST API servisleri konularında deneyim sahibiyim.",
            Email = "contact@example.com",
            GitHubUrl = "https://github.com/example",
            LinkedInUrl = "https://linkedin.com/in/example",
            WebsiteUrl = "https://example.com",
            Location = "İstanbul, Türkiye",
            Specialization = "SQL Server / C#",
            IsAvailableForWork = true,
            HeroBadgeText = "Yeni fırsatlara açık",
            MetaDescription = "ASP.NET Core, C#, SQL Server ve backend geliştirme projelerini içeren kişisel portföy web sitesi.",
            IsActive = true,
            IsDeleted = false,
            CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc),
            UpdatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc)
        });

        builder.Entity<SkillCategory>().HasData(
            new SkillCategory { Id = 1, Name = "Backend", IconKey = "server", SortOrder = 1, IsActive = true, IsDeleted = false, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc), UpdatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new SkillCategory { Id = 2, Name = "Veritabanı", IconKey = "database", SortOrder = 2, IsActive = true, IsDeleted = false, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc), UpdatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new SkillCategory { Id = 3, Name = "Kurumsal Süreçler", IconKey = "briefcase", SortOrder = 3, IsActive = true, IsDeleted = false, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc), UpdatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new SkillCategory { Id = 4, Name = "Araçlar & Teknolojiler", IconKey = "wrench", SortOrder = 4, IsActive = true, IsDeleted = false, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc), UpdatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new SkillCategory { Id = 5, Name = "Siber Güvenlik", IconKey = "shield", SortOrder = 5, IsActive = true, IsDeleted = false, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc), UpdatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) }
        );

        builder.Entity<Skill>().HasData(
            new Skill { Id = 1, Name = "C#", SkillCategoryId = 1, Level = SkillLevel.Uzman, IsFeatured = true, SortOrder = 1, IsActive = true, IsDeleted = false, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc), UpdatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new Skill { Id = 2, Name = "ASP.NET Core MVC", SkillCategoryId = 1, Level = SkillLevel.Uzman, IsFeatured = true, SortOrder = 2, IsActive = true, IsDeleted = false, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc), UpdatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new Skill { Id = 3, Name = "Entity Framework Core", SkillCategoryId = 1, Level = SkillLevel.İleri, IsFeatured = true, SortOrder = 3, IsActive = true, IsDeleted = false, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc), UpdatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new Skill { Id = 4, Name = "REST API", SkillCategoryId = 1, Level = SkillLevel.İleri, SortOrder = 4, IsActive = true, IsDeleted = false, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc), UpdatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new Skill { Id = 5, Name = "Microsoft SQL Server", SkillCategoryId = 2, Level = SkillLevel.Uzman, IsFeatured = true, SortOrder = 1, IsActive = true, IsDeleted = false, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc), UpdatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new Skill { Id = 6, Name = "T-SQL", SkillCategoryId = 2, Level = SkillLevel.Uzman, IsFeatured = true, SortOrder = 2, IsActive = true, IsDeleted = false, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc), UpdatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new Skill { Id = 7, Name = "Stored Procedure", SkillCategoryId = 2, Level = SkillLevel.Uzman, SortOrder = 3, IsActive = true, IsDeleted = false, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc), UpdatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new Skill { Id = 8, Name = "Trigger & View & CTE", SkillCategoryId = 2, Level = SkillLevel.İleri, SortOrder = 4, IsActive = true, IsDeleted = false, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc), UpdatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new Skill { Id = 9, Name = "ERP Entegrasyonu", SkillCategoryId = 3, Level = SkillLevel.İleri, IsFeatured = true, SortOrder = 1, IsActive = true, IsDeleted = false, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc), UpdatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new Skill { Id = 10, Name = "İş Akışı & Onay Süreçleri", SkillCategoryId = 3, Level = SkillLevel.İleri, SortOrder = 2, IsActive = true, IsDeleted = false, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc), UpdatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new Skill { Id = 11, Name = "Muhasebe & Finans Süreçleri", SkillCategoryId = 3, Level = SkillLevel.İleri, SortOrder = 3, IsActive = true, IsDeleted = false, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc), UpdatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new Skill { Id = 12, Name = "Git & GitHub", SkillCategoryId = 4, Level = SkillLevel.İleri, SortOrder = 1, IsActive = true, IsDeleted = false, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc), UpdatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new Skill { Id = 13, Name = "Siber Güvenlik Temelleri", SkillCategoryId = 5, Level = SkillLevel.Orta, SortOrder = 1, IsActive = true, IsDeleted = false, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc), UpdatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) }
        );

        builder.Entity<Certificate>().HasData(
            new Certificate
            {
                Id = 1,
                Name = "Certified Network Security Professional",
                IssuingOrganization = "Global IT Academy",
                IssuedDate = new DateTime(2024, 5, 1, 0, 0, 0, DateTimeKind.Utc),
                CredentialId = "CERT-SEC-2024",
                VerificationUrl = "https://example.com/cert/verify",
                IsFeatured = true,
                IsActive = true,
                IsDeleted = false,
                CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Certificate
            {
                Id = 2,
                Name = "SQL Server Administration & T-SQL Optimization",
                IssuingOrganization = "Database Institute",
                IssuedDate = new DateTime(2023, 10, 1, 0, 0, 0, DateTimeKind.Utc),
                CredentialId = "DB-INST-2023",
                IsFeatured = true,
                IsActive = true,
                IsDeleted = false,
                CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            }
        );

        builder.Entity<Experience>().HasData(
            new Experience
            {
                Id = 1,
                Position = "Backend ve Veritabanı Geliştiricisi",
                Company = "Acme Corp A.Ş.",
                Location = "İstanbul",
                StartDate = new DateTime(2022, 6, 1, 0, 0, 0, DateTimeKind.Utc),
                IsCurrentJob = true,
                Description = "ASP.NET Core ve SQL Server mimarileri ile kurumsal ERP entegrasyonları, iş akışları, Stored Procedure ve Trigger bazlı veri optimizasyonları geliştirmekteyim.",
                Technologies = "C#, ASP.NET Core, SQL Server, T-SQL, EF Core, REST API",
                IsActive = true,
                IsDeleted = false,
                CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            }
        );

        builder.Entity<Project>().HasData(
            new Project
            {
                Id = 1,
                Name = "Kurumsal ERP & Muhasebe Entegrasyon Modülü",
                Slug = "kurumsal-erp-muhasebe-entegrasyonu",
                Summary = "ASP.NET Core ve SQL Server tabanlı, muhasebe süreçlerini otomatize eden kurumsal entegrasyon çözümü.",
                Description = "Stok, fatura ve finans hareketlerini Stored Procedure ve Trigger mekanizmaları ile gerçek zamanlı senkronize eden yüksek performanslı backend mimarisi.",
                ProblemStatement = "Eski sistemdeki senkronizasyon gecikmeleri ve manuel veri girişi hataları.",
                Solution = "T-SQL optimizasyonu, CTE ve JSON işlemleri ile saniyede binlerce veriyi güvenli işleyen REST API servisi.",
                Technologies = "C#, ASP.NET Core MVC, SQL Server, T-SQL, EF Core",
                Status = ProjectStatus.Tamamlandı,
                IsFeatured = true,
                IsActive = true,
                IsDeleted = false,
                CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Project
            {
                Id = 2,
                Name = "İş Akışı ve Onay Yönetim Portalı",
                Slug = "is-akisi-onay-yonetim-portali",
                Summary = "Kurumsal dinamik rol ve yetkilendirme altyapısına sahip onay süreç yönetim sistemi.",
                Description = "Çok kademeli onay mekanizmaları, email bildirim entegrasyonu ve performans takip panosu sunan responsive web portalı.",
                Technologies = "ASP.NET Core, SQL Server, Identity, Bootstrap",
                Status = ProjectStatus.Tamamlandı,
                IsFeatured = true,
                IsActive = true,
                IsDeleted = false,
                CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            }
        );

        builder.Entity<SocialLink>().HasData(
            new SocialLink
            {
                Id = 1,
                Platform = SocialPlatform.GitHub,
                DisplayName = "GitHub",
                Url = "https://github.com/example",
                SortOrder = 1,
                IsActive = true,
                IsDeleted = false,
                CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new SocialLink
            {
                Id = 2,
                Platform = SocialPlatform.LinkedIn,
                DisplayName = "LinkedIn",
                Url = "https://linkedin.com/in/example",
                SortOrder = 2,
                IsActive = true,
                IsDeleted = false,
                CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            }
        );
    }
}
