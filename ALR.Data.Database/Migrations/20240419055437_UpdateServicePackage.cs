using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ALR.Data.Database.Migrations
{
    /// <inheritdoc />
    public partial class UpdateServicePackage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "availableSlot",
                table: "ServicePackage");

            migrationBuilder.AddColumn<DateTime>(
                name: "endDate",
                table: "ServicePackage",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "endDate",
                table: "ServicePackage");

            migrationBuilder.AddColumn<int>(
                name: "availableSlot",
                table: "ServicePackage",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
