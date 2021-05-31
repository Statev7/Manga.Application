using Microsoft.EntityFrameworkCore.Migrations;

namespace CreateManga.Application.Data.Migrations
{
    public partial class Add_New_Property_To_Anime_Model : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Votes",
                table: "Mangas",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Votes",
                table: "Mangas");
        }
    }
}
