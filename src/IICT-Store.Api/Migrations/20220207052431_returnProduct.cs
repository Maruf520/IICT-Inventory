using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IICT_Store.Api.Migrations
{
    public partial class returnProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ReturnedProducts",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    ReceiverId = table.Column<int>(type: "int", nullable: false),
                    ReceiverId1 = table.Column<long>(type: "bigint", nullable: true),
                    SenderId = table.Column<int>(type: "int", nullable: false),
                    SenderId1 = table.Column<long>(type: "bigint", nullable: true),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReturnedProducts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReturnedProducts_Persons_ReceiverId1",
                        column: x => x.ReceiverId1,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReturnedProducts_Persons_SenderId1",
                        column: x => x.SenderId1,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ReturnedProductSerialNos",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductNoId = table.Column<long>(type: "bigint", nullable: false),
                    ReturnedProductId = table.Column<int>(type: "int", nullable: false),
                    ReturnedProductId1 = table.Column<long>(type: "bigint", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReturnedProductSerialNos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReturnedProductSerialNos_ReturnedProducts_ReturnedProductId1",
                        column: x => x.ReturnedProductId1,
                        principalTable: "ReturnedProducts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ReturnedProducts_ReceiverId1",
                table: "ReturnedProducts",
                column: "ReceiverId1");

            migrationBuilder.CreateIndex(
                name: "IX_ReturnedProducts_SenderId1",
                table: "ReturnedProducts",
                column: "SenderId1");

            migrationBuilder.CreateIndex(
                name: "IX_ReturnedProductSerialNos_ReturnedProductId1",
                table: "ReturnedProductSerialNos",
                column: "ReturnedProductId1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReturnedProductSerialNos");

            migrationBuilder.DropTable(
                name: "ReturnedProducts");
        }
    }
}
