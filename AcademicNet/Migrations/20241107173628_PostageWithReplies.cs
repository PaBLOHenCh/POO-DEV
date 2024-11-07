using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AcademicNet.Migrations
{
    /// <inheritdoc />
    public partial class PostageWithReplies : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ParentPostageId",
                table: "PostageModel",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PostageModel_ParentPostageId",
                table: "PostageModel",
                column: "ParentPostageId");

            migrationBuilder.AddForeignKey(
                name: "FK_PostageModel_PostageModel_ParentPostageId",
                table: "PostageModel",
                column: "ParentPostageId",
                principalTable: "PostageModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostageModel_PostageModel_ParentPostageId",
                table: "PostageModel");

            migrationBuilder.DropIndex(
                name: "IX_PostageModel_ParentPostageId",
                table: "PostageModel");

            migrationBuilder.DropColumn(
                name: "ParentPostageId",
                table: "PostageModel");
        }
    }
}
