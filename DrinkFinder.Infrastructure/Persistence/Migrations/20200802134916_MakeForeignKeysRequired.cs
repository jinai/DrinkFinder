using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DrinkFinder.Infrastructure.Persistence.Migrations
{
    public partial class MakeForeignKeysRequired : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BusinessHours_Establishment_EstablishmentId",
                table: "BusinessHours");

            migrationBuilder.DropForeignKey(
                name: "FK_Establishment_AspNetUsers_ApplicationUserId",
                table: "Establishment");

            migrationBuilder.DropForeignKey(
                name: "FK_News_AspNetUsers_ApplicationUserId",
                table: "News");

            migrationBuilder.DropForeignKey(
                name: "FK_News_Establishment_EstablishmentId",
                table: "News");

            migrationBuilder.DropForeignKey(
                name: "FK_Picture_Establishment_EstablishmentId",
                table: "Picture");

            migrationBuilder.DropIndex(
                name: "IX_News_ApplicationUserId",
                table: "News");

            migrationBuilder.DropIndex(
                name: "IX_Establishment_ApplicationUserId",
                table: "Establishment");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "News");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Establishment");

            migrationBuilder.AlterColumn<Guid>(
                name: "EstablishmentId",
                table: "Picture",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "EstablishmentId",
                table: "News",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PublisherId",
                table: "News",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ManagerId",
                table: "Establishment",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<Guid>(
                name: "EstablishmentId",
                table: "BusinessHours",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_News_PublisherId",
                table: "News",
                column: "PublisherId");

            migrationBuilder.CreateIndex(
                name: "IX_Establishment_ManagerId",
                table: "Establishment",
                column: "ManagerId");

            migrationBuilder.AddForeignKey(
                name: "FK_BusinessHours_Establishment_EstablishmentId",
                table: "BusinessHours",
                column: "EstablishmentId",
                principalTable: "Establishment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Establishment_AspNetUsers_ManagerId",
                table: "Establishment",
                column: "ManagerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_News_Establishment_EstablishmentId",
                table: "News",
                column: "EstablishmentId",
                principalTable: "Establishment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_News_AspNetUsers_PublisherId",
                table: "News",
                column: "PublisherId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Picture_Establishment_EstablishmentId",
                table: "Picture",
                column: "EstablishmentId",
                principalTable: "Establishment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BusinessHours_Establishment_EstablishmentId",
                table: "BusinessHours");

            migrationBuilder.DropForeignKey(
                name: "FK_Establishment_AspNetUsers_ManagerId",
                table: "Establishment");

            migrationBuilder.DropForeignKey(
                name: "FK_News_Establishment_EstablishmentId",
                table: "News");

            migrationBuilder.DropForeignKey(
                name: "FK_News_AspNetUsers_PublisherId",
                table: "News");

            migrationBuilder.DropForeignKey(
                name: "FK_Picture_Establishment_EstablishmentId",
                table: "Picture");

            migrationBuilder.DropIndex(
                name: "IX_News_PublisherId",
                table: "News");

            migrationBuilder.DropIndex(
                name: "IX_Establishment_ManagerId",
                table: "Establishment");

            migrationBuilder.DropColumn(
                name: "PublisherId",
                table: "News");

            migrationBuilder.DropColumn(
                name: "ManagerId",
                table: "Establishment");

            migrationBuilder.AlterColumn<Guid>(
                name: "EstablishmentId",
                table: "Picture",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AlterColumn<Guid>(
                name: "EstablishmentId",
                table: "News",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AddColumn<Guid>(
                name: "ApplicationUserId",
                table: "News",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ApplicationUserId",
                table: "Establishment",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "EstablishmentId",
                table: "BusinessHours",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.CreateIndex(
                name: "IX_News_ApplicationUserId",
                table: "News",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Establishment_ApplicationUserId",
                table: "Establishment",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_BusinessHours_Establishment_EstablishmentId",
                table: "BusinessHours",
                column: "EstablishmentId",
                principalTable: "Establishment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Establishment_AspNetUsers_ApplicationUserId",
                table: "Establishment",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_News_AspNetUsers_ApplicationUserId",
                table: "News",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_News_Establishment_EstablishmentId",
                table: "News",
                column: "EstablishmentId",
                principalTable: "Establishment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Picture_Establishment_EstablishmentId",
                table: "Picture",
                column: "EstablishmentId",
                principalTable: "Establishment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
