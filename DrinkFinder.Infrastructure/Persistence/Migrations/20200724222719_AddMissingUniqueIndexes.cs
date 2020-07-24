using Microsoft.EntityFrameworkCore.Migrations;

namespace DrinkFinder.Infrastructure.Persistence.Migrations
{
    public partial class AddMissingUniqueIndexes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Location",
                table: "Photo",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "VATNumber",
                table: "Establishment",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ShortCode",
                table: "Establishment",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Photo_Location",
                table: "Photo",
                column: "Location",
                unique: true,
                filter: "[Location] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Establishment_ShortCode",
                table: "Establishment",
                column: "ShortCode",
                unique: true,
                filter: "[ShortCode] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Establishment_VATNumber",
                table: "Establishment",
                column: "VATNumber",
                unique: true,
                filter: "[VATNumber] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Photo_Location",
                table: "Photo");

            migrationBuilder.DropIndex(
                name: "IX_Establishment_ShortCode",
                table: "Establishment");

            migrationBuilder.DropIndex(
                name: "IX_Establishment_VATNumber",
                table: "Establishment");

            migrationBuilder.AlterColumn<string>(
                name: "Location",
                table: "Photo",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "VATNumber",
                table: "Establishment",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ShortCode",
                table: "Establishment",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
