using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ALR.Data.Database.Migrations
{
    /// <inheritdoc />
    public partial class updateMotelv1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Motel_UserInfo_landlordId",
                table: "Motel");

            migrationBuilder.RenameColumn(
                name: "landlordId",
                table: "Motel",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Motel_landlordId",
                table: "Motel",
                newName: "IX_Motel_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Motel_UserInfo_UserId",
                table: "Motel",
                column: "UserId",
                principalTable: "UserInfo",
                principalColumn: "UserEntityID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Motel_UserInfo_UserId",
                table: "Motel");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Motel",
                newName: "landlordId");

            migrationBuilder.RenameIndex(
                name: "IX_Motel_UserId",
                table: "Motel",
                newName: "IX_Motel_landlordId");

            migrationBuilder.AddForeignKey(
                name: "FK_Motel_UserInfo_landlordId",
                table: "Motel",
                column: "landlordId",
                principalTable: "UserInfo",
                principalColumn: "UserEntityID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
