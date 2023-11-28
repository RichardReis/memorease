using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Ateno.Infra.Data.Migrations
{
    public partial class StudyProcess : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StudyProcess",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    StudyCardId = table.Column<int>(type: "int", nullable: false),
                    StudyDeckId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Repetitions = table.Column<int>(type: "int", nullable: false),
                    Learning = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    EFactor = table.Column<float>(type: "float", nullable: false),
                    NextStudy = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudyProcess", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudyProcess_StudyCard_StudyCardId",
                        column: x => x.StudyCardId,
                        principalTable: "StudyCard",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudyProcess_StudyDeck_StudyDeckId",
                        column: x => x.StudyDeckId,
                        principalTable: "StudyDeck",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudyProcess_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_StudyProcess_StudyCardId",
                table: "StudyProcess",
                column: "StudyCardId");

            migrationBuilder.CreateIndex(
                name: "IX_StudyProcess_StudyDeckId",
                table: "StudyProcess",
                column: "StudyDeckId");

            migrationBuilder.CreateIndex(
                name: "IX_StudyProcess_UserId",
                table: "StudyProcess",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StudyProcess");
        }
    }
}
