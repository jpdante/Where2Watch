using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Where2Watch.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    Guid = table.Column<string>(maxLength: 32, nullable: false),
                    Email = table.Column<string>(maxLength: 255, nullable: false),
                    Username = table.Column<string>(maxLength: 32, nullable: false),
                    Password = table.Column<string>(nullable: false),
                    LastAccess = table.Column<DateTime>(type: "TIMESTAMP", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Platforms",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    Name = table.Column<string>(maxLength: 128, nullable: false),
                    Icon = table.Column<string>(nullable: false),
                    Link = table.Column<string>(nullable: false),
                    Country = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Platforms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Titles",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    Type = table.Column<int>(nullable: false),
                    OriginalName = table.Column<string>(nullable: false),
                    Length = table.Column<int>(nullable: false),
                    Seasons = table.Column<int>(nullable: false),
                    Episodes = table.Column<int>(nullable: false),
                    Poster = table.Column<string>(nullable: true),
                    Summary = table.Column<string>(nullable: true),
                    Likes = table.Column<uint>(nullable: false),
                    Dislikes = table.Column<uint>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Titles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Seasons",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    TitleId = table.Column<long>(nullable: false),
                    Number = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Seasons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Seasons_Titles_TitleId",
                        column: x => x.TitleId,
                        principalTable: "Titles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TitleAvailabilities",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    TitleId = table.Column<long>(nullable: false),
                    PlatformId = table.Column<long>(nullable: false),
                    Country = table.Column<int>(nullable: false),
                    Link = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TitleAvailabilities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TitleAvailabilities_Platforms_PlatformId",
                        column: x => x.PlatformId,
                        principalTable: "Platforms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TitleAvailabilities_Titles_TitleId",
                        column: x => x.TitleId,
                        principalTable: "Titles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TitleLikes",
                columns: table => new
                {
                    AccountId = table.Column<long>(nullable: false),
                    TitleId = table.Column<long>(nullable: false),
                    IsLike = table.Column<bool>(nullable: true),
                    LastUpdate = table.Column<DateTime>(type: "TIMESTAMP", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TitleLikes", x => new { x.AccountId, x.TitleId });
                    table.ForeignKey(
                        name: "FK_TitleLikes_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TitleLikes_Titles_TitleId",
                        column: x => x.TitleId,
                        principalTable: "Titles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TitleNames",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    TitleId = table.Column<long>(nullable: false),
                    Country = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TitleNames", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TitleNames_Titles_TitleId",
                        column: x => x.TitleId,
                        principalTable: "Titles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Episodes",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    TitleId = table.Column<long>(nullable: false),
                    SeasonId = table.Column<long>(nullable: false),
                    Number = table.Column<int>(nullable: false),
                    OriginalName = table.Column<string>(maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Episodes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Episodes_Seasons_SeasonId",
                        column: x => x.SeasonId,
                        principalTable: "Seasons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Episodes_Titles_TitleId",
                        column: x => x.TitleId,
                        principalTable: "Titles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EpisodeNames",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    EpisodeId = table.Column<long>(nullable: false),
                    Country = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EpisodeNames", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EpisodeNames_Episodes_EpisodeId",
                        column: x => x.EpisodeId,
                        principalTable: "Episodes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_Email",
                table: "Accounts",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_Guid",
                table: "Accounts",
                column: "Guid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_Username",
                table: "Accounts",
                column: "Username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EpisodeNames_Country",
                table: "EpisodeNames",
                column: "Country");

            migrationBuilder.CreateIndex(
                name: "IX_EpisodeNames_EpisodeId",
                table: "EpisodeNames",
                column: "EpisodeId");

            migrationBuilder.CreateIndex(
                name: "IX_Episodes_SeasonId",
                table: "Episodes",
                column: "SeasonId");

            migrationBuilder.CreateIndex(
                name: "IX_Episodes_TitleId",
                table: "Episodes",
                column: "TitleId");

            migrationBuilder.CreateIndex(
                name: "IX_Platforms_Country",
                table: "Platforms",
                column: "Country");

            migrationBuilder.CreateIndex(
                name: "IX_Seasons_TitleId",
                table: "Seasons",
                column: "TitleId");

            migrationBuilder.CreateIndex(
                name: "IX_TitleAvailabilities_Country",
                table: "TitleAvailabilities",
                column: "Country");

            migrationBuilder.CreateIndex(
                name: "IX_TitleAvailabilities_PlatformId",
                table: "TitleAvailabilities",
                column: "PlatformId");

            migrationBuilder.CreateIndex(
                name: "IX_TitleAvailabilities_TitleId",
                table: "TitleAvailabilities",
                column: "TitleId");

            migrationBuilder.CreateIndex(
                name: "IX_TitleLikes_AccountId",
                table: "TitleLikes",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_TitleLikes_TitleId",
                table: "TitleLikes",
                column: "TitleId");

            migrationBuilder.CreateIndex(
                name: "IX_TitleNames_Country",
                table: "TitleNames",
                column: "Country");

            migrationBuilder.CreateIndex(
                name: "IX_TitleNames_TitleId",
                table: "TitleNames",
                column: "TitleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EpisodeNames");

            migrationBuilder.DropTable(
                name: "TitleAvailabilities");

            migrationBuilder.DropTable(
                name: "TitleLikes");

            migrationBuilder.DropTable(
                name: "TitleNames");

            migrationBuilder.DropTable(
                name: "Episodes");

            migrationBuilder.DropTable(
                name: "Platforms");

            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "Seasons");

            migrationBuilder.DropTable(
                name: "Titles");
        }
    }
}
