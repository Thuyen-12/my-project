using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ALR.Data.Database.Migrations
{
    /// <inheritdoc />
    public partial class updateServicePack : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "endDate",
                table: "ServicePackage");

            migrationBuilder.RenameColumn(
                name: "startDate",
                table: "ServicePackage",
                newName: "CreatedDate");

            migrationBuilder.AddColumn<int>(
                name: "AvailableSlot",
                table: "ServicePackage",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AvailableSlot",
                table: "ServicePackage");

            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                table: "ServicePackage",
                newName: "startDate");

            migrationBuilder.AddColumn<DateTime>(
                name: "endDate",
                table: "ServicePackage",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
