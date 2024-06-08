using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ALR.Data.Database.Migrations
{
    /// <inheritdoc />
    public partial class updatepostLevel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "postLevel",
                table: "Post",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "postLevel",
                table: "Post");
        }
    }
}
