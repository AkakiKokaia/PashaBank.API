using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PashaBank.Infrastructure.Migrations
{
    public partial class Bonuses_Added_To_Users_And_ProductSales : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "WasCalculated",
                table: "ProductSales",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<decimal>(
                name: "AccummulatedBonus",
                table: "AspNetUsers",
                type: "decimal(18,2)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WasCalculated",
                table: "ProductSales");

            migrationBuilder.DropColumn(
                name: "AccummulatedBonus",
                table: "AspNetUsers");
        }
    }
}
