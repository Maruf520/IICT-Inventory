using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IICT_Store.Api.Migrations
{
    public partial class dateadded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookingTimeSlots_Bookings_BookingId",
                table: "BookingTimeSlots");

            migrationBuilder.DropForeignKey(
                name: "FK_BookingTimeSlots_TimeSlots_TimeSlotId",
                table: "BookingTimeSlots");

            migrationBuilder.AlterColumn<long>(
                name: "TimeSlotId",
                table: "BookingTimeSlots",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "BookingId",
                table: "BookingTimeSlots",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "BookingTimeSlots",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddForeignKey(
                name: "FK_BookingTimeSlots_Bookings_BookingId",
                table: "BookingTimeSlots",
                column: "BookingId",
                principalTable: "Bookings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BookingTimeSlots_TimeSlots_TimeSlotId",
                table: "BookingTimeSlots",
                column: "TimeSlotId",
                principalTable: "TimeSlots",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookingTimeSlots_Bookings_BookingId",
                table: "BookingTimeSlots");

            migrationBuilder.DropForeignKey(
                name: "FK_BookingTimeSlots_TimeSlots_TimeSlotId",
                table: "BookingTimeSlots");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "BookingTimeSlots");

            migrationBuilder.AlterColumn<long>(
                name: "TimeSlotId",
                table: "BookingTimeSlots",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "BookingId",
                table: "BookingTimeSlots",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddForeignKey(
                name: "FK_BookingTimeSlots_Bookings_BookingId",
                table: "BookingTimeSlots",
                column: "BookingId",
                principalTable: "Bookings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BookingTimeSlots_TimeSlots_TimeSlotId",
                table: "BookingTimeSlots",
                column: "TimeSlotId",
                principalTable: "TimeSlots",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
