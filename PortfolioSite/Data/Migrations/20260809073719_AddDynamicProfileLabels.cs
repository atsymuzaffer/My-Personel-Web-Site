using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PortfolioSite.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddDynamicProfileLabels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BusinessProcessesLabel",
                table: "SiteProfiles",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DatabaseSkillsLabel",
                table: "SiteProfiles",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FocusAreaLabel",
                table: "SiteProfiles",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PrimaryTechsLabel",
                table: "SiteProfiles",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SpecializationLabel",
                table: "SiteProfiles",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BusinessProcessesLabel",
                table: "SiteProfiles");

            migrationBuilder.DropColumn(
                name: "DatabaseSkillsLabel",
                table: "SiteProfiles");

            migrationBuilder.DropColumn(
                name: "FocusAreaLabel",
                table: "SiteProfiles");

            migrationBuilder.DropColumn(
                name: "PrimaryTechsLabel",
                table: "SiteProfiles");

            migrationBuilder.DropColumn(
                name: "SpecializationLabel",
                table: "SiteProfiles");
        }
    }
}
