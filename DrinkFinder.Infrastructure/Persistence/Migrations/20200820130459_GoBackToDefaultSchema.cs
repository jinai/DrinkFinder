using Microsoft.EntityFrameworkCore.Migrations;

namespace DrinkFinder.Infrastructure.Persistence.Migrations
{
    public partial class GoBackToDefaultSchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "Picture",
                schema: "Domain",
                newName: "Picture");

            migrationBuilder.RenameTable(
                name: "News",
                schema: "Domain",
                newName: "News");

            migrationBuilder.RenameTable(
                name: "Establishment",
                schema: "Domain",
                newName: "Establishment");

            migrationBuilder.RenameTable(
                name: "BusinessHours",
                schema: "Domain",
                newName: "BusinessHours");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Domain");

            migrationBuilder.RenameTable(
                name: "Picture",
                newName: "Picture",
                newSchema: "Domain");

            migrationBuilder.RenameTable(
                name: "News",
                newName: "News",
                newSchema: "Domain");

            migrationBuilder.RenameTable(
                name: "Establishment",
                newName: "Establishment",
                newSchema: "Domain");

            migrationBuilder.RenameTable(
                name: "BusinessHours",
                newName: "BusinessHours",
                newSchema: "Domain");
        }
    }
}
