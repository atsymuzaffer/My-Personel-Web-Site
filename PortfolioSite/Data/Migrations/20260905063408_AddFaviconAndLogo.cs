using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PortfolioSite.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddFaviconAndLogo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FaviconPath",
                table: "SiteProfiles",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LogoPath",
                table: "SiteProfiles",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.UpdateData(
                table: "SiteProfiles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "FaviconPath", "LogoPath" },
                values: new object[] { null, null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FaviconPath",
                table: "SiteProfiles");

            migrationBuilder.DropColumn(
                name: "LogoPath",
                table: "SiteProfiles");
        }
    }
}
