using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PashaBank.Infrastructure.Migrations
{
    public partial class Updated_ProductSalesEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "SellPrice",
                table: "ProductSales",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SellPrice",
                table: "ProductSales");
        }
    }
}
