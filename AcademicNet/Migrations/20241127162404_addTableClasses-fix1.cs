using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AcademicNet.Migrations
{
    /// <inheritdoc />
    public partial class addTableClassesfix1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "AVGGradeFrequency",
                table: "ClassSubject",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AVGGradeFrequency",
                table: "ClassSubject");
        }
    }
}
