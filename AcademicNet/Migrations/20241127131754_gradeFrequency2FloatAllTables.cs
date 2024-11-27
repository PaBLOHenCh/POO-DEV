using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AcademicNet.Migrations
{
    /// <inheritdoc />
    public partial class gradeFrequency2FloatAllTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "AVGGradeFrequency",
                table: "Students",
                type: "real",
                nullable: true);

            migrationBuilder.AlterColumn<float>(
                name: "Grade",
                table: "Matriculation",
                type: "real",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AlterColumn<float>(
                name: "Frequency",
                table: "Matriculation",
                type: "real",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AVGGradeFrequency",
                table: "Students");

            migrationBuilder.AlterColumn<decimal>(
                name: "Grade",
                table: "Matriculation",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<decimal>(
                name: "Frequency",
                table: "Matriculation",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");
        }
    }
}
