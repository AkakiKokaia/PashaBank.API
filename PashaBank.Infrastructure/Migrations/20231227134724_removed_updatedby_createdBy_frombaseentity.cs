using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PashaBank.Infrastructure.Migrations
{
    public partial class removed_updatedby_createdBy_frombaseentity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "ProductSales");

            migrationBuilder.DropColumn(
                name: "UpdatedById",
                table: "ProductSales");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "UpdatedById",
                table: "Products");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CreatedById",
                table: "ProductSales",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedById",
                table: "ProductSales",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedById",
                table: "Products",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedById",
                table: "Products",
                type: "uniqueidentifier",
                nullable: true);
        }
    }
}
