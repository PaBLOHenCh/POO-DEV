using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AcademicNet.Migrations
{
    /// <inheritdoc />
    public partial class fixAddressOverride1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Coordinators_Adresses_AddressIdEndereco",
                table: "Coordinators");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Adresses_AddressIdEndereco",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "FK_Teachers_Adresses_AddressIdEndereco",
                table: "Teachers");

            migrationBuilder.RenameColumn(
                name: "AddressIdEndereco",
                table: "Teachers",
                newName: "AddressId");

            migrationBuilder.RenameIndex(
                name: "IX_Teachers_AddressIdEndereco",
                table: "Teachers",
                newName: "IX_Teachers_AddressId");

            migrationBuilder.RenameColumn(
                name: "AddressIdEndereco",
                table: "Students",
                newName: "AddressId");

            migrationBuilder.RenameIndex(
                name: "IX_Students_AddressIdEndereco",
                table: "Students",
                newName: "IX_Students_AddressId");

            migrationBuilder.RenameColumn(
                name: "AddressIdEndereco",
                table: "Coordinators",
                newName: "AddressId");

            migrationBuilder.RenameIndex(
                name: "IX_Coordinators_AddressIdEndereco",
                table: "Coordinators",
                newName: "IX_Coordinators_AddressId");

            migrationBuilder.RenameColumn(
                name: "IdEndereco",
                table: "Adresses",
                newName: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Coordinators_Adresses_AddressId",
                table: "Coordinators",
                column: "AddressId",
                principalTable: "Adresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Adresses_AddressId",
                table: "Students",
                column: "AddressId",
                principalTable: "Adresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Teachers_Adresses_AddressId",
                table: "Teachers",
                column: "AddressId",
                principalTable: "Adresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Coordinators_Adresses_AddressId",
                table: "Coordinators");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Adresses_AddressId",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "FK_Teachers_Adresses_AddressId",
                table: "Teachers");

            migrationBuilder.RenameColumn(
                name: "AddressId",
                table: "Teachers",
                newName: "AddressIdEndereco");

            migrationBuilder.RenameIndex(
                name: "IX_Teachers_AddressId",
                table: "Teachers",
                newName: "IX_Teachers_AddressIdEndereco");

            migrationBuilder.RenameColumn(
                name: "AddressId",
                table: "Students",
                newName: "AddressIdEndereco");

            migrationBuilder.RenameIndex(
                name: "IX_Students_AddressId",
                table: "Students",
                newName: "IX_Students_AddressIdEndereco");

            migrationBuilder.RenameColumn(
                name: "AddressId",
                table: "Coordinators",
                newName: "AddressIdEndereco");

            migrationBuilder.RenameIndex(
                name: "IX_Coordinators_AddressId",
                table: "Coordinators",
                newName: "IX_Coordinators_AddressIdEndereco");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Adresses",
                newName: "IdEndereco");

            migrationBuilder.AddForeignKey(
                name: "FK_Coordinators_Adresses_AddressIdEndereco",
                table: "Coordinators",
                column: "AddressIdEndereco",
                principalTable: "Adresses",
                principalColumn: "IdEndereco",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Adresses_AddressIdEndereco",
                table: "Students",
                column: "AddressIdEndereco",
                principalTable: "Adresses",
                principalColumn: "IdEndereco",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Teachers_Adresses_AddressIdEndereco",
                table: "Teachers",
                column: "AddressIdEndereco",
                principalTable: "Adresses",
                principalColumn: "IdEndereco",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
