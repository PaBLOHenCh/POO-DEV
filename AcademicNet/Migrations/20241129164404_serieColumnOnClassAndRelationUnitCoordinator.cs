using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AcademicNet.Migrations
{
    /// <inheritdoc />
    public partial class serieColumnOnClassAndRelationUnitCoordinator : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UnitId",
                table: "Coordinators",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Grade",
                table: "Classes",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Coordinators_UnitId",
                table: "Coordinators",
                column: "UnitId");

            migrationBuilder.AddForeignKey(
                name: "FK_Coordinators_Units_UnitId",
                table: "Coordinators",
                column: "UnitId",
                principalTable: "Units",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Coordinators_Units_UnitId",
                table: "Coordinators");

            migrationBuilder.DropIndex(
                name: "IX_Coordinators_UnitId",
                table: "Coordinators");

            migrationBuilder.DropColumn(
                name: "UnitId",
                table: "Coordinators");

            migrationBuilder.DropColumn(
                name: "Grade",
                table: "Classes");
        }
    }
}
