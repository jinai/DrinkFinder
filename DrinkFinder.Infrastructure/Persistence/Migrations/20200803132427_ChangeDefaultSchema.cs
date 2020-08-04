using Microsoft.EntityFrameworkCore.Migrations;

namespace DrinkFinder.Infrastructure.Persistence.Migrations
{
    public partial class ChangeDefaultSchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.RenameTable(
                name: "AspNetUserTokens",
                newName: "AspNetUserTokens",
                newSchema: "Domain");

            migrationBuilder.RenameTable(
                name: "AspNetUsers",
                newName: "AspNetUsers",
                newSchema: "Domain");

            migrationBuilder.RenameTable(
                name: "AspNetUserRoles",
                newName: "AspNetUserRoles",
                newSchema: "Domain");

            migrationBuilder.RenameTable(
                name: "AspNetUserLogins",
                newName: "AspNetUserLogins",
                newSchema: "Domain");

            migrationBuilder.RenameTable(
                name: "AspNetUserClaims",
                newName: "AspNetUserClaims",
                newSchema: "Domain");

            migrationBuilder.RenameTable(
                name: "AspNetRoles",
                newName: "AspNetRoles",
                newSchema: "Domain");

            migrationBuilder.RenameTable(
                name: "AspNetRoleClaims",
                newName: "AspNetRoleClaims",
                newSchema: "Domain");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.RenameTable(
                name: "AspNetUserTokens",
                schema: "Domain",
                newName: "AspNetUserTokens");

            migrationBuilder.RenameTable(
                name: "AspNetUsers",
                schema: "Domain",
                newName: "AspNetUsers");

            migrationBuilder.RenameTable(
                name: "AspNetUserRoles",
                schema: "Domain",
                newName: "AspNetUserRoles");

            migrationBuilder.RenameTable(
                name: "AspNetUserLogins",
                schema: "Domain",
                newName: "AspNetUserLogins");

            migrationBuilder.RenameTable(
                name: "AspNetUserClaims",
                schema: "Domain",
                newName: "AspNetUserClaims");

            migrationBuilder.RenameTable(
                name: "AspNetRoles",
                schema: "Domain",
                newName: "AspNetRoles");

            migrationBuilder.RenameTable(
                name: "AspNetRoleClaims",
                schema: "Domain",
                newName: "AspNetRoleClaims");
        }
    }
}
