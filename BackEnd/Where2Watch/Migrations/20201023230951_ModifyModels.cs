using Microsoft.EntityFrameworkCore.Migrations;

namespace Where2Watch.Migrations
{
    public partial class ModifyModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IMDbId",
                table: "Titles",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Year",
                table: "Titles",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Titles_IMDbId",
                table: "Titles",
                column: "IMDbId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Titles_IMDbId",
                table: "Titles");

            migrationBuilder.DropColumn(
                name: "IMDbId",
                table: "Titles");

            migrationBuilder.DropColumn(
                name: "Year",
                table: "Titles");
        }
    }
}
