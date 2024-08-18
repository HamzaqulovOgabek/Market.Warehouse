using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Market.Warehouse.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class RemovedWareHouseTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_Warehouse_WarehouseId",
                table: "Product");

            migrationBuilder.DropTable(
                name: "Warehouse");


            migrationBuilder.DropColumn(
                name: "WarehouseId",
                table: "Product");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "WarehouseId",
                table: "Product",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Warehouse",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Warehouse", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Product_WarehouseId",
                table: "Product",
                column: "WarehouseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Warehouse_WarehouseId",
                table: "Product",
                column: "WarehouseId",
                principalTable: "Warehouse",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
