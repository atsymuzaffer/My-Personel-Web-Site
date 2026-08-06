using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PortfolioSite.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Certificates",
                columns: new[] { "Id", "CreatedAt", "CredentialId", "ExpiryDate", "ImagePath", "IsActive", "IsDeleted", "IsFeatured", "IssuedDate", "IssuingOrganization", "Name", "RelatedSkills", "SortOrder", "UpdatedAt", "VerificationUrl" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "FT-NSE4-2024", null, null, true, false, true, new DateTime(2024, 5, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Fortinet", "NSE 4 Network Security Professional", null, 0, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "https://training.fortinet.com" },
                    { 2, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "MS-SQL-2023", null, null, true, false, true, new DateTime(2023, 10, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Microsoft Learning", "SQL Server Database Administration & T-SQL Optimization", null, 0, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null }
                });

            migrationBuilder.InsertData(
                table: "Experiences",
                columns: new[] { "Id", "Company", "CreatedAt", "Description", "EndDate", "HideCompanyName", "IsActive", "IsCurrentJob", "IsDeleted", "Location", "Position", "SortOrder", "StartDate", "Technologies", "UpdatedAt" },
                values: new object[] { 1, "Kurumsal Yazılım & Danışmanlık", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "ASP.NET Core ve SQL Server mimarileri ile kurumsal ERP entegrasyonları, muhasebe iş akışları, Stored Procedure ve Trigger bazlı veri optimizasyonları geliştirmekteyim.", null, false, true, true, false, "Türkiye", "Backend ve Veritabanı Geliştiricisi", 0, new DateTime(2022, 6, 1, 0, 0, 0, 0, DateTimeKind.Utc), "C#, ASP.NET Core, SQL Server, T-SQL, EF Core, REST API", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.InsertData(
                table: "Projects",
                columns: new[] { "Id", "CoverImagePath", "CreatedAt", "Description", "GitHubUrl", "IsActive", "IsDeleted", "IsFeatured", "LiveUrl", "MyRole", "Name", "ProblemStatement", "Slug", "Solution", "SortOrder", "Status", "Summary", "TechDetails", "Technologies", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Stok, fatura ve finans hareketlerini Stored Procedure ve Trigger mekanizmaları ile gerçek zamanlı senkronize eden yüksek performanslı backend mimarisi.", null, true, false, true, null, null, "Kurumsal ERP & Muhasebe Entegrasyon Modülü", "Eski sistemdeki senkronizasyon gecikmeleri ve manuel veri girişi hataları.", "kurumsal-erp-muhasebe-entegrasyonu", "T-SQL optimizasyonu, CTE ve JSON işlemleri ile saniyede binlerce veriyi güvenli işleyen REST API servisi.", 0, 1, "ASP.NET Core ve SQL Server tabanlı, muhasebe süreçlerini otomatize eden kurumsal entegrasyon çözümü.", null, "C#, ASP.NET Core MVC, SQL Server, T-SQL, EF Core", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 2, null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Çok kademeli onay mekanizmaları, email bildirim entegrasyonu ve performans takip panosu sunan responsive web portalı.", null, true, false, true, null, null, "İş Akışı ve Onay Yönetim Portalı", null, "is-akisi-onay-yonetim-portali", null, 0, 1, "Kurumsal dinamik rol ve yetkilendirme altyapısına sahip onay süreç yönetim sistemi.", null, "ASP.NET Core, SQL Server, Identity, Bootstrap", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.InsertData(
                table: "SocialLinks",
                columns: new[] { "Id", "CreatedAt", "DisplayName", "IsActive", "IsDeleted", "OpenInNewTab", "Platform", "SortOrder", "UpdatedAt", "Url" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "GitHub", true, false, true, 0, 1, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "https://github.com/atsymuzaffer" },
                    { 2, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "LinkedIn", true, false, true, 1, 2, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "https://linkedin.com" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Certificates",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Certificates",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Experiences",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "SocialLinks",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "SocialLinks",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
