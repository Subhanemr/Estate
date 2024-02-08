using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Estate.Persistance.Contexts.Migrations
{
    public partial class Mig3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OrderDayOrMoth",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrderDayOrMoth",
                table: "Products");
        }
    }
}
