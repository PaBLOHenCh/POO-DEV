using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AcademicNet.Migrations
{
    /// <inheritdoc />
    public partial class versao2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClassSubjectModel_Subjects_SubjectModelId",
                table: "ClassSubjectModel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Matriculation",
                table: "Matriculation");

            migrationBuilder.DropIndex(
                name: "IX_ClassSubjectModel_SubjectModelId",
                table: "ClassSubjectModel");

            migrationBuilder.DropColumn(
                name: "StudentSubjectId",
                table: "ClassSubjectModel");

            migrationBuilder.DropColumn(
                name: "SubjectModelId",
                table: "ClassSubjectModel");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Matriculation",
                table: "Matriculation",
                columns: new[] { "StudentId", "SubjectId", "ClassSubjectClassId", "ClassSubjectSubjectId" });

            migrationBuilder.CreateIndex(
                name: "IX_ClassSubjectModel_SubjectId",
                table: "ClassSubjectModel",
                column: "SubjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_ClassSubjectModel_ClassModel_ClassId",
                table: "ClassSubjectModel",
                column: "ClassId",
                principalTable: "ClassModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ClassSubjectModel_Subjects_SubjectId",
                table: "ClassSubjectModel",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClassSubjectModel_ClassModel_ClassId",
                table: "ClassSubjectModel");

            migrationBuilder.DropForeignKey(
                name: "FK_ClassSubjectModel_Subjects_SubjectId",
                table: "ClassSubjectModel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Matriculation",
                table: "Matriculation");

            migrationBuilder.DropIndex(
                name: "IX_ClassSubjectModel_SubjectId",
                table: "ClassSubjectModel");

            migrationBuilder.AddColumn<int>(
                name: "StudentSubjectId",
                table: "ClassSubjectModel",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SubjectModelId",
                table: "ClassSubjectModel",
                type: "integer",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Matriculation",
                table: "Matriculation",
                columns: new[] { "StudentId", "SubjectId" });

            migrationBuilder.CreateIndex(
                name: "IX_ClassSubjectModel_SubjectModelId",
                table: "ClassSubjectModel",
                column: "SubjectModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_ClassSubjectModel_Subjects_SubjectModelId",
                table: "ClassSubjectModel",
                column: "SubjectModelId",
                principalTable: "Subjects",
                principalColumn: "Id");
        }
    }
}
