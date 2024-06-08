using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ALR.Data.Database.Migrations
{
    /// <inheritdoc />



    public partial class billDes : Migration

    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(



                name: "billDescription",
                table: "BillHistory",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(



                name: "billDescription",
                table: "BillHistory");

        }
    }
}
