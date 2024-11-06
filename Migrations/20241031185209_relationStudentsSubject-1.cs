using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AcademicNet.Migrations
{
    /// <inheritdoc />
    public partial class relationStudentsSubject1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentSubjectModel_Students_StudentId",
                table: "StudentSubjectModel");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentSubjectModel_SubjectModel_SubjectId",
                table: "StudentSubjectModel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SubjectModel",
                table: "SubjectModel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StudentSubjectModel",
                table: "StudentSubjectModel");

            migrationBuilder.RenameTable(
                name: "SubjectModel",
                newName: "Subject");

            migrationBuilder.RenameTable(
                name: "StudentSubjectModel",
                newName: "StudentSubject");

            migrationBuilder.RenameIndex(
                name: "IX_StudentSubjectModel_SubjectId",
                table: "StudentSubject",
                newName: "IX_StudentSubject_SubjectId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Subject",
                table: "Subject",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudentSubject",
                table: "StudentSubject",
                columns: new[] { "StudentId", "SubjectId" });

            migrationBuilder.AddForeignKey(
                name: "FK_StudentSubject_Students_StudentId",
                table: "StudentSubject",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentSubject_Subject_SubjectId",
                table: "StudentSubject",
                column: "SubjectId",
                principalTable: "Subject",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentSubject_Students_StudentId",
                table: "StudentSubject");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentSubject_Subject_SubjectId",
                table: "StudentSubject");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Subject",
                table: "Subject");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StudentSubject",
                table: "StudentSubject");

            migrationBuilder.RenameTable(
                name: "Subject",
                newName: "SubjectModel");

            migrationBuilder.RenameTable(
                name: "StudentSubject",
                newName: "StudentSubjectModel");

            migrationBuilder.RenameIndex(
                name: "IX_StudentSubject_SubjectId",
                table: "StudentSubjectModel",
                newName: "IX_StudentSubjectModel_SubjectId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SubjectModel",
                table: "SubjectModel",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudentSubjectModel",
                table: "StudentSubjectModel",
                columns: new[] { "StudentId", "SubjectId" });

            migrationBuilder.AddForeignKey(
                name: "FK_StudentSubjectModel_Students_StudentId",
                table: "StudentSubjectModel",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentSubjectModel_SubjectModel_SubjectId",
                table: "StudentSubjectModel",
                column: "SubjectId",
                principalTable: "SubjectModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
