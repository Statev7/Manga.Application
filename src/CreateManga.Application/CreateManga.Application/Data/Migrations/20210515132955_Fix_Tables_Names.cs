using Microsoft.EntityFrameworkCore.Migrations;

namespace CreateManga.Application.Data.Migrations
{
    public partial class Fix_Tables_Names : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chapter_Manga_MangaId",
                table: "Chapter");

            migrationBuilder.DropForeignKey(
                name: "FK_Character_Manga_MangaId",
                table: "Character");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Manga",
                table: "Manga");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Character",
                table: "Character");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Chapter",
                table: "Chapter");

            migrationBuilder.RenameTable(
                name: "Manga",
                newName: "Mangas");

            migrationBuilder.RenameTable(
                name: "Character",
                newName: "Characters");

            migrationBuilder.RenameTable(
                name: "Chapter",
                newName: "Chapters");

            migrationBuilder.RenameIndex(
                name: "IX_Manga_Name",
                table: "Mangas",
                newName: "IX_Mangas_Name");

            migrationBuilder.RenameIndex(
                name: "IX_Character_MangaId",
                table: "Characters",
                newName: "IX_Characters_MangaId");

            migrationBuilder.RenameIndex(
                name: "IX_Chapter_MangaId",
                table: "Chapters",
                newName: "IX_Chapters_MangaId");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Mangas",
                type: "nvarchar(32)",
                maxLength: 32,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Mangas",
                table: "Mangas",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Characters",
                table: "Characters",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Chapters",
                table: "Chapters",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Chapters_Mangas_MangaId",
                table: "Chapters",
                column: "MangaId",
                principalTable: "Mangas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Characters_Mangas_MangaId",
                table: "Characters",
                column: "MangaId",
                principalTable: "Mangas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chapters_Mangas_MangaId",
                table: "Chapters");

            migrationBuilder.DropForeignKey(
                name: "FK_Characters_Mangas_MangaId",
                table: "Characters");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Mangas",
                table: "Mangas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Characters",
                table: "Characters");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Chapters",
                table: "Chapters");

            migrationBuilder.RenameTable(
                name: "Mangas",
                newName: "Manga");

            migrationBuilder.RenameTable(
                name: "Characters",
                newName: "Character");

            migrationBuilder.RenameTable(
                name: "Chapters",
                newName: "Chapter");

            migrationBuilder.RenameIndex(
                name: "IX_Mangas_Name",
                table: "Manga",
                newName: "IX_Manga_Name");

            migrationBuilder.RenameIndex(
                name: "IX_Characters_MangaId",
                table: "Character",
                newName: "IX_Character_MangaId");

            migrationBuilder.RenameIndex(
                name: "IX_Chapters_MangaId",
                table: "Chapter",
                newName: "IX_Chapter_MangaId");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Manga",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(32)",
                oldMaxLength: 32);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Manga",
                table: "Manga",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Character",
                table: "Character",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Chapter",
                table: "Chapter",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Chapter_Manga_MangaId",
                table: "Chapter",
                column: "MangaId",
                principalTable: "Manga",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Character_Manga_MangaId",
                table: "Character",
                column: "MangaId",
                principalTable: "Manga",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
