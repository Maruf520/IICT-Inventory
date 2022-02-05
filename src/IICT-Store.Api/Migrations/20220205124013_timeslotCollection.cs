using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IICT_Store.Api.Migrations
{
    public partial class timeslotCollection : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndTime",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "StartTime",
                table: "Bookings");

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<TimeSpan>(
                name: "EndTime",
                table: "Bookings",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<TimeSpan>(
                name: "StartTime",
                table: "Bookings",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));
        }
    }
}
