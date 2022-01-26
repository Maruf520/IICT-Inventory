using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IICT_Store.Api.Migrations
{
    public partial class updateTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DamagedProductSerialNo",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductNoId = table.Column<long>(type: "bigint", nullable: false),
                    DamagedProductId = table.Column<int>(type: "int", nullable: false),
                    DamagedProductId1 = table.Column<long>(type: "bigint", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DamagedProductSerialNo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DamagedProductSerialNo_DamagedProducts_DamagedProductId1",
                        column: x => x.DamagedProductId1,
                        principalTable: "DamagedProducts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DamagedProductSerialNo_DamagedProductId1",
                table: "DamagedProductSerialNo",
                column: "DamagedProductId1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DamagedProductSerialNo");
        }
    }
}
