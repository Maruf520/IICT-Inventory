using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IICT_Store.Api.Migrations
{
    public partial class bookingTimeslot : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TimeSlots_Bookings_BookingId",
                table: "TimeSlots");

            migrationBuilder.DropIndex(
                name: "IX_TimeSlots_BookingId",
                table: "TimeSlots");

            migrationBuilder.DropColumn(
                name: "BookingId",
                table: "TimeSlots");

            migrationBuilder.CreateTable(
                name: "BookingTimeSlots",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookingId = table.Column<int>(type: "int", nullable: false),
                    BookingId1 = table.Column<long>(type: "bigint", nullable: true),
                    TimeSlotId = table.Column<int>(type: "int", nullable: false),
                    TimeSlotId1 = table.Column<long>(type: "bigint", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookingTimeSlots", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BookingTimeSlots_Bookings_BookingId1",
                        column: x => x.BookingId1,
                        principalTable: "Bookings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BookingTimeSlots_TimeSlots_TimeSlotId1",
                        column: x => x.TimeSlotId1,
                        principalTable: "TimeSlots",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookingTimeSlots_BookingId1",
                table: "BookingTimeSlots",
                column: "BookingId1");

            migrationBuilder.CreateIndex(
                name: "IX_BookingTimeSlots_TimeSlotId1",
                table: "BookingTimeSlots",
                column: "TimeSlotId1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookingTimeSlots");

            migrationBuilder.AddColumn<long>(
                name: "BookingId",
                table: "TimeSlots",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TimeSlots_BookingId",
                table: "TimeSlots",
                column: "BookingId");

            migrationBuilder.AddForeignKey(
                name: "FK_TimeSlots_Bookings_BookingId",
                table: "TimeSlots",
                column: "BookingId",
                principalTable: "Bookings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
