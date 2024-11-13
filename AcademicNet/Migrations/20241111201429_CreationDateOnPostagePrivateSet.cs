using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AcademicNet.Migrations
{
    /// <inheritdoc />
    public partial class CreationDateOnPostagePrivateSet : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostageModel_PostageModel_ParentPostageId",
                table: "PostageModel");

            migrationBuilder.DropForeignKey(
                name: "FK_PostageModel_StudentStudiesGroupModel_StudentStudiesGroupSt~",
                table: "PostageModel");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentStudiesGroupModel_Students_StudentId",
                table: "StudentStudiesGroupModel");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentStudiesGroupModel_StudiesGroups_StudiesGroupId",
                table: "StudentStudiesGroupModel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StudentStudiesGroupModel",
                table: "StudentStudiesGroupModel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PostageModel",
                table: "PostageModel");

            migrationBuilder.RenameTable(
                name: "StudentStudiesGroupModel",
                newName: "StudentStudiesGroups");

            migrationBuilder.RenameTable(
                name: "PostageModel",
                newName: "Postages");

            migrationBuilder.RenameIndex(
                name: "IX_StudentStudiesGroupModel_StudentId",
                table: "StudentStudiesGroups",
                newName: "IX_StudentStudiesGroups_StudentId");

            migrationBuilder.RenameIndex(
                name: "IX_PostageModel_StudentStudiesGroupStudiesGroupId_StudentStudi~",
                table: "Postages",
                newName: "IX_Postages_StudentStudiesGroupStudiesGroupId_StudentStudiesGr~");

            migrationBuilder.RenameIndex(
                name: "IX_PostageModel_ParentPostageId",
                table: "Postages",
                newName: "IX_Postages_ParentPostageId");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "StudiesGroups",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "TextBody",
                table: "Postages",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "PathToPhoto",
                table: "Postages",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDate",
                table: "Postages",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "CURRENT_TIMESTAMP",
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudentStudiesGroups",
                table: "StudentStudiesGroups",
                columns: new[] { "StudiesGroupId", "StudentId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Postages",
                table: "Postages",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Postages_Postages_ParentPostageId",
                table: "Postages",
                column: "ParentPostageId",
                principalTable: "Postages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Postages_StudentStudiesGroups_StudentStudiesGroupStudiesGro~",
                table: "Postages",
                columns: new[] { "StudentStudiesGroupStudiesGroupId", "StudentStudiesGroupStudentId" },
                principalTable: "StudentStudiesGroups",
                principalColumns: new[] { "StudiesGroupId", "StudentId" },
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentStudiesGroups_Students_StudentId",
                table: "StudentStudiesGroups",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentStudiesGroups_StudiesGroups_StudiesGroupId",
                table: "StudentStudiesGroups",
                column: "StudiesGroupId",
                principalTable: "StudiesGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Postages_Postages_ParentPostageId",
                table: "Postages");

            migrationBuilder.DropForeignKey(
                name: "FK_Postages_StudentStudiesGroups_StudentStudiesGroupStudiesGro~",
                table: "Postages");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentStudiesGroups_Students_StudentId",
                table: "StudentStudiesGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentStudiesGroups_StudiesGroups_StudiesGroupId",
                table: "StudentStudiesGroups");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StudentStudiesGroups",
                table: "StudentStudiesGroups");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Postages",
                table: "Postages");

            migrationBuilder.RenameTable(
                name: "StudentStudiesGroups",
                newName: "StudentStudiesGroupModel");

            migrationBuilder.RenameTable(
                name: "Postages",
                newName: "PostageModel");

            migrationBuilder.RenameIndex(
                name: "IX_StudentStudiesGroups_StudentId",
                table: "StudentStudiesGroupModel",
                newName: "IX_StudentStudiesGroupModel_StudentId");

            migrationBuilder.RenameIndex(
                name: "IX_Postages_StudentStudiesGroupStudiesGroupId_StudentStudiesGr~",
                table: "PostageModel",
                newName: "IX_PostageModel_StudentStudiesGroupStudiesGroupId_StudentStudi~");

            migrationBuilder.RenameIndex(
                name: "IX_Postages_ParentPostageId",
                table: "PostageModel",
                newName: "IX_PostageModel_ParentPostageId");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "StudiesGroups",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "TextBody",
                table: "PostageModel",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PathToPhoto",
                table: "PostageModel",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDate",
                table: "PostageModel",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValueSql: "CURRENT_TIMESTAMP");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudentStudiesGroupModel",
                table: "StudentStudiesGroupModel",
                columns: new[] { "StudiesGroupId", "StudentId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_PostageModel",
                table: "PostageModel",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PostageModel_PostageModel_ParentPostageId",
                table: "PostageModel",
                column: "ParentPostageId",
                principalTable: "PostageModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PostageModel_StudentStudiesGroupModel_StudentStudiesGroupSt~",
                table: "PostageModel",
                columns: new[] { "StudentStudiesGroupStudiesGroupId", "StudentStudiesGroupStudentId" },
                principalTable: "StudentStudiesGroupModel",
                principalColumns: new[] { "StudiesGroupId", "StudentId" },
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentStudiesGroupModel_Students_StudentId",
                table: "StudentStudiesGroupModel",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentStudiesGroupModel_StudiesGroups_StudiesGroupId",
                table: "StudentStudiesGroupModel",
                column: "StudiesGroupId",
                principalTable: "StudiesGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
