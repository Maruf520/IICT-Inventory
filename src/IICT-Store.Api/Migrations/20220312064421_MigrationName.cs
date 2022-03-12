using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IICT_Store.Api.Migrations
{
    public partial class MigrationName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bookings",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GalleryNo = table.Column<int>(type: "int", nullable: false),
                    Application = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BookingBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Purposes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MoneyReceipt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MoneyReceiptNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MaintenanceProductSerialNo",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductNoId = table.Column<long>(type: "bigint", nullable: false),
                    IsRepaired = table.Column<bool>(type: "bit", nullable: false),
                    MaintananceProductId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaintenanceProductSerialNo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Persons",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Designation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persons", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TimeSlots",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartTime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EndTime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeSlots", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QuantityInStock = table.Column<int>(type: "int", nullable: false),
                    TotalQuantity = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<long>(type: "bigint", nullable: false),
                    HasSerial = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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
                    FromRoom = table.Column<int>(type: "int", nullable: false),
                    FromPerson = table.Column<int>(type: "int", nullable: false),
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
                name: "BookingTimeSlots",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GalleryNo = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BookingId = table.Column<long>(type: "bigint", nullable: false),
                    TimeSlotId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookingTimeSlots", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BookingTimeSlots_Bookings_BookingId",
                        column: x => x.BookingId,
                        principalTable: "Bookings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookingTimeSlots_TimeSlots_TimeSlotId",
                        column: x => x.TimeSlotId,
                        principalTable: "TimeSlots",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DamagedProducts",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<long>(type: "bigint", nullable: false),
                    ReceiverId = table.Column<long>(type: "bigint", nullable: true),
                    SenderId = table.Column<long>(type: "bigint", nullable: true),
                    RoomNo = table.Column<int>(type: "int", nullable: false),
                    PersonId = table.Column<long>(type: "bigint", nullable: true),
                    DamagedFrom = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    WasNotDistributed = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DamagedProducts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DamagedProducts_Persons_ReceiverId",
                        column: x => x.ReceiverId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DamagedProducts_Persons_SenderId",
                        column: x => x.SenderId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DamagedProducts_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Distributions",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoomNo = table.Column<int>(type: "int", nullable: true),
                    ProductId = table.Column<long>(type: "bigint", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    NameOfUser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrderNo = table.Column<int>(type: "int", nullable: false),
                    TotalRemainingQuantity = table.Column<int>(type: "int", nullable: false),
                    SenderId = table.Column<long>(type: "bigint", nullable: false),
                    ReceiverId = table.Column<long>(type: "bigint", nullable: true),
                    DistributedTo = table.Column<long>(type: "bigint", nullable: false),
                    SignatureOfReceiver = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Distributions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Distributions_Persons_ReceiverId",
                        column: x => x.ReceiverId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Distributions_Persons_SenderId",
                        column: x => x.SenderId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Distributions_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MaintenanceProducts",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<long>(type: "bigint", nullable: false),
                    DistributionId = table.Column<long>(type: "bigint", nullable: false),
                    ReceiverId = table.Column<long>(type: "bigint", nullable: false),
                    SenderId = table.Column<long>(type: "bigint", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaintenanceProducts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MaintenanceProducts_Persons_ReceiverId",
                        column: x => x.ReceiverId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MaintenanceProducts_Persons_SenderId",
                        column: x => x.SenderId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MaintenanceProducts_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductNos",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductId = table.Column<long>(type: "bigint", nullable: false),
                    ProductStatus = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductNos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductNos_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Purchasheds",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<long>(type: "bigint", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Supplier = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PurchaseStatus = table.Column<int>(type: "int", nullable: false),
                    PaymentProcess = table.Column<int>(type: "int", nullable: false),
                    PaymentBy = table.Column<int>(type: "int", nullable: false),
                    Purchasedby = table.Column<int>(type: "int", nullable: false),
                    IsConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    ConfirmedBy = table.Column<int>(type: "int", nullable: false),
                    PuchasedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ConfirmDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Purchasheds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Purchasheds_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReturnedProductSerialNos",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductNoId = table.Column<long>(type: "bigint", nullable: false),
                    ReturnedProductId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReturnedProductSerialNos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReturnedProductSerialNos_ReturnedProducts_ReturnedProductId",
                        column: x => x.ReturnedProductId,
                        principalTable: "ReturnedProducts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DamagedProductSerialNos",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductNoId = table.Column<long>(type: "bigint", nullable: false),
                    DamagedProductId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DamagedProductSerialNos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DamagedProductSerialNos_DamagedProducts_DamagedProductId",
                        column: x => x.DamagedProductId,
                        principalTable: "DamagedProducts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductSerialNos",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductNoId = table.Column<long>(type: "bigint", nullable: false),
                    DistributionId = table.Column<long>(type: "bigint", nullable: false),
                    ProductStatus = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductSerialNos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductSerialNos_Distributions_DistributionId",
                        column: x => x.DistributionId,
                        principalTable: "Distributions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CashMemos",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PurchashedId = table.Column<long>(type: "bigint", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CashMemos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CashMemos_Purchasheds_PurchashedId",
                        column: x => x.PurchashedId,
                        principalTable: "Purchasheds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookingTimeSlots_BookingId",
                table: "BookingTimeSlots",
                column: "BookingId");

            migrationBuilder.CreateIndex(
                name: "IX_BookingTimeSlots_TimeSlotId",
                table: "BookingTimeSlots",
                column: "TimeSlotId");

            migrationBuilder.CreateIndex(
                name: "IX_CashMemos_PurchashedId",
                table: "CashMemos",
                column: "PurchashedId");

            migrationBuilder.CreateIndex(
                name: "IX_DamagedProducts_ProductId",
                table: "DamagedProducts",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_DamagedProducts_ReceiverId",
                table: "DamagedProducts",
                column: "ReceiverId");

            migrationBuilder.CreateIndex(
                name: "IX_DamagedProducts_SenderId",
                table: "DamagedProducts",
                column: "SenderId");

            migrationBuilder.CreateIndex(
                name: "IX_DamagedProductSerialNos_DamagedProductId",
                table: "DamagedProductSerialNos",
                column: "DamagedProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Distributions_ProductId",
                table: "Distributions",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Distributions_ReceiverId",
                table: "Distributions",
                column: "ReceiverId");

            migrationBuilder.CreateIndex(
                name: "IX_Distributions_SenderId",
                table: "Distributions",
                column: "SenderId");

            migrationBuilder.CreateIndex(
                name: "IX_MaintenanceProducts_ProductId",
                table: "MaintenanceProducts",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_MaintenanceProducts_ReceiverId",
                table: "MaintenanceProducts",
                column: "ReceiverId");

            migrationBuilder.CreateIndex(
                name: "IX_MaintenanceProducts_SenderId",
                table: "MaintenanceProducts",
                column: "SenderId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductNos_ProductId",
                table: "ProductNos",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductSerialNos_DistributionId",
                table: "ProductSerialNos",
                column: "DistributionId");

            migrationBuilder.CreateIndex(
                name: "IX_Purchasheds_ProductId",
                table: "Purchasheds",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ReturnedProducts_ReceiverId1",
                table: "ReturnedProducts",
                column: "ReceiverId1");

            migrationBuilder.CreateIndex(
                name: "IX_ReturnedProducts_SenderId1",
                table: "ReturnedProducts",
                column: "SenderId1");

            migrationBuilder.CreateIndex(
                name: "IX_ReturnedProductSerialNos_ReturnedProductId",
                table: "ReturnedProductSerialNos",
                column: "ReturnedProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookingTimeSlots");

            migrationBuilder.DropTable(
                name: "CashMemos");

            migrationBuilder.DropTable(
                name: "DamagedProductSerialNos");

            migrationBuilder.DropTable(
                name: "MaintenanceProducts");

            migrationBuilder.DropTable(
                name: "MaintenanceProductSerialNo");

            migrationBuilder.DropTable(
                name: "ProductNos");

            migrationBuilder.DropTable(
                name: "ProductSerialNos");

            migrationBuilder.DropTable(
                name: "ReturnedProductSerialNos");

            migrationBuilder.DropTable(
                name: "Bookings");

            migrationBuilder.DropTable(
                name: "TimeSlots");

            migrationBuilder.DropTable(
                name: "Purchasheds");

            migrationBuilder.DropTable(
                name: "DamagedProducts");

            migrationBuilder.DropTable(
                name: "Distributions");

            migrationBuilder.DropTable(
                name: "ReturnedProducts");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Persons");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
