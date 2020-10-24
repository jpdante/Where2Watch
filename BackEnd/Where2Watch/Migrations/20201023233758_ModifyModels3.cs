using Microsoft.EntityFrameworkCore.Migrations;

namespace Where2Watch.Migrations
{
    public partial class ModifyModels3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IMDbId",
                table: "Episodes",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Episodes_IMDbId",
                table: "Episodes",
                column: "IMDbId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Episodes_IMDbId",
                table: "Episodes");

            migrationBuilder.DropColumn(
                name: "IMDbId",
                table: "Episodes");
        }
    }
}
