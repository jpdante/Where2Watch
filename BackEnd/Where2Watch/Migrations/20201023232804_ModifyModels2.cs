using Microsoft.EntityFrameworkCore.Migrations;

namespace Where2Watch.Migrations
{
    public partial class ModifyModels2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Year",
                table: "Episodes",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Year",
                table: "Episodes");
        }
    }
}
