using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CreateManga.Application.Data.Migrations
{
    public partial class Add_New_Property_To_Manga : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MangaName",
                table: "Mangas",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "MangaId",
                table: "Mangas",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_Mangas_MangaName",
                table: "Mangas",
                newName: "IX_Mangas_Name");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Mangas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "Mangas",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "Mangas",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Mangas");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "Mangas");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "Mangas");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Mangas",
                newName: "MangaName");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Mangas",
                newName: "MangaId");

            migrationBuilder.RenameIndex(
                name: "IX_Mangas_Name",
                table: "Mangas",
                newName: "IX_Mangas_MangaName");
        }
    }
}
