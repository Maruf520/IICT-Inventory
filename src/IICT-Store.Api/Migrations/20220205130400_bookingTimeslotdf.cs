using Microsoft.EntityFrameworkCore.Migrations;

namespace IICT_Store.Api.Migrations
{
    public partial class bookingTimeslotdf : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookingTimeSlots_Bookings_BookingId1",
                table: "BookingTimeSlots");

            migrationBuilder.DropForeignKey(
                name: "FK_BookingTimeSlots_TimeSlots_TimeSlotId1",
                table: "BookingTimeSlots");

            migrationBuilder.DropIndex(
                name: "IX_BookingTimeSlots_BookingId1",
                table: "BookingTimeSlots");

            migrationBuilder.DropIndex(
                name: "IX_BookingTimeSlots_TimeSlotId1",
                table: "BookingTimeSlots");

            migrationBuilder.DropColumn(
                name: "BookingId1",
                table: "BookingTimeSlots");

            migrationBuilder.DropColumn(
                name: "TimeSlotId1",
                table: "BookingTimeSlots");

            migrationBuilder.AlterColumn<long>(
                name: "TimeSlotId",
                table: "BookingTimeSlots",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<long>(
                name: "BookingId",
                table: "BookingTimeSlots",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_BookingTimeSlots_BookingId",
                table: "BookingTimeSlots",
                column: "BookingId");

            migrationBuilder.CreateIndex(
                name: "IX_BookingTimeSlots_TimeSlotId",
                table: "BookingTimeSlots",
                column: "TimeSlotId");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookingTimeSlots_Bookings_BookingId",
                table: "BookingTimeSlots");

            migrationBuilder.DropForeignKey(
                name: "FK_BookingTimeSlots_TimeSlots_TimeSlotId",
                table: "BookingTimeSlots");

            migrationBuilder.DropIndex(
                name: "IX_BookingTimeSlots_BookingId",
                table: "BookingTimeSlots");

            migrationBuilder.DropIndex(
                name: "IX_BookingTimeSlots_TimeSlotId",
                table: "BookingTimeSlots");

            migrationBuilder.AlterColumn<int>(
                name: "TimeSlotId",
                table: "BookingTimeSlots",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "BookingId",
                table: "BookingTimeSlots",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddColumn<long>(
                name: "BookingId1",
                table: "BookingTimeSlots",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "TimeSlotId1",
                table: "BookingTimeSlots",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BookingTimeSlots_BookingId1",
                table: "BookingTimeSlots",
                column: "BookingId1");

            migrationBuilder.CreateIndex(
                name: "IX_BookingTimeSlots_TimeSlotId1",
                table: "BookingTimeSlots",
                column: "TimeSlotId1");

            migrationBuilder.AddForeignKey(
                name: "FK_BookingTimeSlots_Bookings_BookingId1",
                table: "BookingTimeSlots",
                column: "BookingId1",
                principalTable: "Bookings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BookingTimeSlots_TimeSlots_TimeSlotId1",
                table: "BookingTimeSlots",
                column: "TimeSlotId1",
                principalTable: "TimeSlots",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
