using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PortfolioSite.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddMustChangeCredentials : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "MustChangeCredentials",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MustChangeCredentials",
                table: "AspNetUsers");
        }
    }
}
