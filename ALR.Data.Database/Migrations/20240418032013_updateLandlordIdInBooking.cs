using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ALR.Data.Database.Migrations
{
    /// <inheritdoc />
    public partial class updateLandlordIdInBooking : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_BookingSchedule_landlordId",
                table: "BookingSchedule",
                column: "landlordId");

            migrationBuilder.AddForeignKey(
                name: "FK_BookingSchedule_UserInfo_landlordId",
                table: "BookingSchedule",
                column: "landlordId",
                principalTable: "UserInfo",
                principalColumn: "UserEntityID",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookingSchedule_UserInfo_landlordId",
                table: "BookingSchedule");

            migrationBuilder.DropIndex(
                name: "IX_BookingSchedule_landlordId",
                table: "BookingSchedule");
        }
    }
}
