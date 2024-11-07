using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace AcademicNet.Migrations
{
    /// <inheritdoc />
    public partial class Student_studiesGroup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StudiesGroupModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudiesGroupModel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StudentStudiesGroupModel",
                columns: table => new
                {
                    StudentId = table.Column<int>(type: "integer", nullable: false),
                    StudiesGroupId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentStudiesGroupModel", x => new { x.StudiesGroupId, x.StudentId });
                    table.ForeignKey(
                        name: "FK_StudentStudiesGroupModel_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentStudiesGroupModel_StudiesGroupModel_StudiesGroupId",
                        column: x => x.StudiesGroupId,
                        principalTable: "StudiesGroupModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StudentStudiesGroupModel_StudentId",
                table: "StudentStudiesGroupModel",
                column: "StudentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StudentStudiesGroupModel");

            migrationBuilder.DropTable(
                name: "StudiesGroupModel");
        }
    }
}
