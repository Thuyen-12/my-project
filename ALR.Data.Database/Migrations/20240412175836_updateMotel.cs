using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ALR.Data.Database.Migrations
{
    /// <inheritdoc />
    public partial class updateMotel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "motelAddress",
                table: "Motel");

            migrationBuilder.AddColumn<Guid>(
                name: "AddressId",
                table: "Motel",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "MotelAddresses",
                columns: table => new
                {
                    AddressId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MoreDetails = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Commune = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    District = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MotelAddresses", x => x.AddressId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Motel_AddressId",
                table: "Motel",
                column: "AddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_Motel_MotelAddTest_AddressId",
                table: "Motel",
                column: "AddressId",
                principalTable: "MotelAddresses",
                principalColumn: "AddressId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Motel_MotelAddresses_AddressId",
                table: "Motel");

            migrationBuilder.DropTable(
                name: "MotelAddresses");

            migrationBuilder.DropIndex(
                name: "IX_Motel_AddressId",
                table: "Motel");

            migrationBuilder.DropColumn(
                name: "AddressId",
                table: "Motel");

            migrationBuilder.AddColumn<string>(
                name: "motelAddress",
                table: "Motel",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
