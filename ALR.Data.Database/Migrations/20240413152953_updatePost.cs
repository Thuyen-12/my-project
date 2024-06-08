using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ALR.Data.Database.Migrations
{
    /// <inheritdoc />
    public partial class updatePost : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "motelId",
                table: "Post",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: "335b580d-d059-49df-a99f-19886a126516");

            migrationBuilder.CreateIndex(
                name: "IX_Post_motelId",
                table: "Post",
                column: "motelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Post_Motel_motelId",
                table: "Post",
                column: "motelId",
                principalTable: "Motel",
                principalColumn: "motelID",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Post_Motel_motelId",
                table: "Post");

            migrationBuilder.DropIndex(
                name: "IX_Post_motelId",
                table: "Post");

            migrationBuilder.DropColumn(
                name: "motelId",
                table: "Post");
        }
    }
}
