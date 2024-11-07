using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace AcademicNet.Migrations
{
    /// <inheritdoc />
    public partial class Student_StudiesGroupWithPostage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PostageModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TextBody = table.Column<string>(type: "text", nullable: false),
                    PathToPhoto = table.Column<string>(type: "text", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    StudentStudiesGroupStudiesGroupId = table.Column<int>(type: "integer", nullable: false),
                    StudentStudiesGroupStudentId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostageModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PostageModel_StudentStudiesGroupModel_StudentStudiesGroupSt~",
                        columns: x => new { x.StudentStudiesGroupStudiesGroupId, x.StudentStudiesGroupStudentId },
                        principalTable: "StudentStudiesGroupModel",
                        principalColumns: new[] { "StudiesGroupId", "StudentId" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PostageModel_StudentStudiesGroupStudiesGroupId_StudentStudi~",
                table: "PostageModel",
                columns: new[] { "StudentStudiesGroupStudiesGroupId", "StudentStudiesGroupStudentId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PostageModel");
        }
    }
}
