using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Market.Warehouse.DataAccess.Migrations
{
    public partial class RenameCategoriesAndProductImages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            // Rename Categories table to Category
            migrationBuilder.RenameTable(
                name: "Categories",
                newName: "Category");

            

            // Add ParentId column to the Category table
            migrationBuilder.AddColumn<int>(
                name: "ParentId",
                table: "Category",
                type: "int",
                nullable: true);

            

            // Create Favourite table
            migrationBuilder.CreateTable(
                name: "Favourite",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Favourite", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Favourite_CartItem_Id",
                        column: x => x.Id,
                        principalTable: "CartItem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            // Add new foreign key constraints
            migrationBuilder.AddForeignKey(
                name: "FK_Category_Category_ParentId",
                table: "Category",
                column: "ParentId",
                principalTable: "Category",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Category_CategoryId",
                table: "Product",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Drop new foreign key constraints and the Favourite table
            migrationBuilder.DropForeignKey(
                name: "FK_Category_Category_ParentId",
                table: "Category");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_Category_CategoryId",
                table: "Product");

            migrationBuilder.DropTable(
                name: "Favourite");

            // Drop ParentId column from the Category table
            migrationBuilder.DropColumn(
                name: "ParentId",
                table: "Category");

            // Drop primary key for the Category table
            migrationBuilder.DropPrimaryKey(
                name: "PK_Category",
                table: "Category");

            // Rename Category table back to Categories
            migrationBuilder.RenameTable(
                name: "Category",
                newName: "Categories");

            // Rename index back
            migrationBuilder.RenameIndex(
                name: "IX_Category_ParentId",
                table: "Categories",
                newName: "IX_Categories_ParentId");

            // Add primary key for the Categories table
            migrationBuilder.AddPrimaryKey(
                name: "PK_Categories",
                table: "Categories",
                column: "Id");

            // Add old foreign key constraints back
            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Categories_ParentId",
                table: "Categories",
                column: "ParentId",
                principalTable: "Categories",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Categories_CategoryId",
                table: "Product",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id");
        }
    }
}
