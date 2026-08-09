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
        }
    }
}
