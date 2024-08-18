using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Market.Warehouse.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddColumnsToDiscount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "Discounts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Discounts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "CreatedUserId",
                table: "Discounts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "ExpirationDate",
                table: "Discounts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedAt",
                table: "Discounts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "ModifiedUserId",
                table: "Discounts",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Code",
                table: "Discounts");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Discounts");

            migrationBuilder.DropColumn(
                name: "CreatedUserId",
                table: "Discounts");

            migrationBuilder.DropColumn(
                name: "ExpirationDate",
                table: "Discounts");

            migrationBuilder.DropColumn(
                name: "ModifiedAt",
                table: "Discounts");

            migrationBuilder.DropColumn(
                name: "ModifiedUserId",
                table: "Discounts");
        }
    }
}
