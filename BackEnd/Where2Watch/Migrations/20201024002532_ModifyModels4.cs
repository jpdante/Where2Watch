using Microsoft.EntityFrameworkCore.Migrations;

namespace Where2Watch.Migrations
{
    public partial class ModifyModels4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Classification",
                table: "Titles",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Genres",
                table: "Titles",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Outline",
                table: "Titles",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Classification",
                table: "Titles");

            migrationBuilder.DropColumn(
                name: "Genres",
                table: "Titles");

            migrationBuilder.DropColumn(
                name: "Outline",
                table: "Titles");
        }
    }
}
