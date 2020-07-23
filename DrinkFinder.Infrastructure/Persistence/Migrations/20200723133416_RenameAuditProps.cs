using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DrinkFinder.Infrastructure.Persistence.Migrations
{
    public partial class RenameAuditProps : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Timetable");

            migrationBuilder.DropColumn(
                name: "LastEditedAt",
                table: "Timetable");

            migrationBuilder.DropColumn(
                name: "SoftDeleted",
                table: "Timetable");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Photo");

            migrationBuilder.DropColumn(
                name: "LastEditedAt",
                table: "Photo");

            migrationBuilder.DropColumn(
                name: "SoftDeleted",
                table: "Photo");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "News");

            migrationBuilder.DropColumn(
                name: "LastEditedAt",
                table: "News");

            migrationBuilder.DropColumn(
                name: "SoftDeleted",
                table: "News");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Establishment");

            migrationBuilder.DropColumn(
                name: "LastEditedAt",
                table: "Establishment");

            migrationBuilder.DropColumn(
                name: "SoftDeleted",
                table: "Establishment");

            migrationBuilder.AddColumn<DateTime>(
                name: "AddedDate",
                table: "Timetable",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Timetable",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                table: "Timetable",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "AddedDate",
                table: "Photo",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Photo",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                table: "Photo",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "AddedDate",
                table: "News",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "News",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                table: "News",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "AddedDate",
                table: "Establishment",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Establishment",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                table: "Establishment",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AddedDate",
                table: "Timetable");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Timetable");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "Timetable");

            migrationBuilder.DropColumn(
                name: "AddedDate",
                table: "Photo");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Photo");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "Photo");

            migrationBuilder.DropColumn(
                name: "AddedDate",
                table: "News");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "News");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "News");

            migrationBuilder.DropColumn(
                name: "AddedDate",
                table: "Establishment");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Establishment");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "Establishment");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Timetable",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "LastEditedAt",
                table: "Timetable",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "SoftDeleted",
                table: "Timetable",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Photo",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "LastEditedAt",
                table: "Photo",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "SoftDeleted",
                table: "Photo",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "News",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "LastEditedAt",
                table: "News",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "SoftDeleted",
                table: "News",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Establishment",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "LastEditedAt",
                table: "Establishment",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "SoftDeleted",
                table: "Establishment",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
