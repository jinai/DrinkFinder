using Microsoft.EntityFrameworkCore.Migrations;

namespace DrinkFinder.Infrastructure.Persistence.Migrations
{
    public partial class VatNumberFollowCapitalizationConventions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Establishment_VATNumber",
                schema: "Domain",
                table: "Establishment");

            migrationBuilder.RenameColumn(
                name: "VATNumber",
                schema: "Domain",
                table: "Establishment",
                newName: "VatNumber");

            migrationBuilder.CreateIndex(
                name: "IX_Establishment_VatNumber",
                schema: "Domain",
                table: "Establishment",
                column: "VatNumber",
                unique: true,
                filter: "[VatNumber] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Establishment_VatNumber",
                schema: "Domain",
                table: "Establishment");

            migrationBuilder.RenameColumn(
                name: "VatNumber",
                schema: "Domain",
                table: "Establishment",
                newName: "VATNumber");

            migrationBuilder.CreateIndex(
                name: "IX_Establishment_VATNumber",
                schema: "Domain",
                table: "Establishment",
                column: "VATNumber",
                unique: true,
                filter: "[VATNumber] IS NOT NULL");
        }
    }
}
