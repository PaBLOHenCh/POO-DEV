using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace AcademicNet.Migrations
{
    /// <inheritdoc />
    public partial class ClassSubject2Table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClassModel_Coordinators_CoordinatorModelId",
                table: "ClassModel");

            migrationBuilder.DropForeignKey(
                name: "FK_ClassSubjectModel_ClassModel_ClassId",
                table: "ClassSubjectModel");

            migrationBuilder.DropForeignKey(
                name: "FK_ClassSubjectModel_Subjects_SubjectId",
                table: "ClassSubjectModel");

            migrationBuilder.DropForeignKey(
                name: "FK_ClassSubjectModel_Teachers_TeacherModelId",
                table: "ClassSubjectModel");

            migrationBuilder.DropForeignKey(
                name: "FK_Matriculation_ClassSubjectModel_ClassSubjectClassId_ClassSu~",
                table: "Matriculation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Matriculation",
                table: "Matriculation");

            migrationBuilder.DropIndex(
                name: "IX_ClassModel_CoordinatorModelId",
                table: "ClassModel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ClassSubjectModel",
                table: "ClassSubjectModel");

            migrationBuilder.DropIndex(
                name: "IX_ClassSubjectModel_TeacherModelId",
                table: "ClassSubjectModel");

            migrationBuilder.DropColumn(
                name: "CoordinatorModelId",
                table: "ClassModel");

            migrationBuilder.DropColumn(
                name: "TeacherModelId",
                table: "ClassSubjectModel");

            migrationBuilder.RenameTable(
                name: "ClassSubjectModel",
                newName: "ClassSubject");

            migrationBuilder.RenameIndex(
                name: "IX_ClassSubjectModel_SubjectId",
                table: "ClassSubject",
                newName: "IX_ClassSubject_SubjectId");

            migrationBuilder.AddColumn<int>(
                name: "ClassId",
                table: "Students",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Matriculation",
                table: "Matriculation",
                columns: new[] { "StudentId", "SubjectId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_ClassSubject",
                table: "ClassSubject",
                columns: new[] { "ClassId", "SubjectId" });

            migrationBuilder.CreateTable(
                name: "UnitModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    AVGGradePerClass = table.Column<float>(type: "real", nullable: false),
                    AVGGradePerClassSubject = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnitModel", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Students_ClassId",
                table: "Students",
                column: "ClassId");

            migrationBuilder.CreateIndex(
                name: "IX_ClassModel_CoordinatorId",
                table: "ClassModel",
                column: "CoordinatorId");

            migrationBuilder.CreateIndex(
                name: "IX_ClassModel_UnitId",
                table: "ClassModel",
                column: "UnitId");

            migrationBuilder.CreateIndex(
                name: "IX_ClassSubject_TeacherId",
                table: "ClassSubject",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_ClassSubject_UnitId",
                table: "ClassSubject",
                column: "UnitId");

            migrationBuilder.AddForeignKey(
                name: "FK_ClassModel_Coordinators_CoordinatorId",
                table: "ClassModel",
                column: "CoordinatorId",
                principalTable: "Coordinators",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ClassModel_UnitModel_UnitId",
                table: "ClassModel",
                column: "UnitId",
                principalTable: "UnitModel",
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
                name: "FK_ClassSubject_Subjects_SubjectId",
                table: "ClassSubject",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ClassSubject_Teachers_TeacherId",
                table: "ClassSubject",
                column: "TeacherId",
                principalTable: "Teachers",
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
                name: "FK_Matriculation_ClassSubject_ClassSubjectClassId_ClassSubject~",
                table: "Matriculation",
                columns: new[] { "ClassSubjectClassId", "ClassSubjectSubjectId" },
                principalTable: "ClassSubject",
                principalColumns: new[] { "ClassId", "SubjectId" },
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_ClassModel_ClassId",
                table: "Students",
                column: "ClassId",
                principalTable: "ClassModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClassModel_Coordinators_CoordinatorId",
                table: "ClassModel");

            migrationBuilder.DropForeignKey(
                name: "FK_ClassModel_UnitModel_UnitId",
                table: "ClassModel");

            migrationBuilder.DropForeignKey(
                name: "FK_ClassSubject_ClassModel_ClassId",
                table: "ClassSubject");

            migrationBuilder.DropForeignKey(
                name: "FK_ClassSubject_Subjects_SubjectId",
                table: "ClassSubject");

            migrationBuilder.DropForeignKey(
                name: "FK_ClassSubject_Teachers_TeacherId",
                table: "ClassSubject");

            migrationBuilder.DropForeignKey(
                name: "FK_ClassSubject_UnitModel_UnitId",
                table: "ClassSubject");

            migrationBuilder.DropForeignKey(
                name: "FK_Matriculation_ClassSubject_ClassSubjectClassId_ClassSubject~",
                table: "Matriculation");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_ClassModel_ClassId",
                table: "Students");

            migrationBuilder.DropTable(
                name: "UnitModel");

            migrationBuilder.DropIndex(
                name: "IX_Students_ClassId",
                table: "Students");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Matriculation",
                table: "Matriculation");

            migrationBuilder.DropIndex(
                name: "IX_ClassModel_CoordinatorId",
                table: "ClassModel");

            migrationBuilder.DropIndex(
                name: "IX_ClassModel_UnitId",
                table: "ClassModel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ClassSubject",
                table: "ClassSubject");

            migrationBuilder.DropIndex(
                name: "IX_ClassSubject_TeacherId",
                table: "ClassSubject");

            migrationBuilder.DropIndex(
                name: "IX_ClassSubject_UnitId",
                table: "ClassSubject");

            migrationBuilder.DropColumn(
                name: "ClassId",
                table: "Students");

            migrationBuilder.RenameTable(
                name: "ClassSubject",
                newName: "ClassSubjectModel");

            migrationBuilder.RenameIndex(
                name: "IX_ClassSubject_SubjectId",
                table: "ClassSubjectModel",
                newName: "IX_ClassSubjectModel_SubjectId");

            migrationBuilder.AddColumn<int>(
                name: "CoordinatorModelId",
                table: "ClassModel",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TeacherModelId",
                table: "ClassSubjectModel",
                type: "integer",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Matriculation",
                table: "Matriculation",
                columns: new[] { "StudentId", "SubjectId", "ClassSubjectClassId", "ClassSubjectSubjectId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_ClassSubjectModel",
                table: "ClassSubjectModel",
                columns: new[] { "ClassId", "SubjectId" });

            migrationBuilder.CreateIndex(
                name: "IX_ClassModel_CoordinatorModelId",
                table: "ClassModel",
                column: "CoordinatorModelId");

            migrationBuilder.CreateIndex(
                name: "IX_ClassSubjectModel_TeacherModelId",
                table: "ClassSubjectModel",
                column: "TeacherModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_ClassModel_Coordinators_CoordinatorModelId",
                table: "ClassModel",
                column: "CoordinatorModelId",
                principalTable: "Coordinators",
                principalColumn: "Id");

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

            migrationBuilder.AddForeignKey(
                name: "FK_ClassSubjectModel_Teachers_TeacherModelId",
                table: "ClassSubjectModel",
                column: "TeacherModelId",
                principalTable: "Teachers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Matriculation_ClassSubjectModel_ClassSubjectClassId_ClassSu~",
                table: "Matriculation",
                columns: new[] { "ClassSubjectClassId", "ClassSubjectSubjectId" },
                principalTable: "ClassSubjectModel",
                principalColumns: new[] { "ClassId", "SubjectId" },
                onDelete: ReferentialAction.Cascade);
        }
    }
}
