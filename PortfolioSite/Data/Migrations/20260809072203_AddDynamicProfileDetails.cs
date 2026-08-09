using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PortfolioSite.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddDynamicProfileDetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BusinessProcesses",
                table: "SiteProfiles",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DatabaseSkills",
                table: "SiteProfiles",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FocusArea",
                table: "SiteProfiles",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PrimaryTechs",
                table: "SiteProfiles",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Specialization",
                table: "SiteProfiles",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Certificates",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CredentialId", "IssuingOrganization", "Name", "VerificationUrl" },
                values: new object[] { "CERT-SEC-2024", "Global IT Academy", "Certified Network Security Professional", "https://example.com/cert/verify" });

            migrationBuilder.UpdateData(
                table: "Certificates",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CredentialId", "IssuingOrganization", "Name" },
                values: new object[] { "DB-INST-2023", "Database Institute", "SQL Server Administration & T-SQL Optimization" });

            migrationBuilder.UpdateData(
                table: "Experiences",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Company", "Description", "Location" },
                values: new object[] { "Acme Corp A.Ş.", "ASP.NET Core ve SQL Server mimarileri ile kurumsal ERP entegrasyonları, iş akışları, Stored Procedure ve Trigger bazlı veri optimizasyonları geliştirmekteyim.", "İstanbul" });

            migrationBuilder.UpdateData(
                table: "SiteProfiles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "AboutText", "BusinessProcesses", "DatabaseSkills", "Email", "FocusArea", "FullName", "GitHubUrl", "LinkedInUrl", "Location", "MetaDescription", "PrimaryTechs", "ShortBio", "Specialization", "Title", "WebsiteUrl" },
                values: new object[] { "Yazılım geliştirme alanında backend ve veritabanı sistemleri üzerine uzmanlaşmış bir geliştiriciyim. C#, ASP.NET Core MVC, Entity Framework Core ve Microsoft SQL Server teknolojilerini kullanarak kurumsal düzeyde ölçeklenebilir uygulamalar geliştiriyorum.\n\nKurumsal ERP entegrasyonları, iş akışı ve onay süreçleri, veritabanı performans optimizasyonları ve REST API servisleri konularında deneyim sahibiyim.", "ERP Entegrasyonu, İş Akışları, Muhasebe", "T-SQL, Stored Procedure, Trigger, CTE, JSON", "contact@example.com", "Backend & Veritabanı Mimarisi", "John Doe", "https://github.com/example", "https://linkedin.com/in/example", "İstanbul, Türkiye", "ASP.NET Core, C#, SQL Server ve backend geliştirme projelerini içeren kişisel portföy web sitesi.", "C#, ASP.NET Core, EF Core, SQL Server", "Kurumsal iş süreçleri, SQL Server ve ASP.NET Core tabanlı sürdürülebilir yüksek performanslı uygulamalar geliştiriyorum.", "SQL Server / C#", "Software Engineer & Backend Developer", "https://example.com" });

            migrationBuilder.UpdateData(
                table: "SocialLinks",
                keyColumn: "Id",
                keyValue: 1,
                column: "Url",
                value: "https://github.com/example");

            migrationBuilder.UpdateData(
                table: "SocialLinks",
                keyColumn: "Id",
                keyValue: 2,
                column: "Url",
                value: "https://linkedin.com/in/example");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BusinessProcesses",
                table: "SiteProfiles");

            migrationBuilder.DropColumn(
                name: "DatabaseSkills",
                table: "SiteProfiles");

            migrationBuilder.DropColumn(
                name: "FocusArea",
                table: "SiteProfiles");

            migrationBuilder.DropColumn(
                name: "PrimaryTechs",
                table: "SiteProfiles");

            migrationBuilder.DropColumn(
                name: "Specialization",
                table: "SiteProfiles");

            migrationBuilder.UpdateData(
                table: "Certificates",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CredentialId", "IssuingOrganization", "Name", "VerificationUrl" },
                values: new object[] { "FT-NSE4-2024", "Fortinet", "NSE 4 Network Security Professional", "https://training.fortinet.com" });

            migrationBuilder.UpdateData(
                table: "Certificates",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CredentialId", "IssuingOrganization", "Name" },
                values: new object[] { "MS-SQL-2023", "Microsoft Learning", "SQL Server Database Administration & T-SQL Optimization" });

            migrationBuilder.UpdateData(
                table: "Experiences",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Company", "Description", "Location" },
                values: new object[] { "Kurumsal Yazılım & Danışmanlık", "ASP.NET Core ve SQL Server mimarileri ile kurumsal ERP entegrasyonları, muhasebe iş akışları, Stored Procedure ve Trigger bazlı veri optimizasyonları geliştirmekteyim.", "Türkiye" });

            migrationBuilder.UpdateData(
                table: "SiteProfiles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "AboutText", "Email", "FullName", "GitHubUrl", "LinkedInUrl", "Location", "MetaDescription", "ShortBio", "Title", "WebsiteUrl" },
                values: new object[] { "Yazılım geliştirme alanında backend ve veritabanı üzerine uzmanlaşmış bir geliştiriciyim. C#, ASP.NET Core MVC, Entity Framework Core ve Microsoft SQL Server teknolojilerini kullanarak kurumsal düzeyde, ölçeklenebilir ve sürdürülebilir uygulamalar geliştiriyorum.\n\nKurumsal ERP entegrasyonları, iş akışı ve onay süreçleri, muhasebe ve finans sistemleri üzerine deneyim sahibiyim. Stored Procedure, Trigger, View, CTE ve JSON işlemleri konularında ileri düzey SQL Server bilgisine sahibim.", null, "Muzaffer Atasoy", null, null, "Türkiye", "Muzaffer Atasoy'un ASP.NET Core, C#, SQL Server, kurumsal iş süreçleri ve backend geliştirme projelerini içeren kişisel portföy sitesi.", "Kurumsal iş süreçleri, SQL Server ve ASP.NET Core tabanlı sürdürülebilir uygulamalar geliştiriyorum. İş akışları, muhasebe entegrasyonları ve veritabanı çözümlerinde uzmanlaşıyorum.", "Backend ve Veritabanı Geliştiricisi", "https://muzafferatasoy.com" });

            migrationBuilder.UpdateData(
                table: "SocialLinks",
                keyColumn: "Id",
                keyValue: 1,
                column: "Url",
                value: "https://github.com/atsymuzaffer");

            migrationBuilder.UpdateData(
                table: "SocialLinks",
                keyColumn: "Id",
                keyValue: 2,
                column: "Url",
                value: "https://linkedin.com");
        }
    }
}
