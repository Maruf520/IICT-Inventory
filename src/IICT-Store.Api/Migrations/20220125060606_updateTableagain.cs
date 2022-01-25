using Microsoft.EntityFrameworkCore.Migrations;

namespace IICT_Store.Api.Migrations
{
    public partial class updateTableagain : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DamagedProductSerialNo_DamagedProducts_DamagedProductId1",
                table: "DamagedProductSerialNo");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DamagedProductSerialNo",
                table: "DamagedProductSerialNo");

            migrationBuilder.RenameTable(
                name: "DamagedProductSerialNo",
                newName: "DamagedProductSerialNos");

            migrationBuilder.RenameIndex(
                name: "IX_DamagedProductSerialNo_DamagedProductId1",
                table: "DamagedProductSerialNos",
                newName: "IX_DamagedProductSerialNos_DamagedProductId1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DamagedProductSerialNos",
                table: "DamagedProductSerialNos",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DamagedProductSerialNos_DamagedProducts_DamagedProductId1",
                table: "DamagedProductSerialNos",
                column: "DamagedProductId1",
                principalTable: "DamagedProducts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DamagedProductSerialNos_DamagedProducts_DamagedProductId1",
                table: "DamagedProductSerialNos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DamagedProductSerialNos",
                table: "DamagedProductSerialNos");

            migrationBuilder.RenameTable(
                name: "DamagedProductSerialNos",
                newName: "DamagedProductSerialNo");

            migrationBuilder.RenameIndex(
                name: "IX_DamagedProductSerialNos_DamagedProductId1",
                table: "DamagedProductSerialNo",
                newName: "IX_DamagedProductSerialNo_DamagedProductId1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DamagedProductSerialNo",
                table: "DamagedProductSerialNo",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DamagedProductSerialNo_DamagedProducts_DamagedProductId1",
                table: "DamagedProductSerialNo",
                column: "DamagedProductId1",
                principalTable: "DamagedProducts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
