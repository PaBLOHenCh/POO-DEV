using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AcademicNet.Migrations
{
    /// <inheritdoc />
    public partial class addTableClasses : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClassModel_Coordinators_CoordinatorId",
                table: "ClassModel");

            migrationBuilder.DropForeignKey(
                name: "FK_ClassModel_Units_UnitId",
                table: "ClassModel");

            migrationBuilder.DropForeignKey(
                name: "FK_ClassSubject_ClassModel_ClassId",
                table: "ClassSubject");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_ClassModel_ClassId",
                table: "Students");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ClassModel",
                table: "ClassModel");

            migrationBuilder.RenameTable(
                name: "ClassModel",
                newName: "Classes");

            migrationBuilder.RenameIndex(
                name: "IX_ClassModel_UnitId",
                table: "Classes",
                newName: "IX_Classes_UnitId");

            migrationBuilder.RenameIndex(
                name: "IX_ClassModel_CoordinatorId",
                table: "Classes",
                newName: "IX_Classes_CoordinatorId");

            migrationBuilder.AddColumn<float>(
                name: "AVGGradeFrequencyPerClass",
                table: "Units",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "AVGGradeFrequencyPerClassSubject",
                table: "Units",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "GradeFrequency",
                table: "Matriculation",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "AVGGradeFrequency",
                table: "Classes",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Classes",
                table: "Classes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ClassSubject_Classes_ClassId",
                table: "ClassSubject",
                column: "ClassId",
                principalTable: "Classes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Classes_Coordinators_CoordinatorId",
                table: "Classes",
                column: "CoordinatorId",
                principalTable: "Coordinators",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Classes_Units_UnitId",
                table: "Classes",
                column: "UnitId",
                principalTable: "Units",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Classes_ClassId",
                table: "Students",
                column: "ClassId",
                principalTable: "Classes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClassSubject_Classes_ClassId",
                table: "ClassSubject");

            migrationBuilder.DropForeignKey(
                name: "FK_Classes_Coordinators_CoordinatorId",
                table: "Classes");

            migrationBuilder.DropForeignKey(
                name: "FK_Classes_Units_UnitId",
                table: "Classes");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Classes_ClassId",
                table: "Students");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Classes",
                table: "Classes");

            migrationBuilder.DropColumn(
                name: "AVGGradeFrequencyPerClass",
                table: "Units");

            migrationBuilder.DropColumn(
                name: "AVGGradeFrequencyPerClassSubject",
                table: "Units");

            migrationBuilder.DropColumn(
                name: "GradeFrequency",
                table: "Matriculation");

            migrationBuilder.DropColumn(
                name: "AVGGradeFrequency",
                table: "Classes");

            migrationBuilder.RenameTable(
                name: "Classes",
                newName: "ClassModel");

            migrationBuilder.RenameIndex(
                name: "IX_Classes_UnitId",
                table: "ClassModel",
                newName: "IX_ClassModel_UnitId");

            migrationBuilder.RenameIndex(
                name: "IX_Classes_CoordinatorId",
                table: "ClassModel",
                newName: "IX_ClassModel_CoordinatorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ClassModel",
                table: "ClassModel",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ClassModel_Coordinators_CoordinatorId",
                table: "ClassModel",
                column: "CoordinatorId",
                principalTable: "Coordinators",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ClassModel_Units_UnitId",
                table: "ClassModel",
                column: "UnitId",
                principalTable: "Units",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ClassSubject_ClassModel_ClassId",
                table: "ClassSubject",
                column: "ClassId",
                principalTable: "ClassModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_ClassModel_ClassId",
                table: "Students",
                column: "ClassId",
                principalTable: "ClassModel",
                principalColumn: "Id");
        }
    }
}
