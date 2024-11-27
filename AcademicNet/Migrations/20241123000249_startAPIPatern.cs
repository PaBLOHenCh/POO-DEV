using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AcademicNet.Migrations
{
    /// <inheritdoc />
    public partial class startAPIPatern : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "AVGFrequencyPerClass",
                table: "Units",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "AVGFrequencyPerClassSubject",
                table: "Units",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "AVGGradePerClass",
                table: "Units",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "AVGGradePerClassSubject",
                table: "Units",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "AVGFrequency",
                table: "ClassSubject",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "AVGGrade",
                table: "ClassSubject",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "AVGFrequency",
                table: "ClassModel",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "AVGGrade",
                table: "ClassModel",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AVGFrequencyPerClass",
                table: "Units");

            migrationBuilder.DropColumn(
                name: "AVGFrequencyPerClassSubject",
                table: "Units");

            migrationBuilder.DropColumn(
                name: "AVGGradePerClass",
                table: "Units");

            migrationBuilder.DropColumn(
                name: "AVGGradePerClassSubject",
                table: "Units");

            migrationBuilder.DropColumn(
                name: "AVGFrequency",
                table: "ClassSubject");

            migrationBuilder.DropColumn(
                name: "AVGGrade",
                table: "ClassSubject");

            migrationBuilder.DropColumn(
                name: "AVGFrequency",
                table: "ClassModel");

            migrationBuilder.DropColumn(
                name: "AVGGrade",
                table: "ClassModel");
        }
    }
}
