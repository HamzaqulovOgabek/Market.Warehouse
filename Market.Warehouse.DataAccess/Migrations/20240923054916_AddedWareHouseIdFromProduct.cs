using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Market.Warehouse.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddedWareHouseIdFromProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            
            migrationBuilder.AddUniqueConstraint(
                name: "UK_Inventory_ProductId_WarehouseId",
                table: "Inventory",
                columns: new[] { "ProductId", "WarehouseId" });

            migrationBuilder.DropColumn(
                name: "WarehouseId",
                table: "Product");
        }


        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Drop the unique constraint
            migrationBuilder.DropUniqueConstraint(
                name: "UK_Inventory_ProductId_WarehouseId",
                table: "Inventory");

            // Re-add the WarehouseId column to the Product table
            migrationBuilder.AddColumn<int>(
                name: "WarehouseId",
                table: "Product",
                nullable: true);
        }
    }
}
