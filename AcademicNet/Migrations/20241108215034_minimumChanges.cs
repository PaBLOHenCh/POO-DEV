using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AcademicNet.Migrations
{
    /// <inheritdoc />
    public partial class minimumChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClassModel_UnitModel_UnitId",
                table: "ClassModel");

            migrationBuilder.DropForeignKey(
                name: "FK_ClassSubject_UnitModel_UnitId",
                table: "ClassSubject");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentStudiesGroupModel_StudiesGroupModel_StudiesGroupId",
                table: "StudentStudiesGroupModel");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_ClassModel_ClassId",
                table: "Students");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UnitModel",
                table: "UnitModel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StudiesGroupModel",
                table: "StudiesGroupModel");

            migrationBuilder.DropColumn(
                name: "AVGGradePerStudent",
                table: "ClassModel");

            migrationBuilder.DropColumn(
                name: "AVGGradePerClass",
                table: "UnitModel");

            migrationBuilder.DropColumn(
                name: "AVGGradePerClassSubject",
                table: "UnitModel");

            migrationBuilder.RenameTable(
                name: "UnitModel",
                newName: "Units");

            migrationBuilder.RenameTable(
                name: "StudiesGroupModel",
                newName: "StudiesGroups");

            migrationBuilder.AlterColumn<int>(
                name: "ClassId",
                table: "Students",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "ClassModel",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Units",
                table: "Units",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudiesGroups",
                table: "StudiesGroups",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ClassModel_Units_UnitId",
                table: "ClassModel",
                column: "UnitId",
                principalTable: "Units",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ClassSubject_Units_UnitId",
                table: "ClassSubject",
                column: "UnitId",
                principalTable: "Units",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentStudiesGroupModel_StudiesGroups_StudiesGroupId",
                table: "StudentStudiesGroupModel",
                column: "StudiesGroupId",
                principalTable: "StudiesGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_ClassModel_ClassId",
                table: "Students",
                column: "ClassId",
                principalTable: "ClassModel",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClassModel_Units_UnitId",
                table: "ClassModel");

            migrationBuilder.DropForeignKey(
                name: "FK_ClassSubject_Units_UnitId",
                table: "ClassSubject");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentStudiesGroupModel_StudiesGroups_StudiesGroupId",
                table: "StudentStudiesGroupModel");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_ClassModel_ClassId",
                table: "Students");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Units",
                table: "Units");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StudiesGroups",
                table: "StudiesGroups");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "ClassModel");

            migrationBuilder.RenameTable(
                name: "Units",
                newName: "UnitModel");

            migrationBuilder.RenameTable(
                name: "StudiesGroups",
                newName: "StudiesGroupModel");

            migrationBuilder.AlterColumn<int>(
                name: "ClassId",
                table: "Students",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddColumn<float>(
                name: "AVGGradePerStudent",
                table: "ClassModel",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "AVGGradePerClass",
                table: "UnitModel",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "AVGGradePerClassSubject",
                table: "UnitModel",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UnitModel",
                table: "UnitModel",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudiesGroupModel",
                table: "StudiesGroupModel",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ClassModel_UnitModel_UnitId",
                table: "ClassModel",
                column: "UnitId",
                principalTable: "UnitModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ClassSubject_UnitModel_UnitId",
                table: "ClassSubject",
                column: "UnitId",
                principalTable: "UnitModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentStudiesGroupModel_StudiesGroupModel_StudiesGroupId",
                table: "StudentStudiesGroupModel",
                column: "StudiesGroupId",
                principalTable: "StudiesGroupModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_ClassModel_ClassId",
                table: "Students",
                column: "ClassId",
                principalTable: "ClassModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
