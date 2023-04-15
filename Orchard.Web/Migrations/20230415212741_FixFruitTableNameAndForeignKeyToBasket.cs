using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Orchard.Web.Migrations
{
    /// <inheritdoc />
    public partial class FixFruitTableNameAndForeignKeyToBasket : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_basket_fruit_fruits_fruit_id",
                schema: "orchard",
                table: "basket_fruit");

            migrationBuilder.RenameTable(
                name: "fruits",
                schema: "orchard",
                newName: "fruit",
                newSchema: "orchard");

            migrationBuilder.AddForeignKey(
                name: "FK_basket_fruit_fruit_fruit_id",
                schema: "orchard",
                table: "basket_fruit",
                column: "fruit_id",
                principalSchema: "orchard",
                principalTable: "fruit",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_basket_fruit_fruit_fruit_id",
                schema: "orchard",
                table: "basket_fruit");

            migrationBuilder.RenameTable(
                name: "fruit",
                schema: "orchard",
                newName: "fruits",
                newSchema: "orchard");

            migrationBuilder.AddForeignKey(
                name: "FK_basket_fruit_fruits_fruit_id",
                schema: "orchard",
                table: "basket_fruit",
                column: "fruit_id",
                principalSchema: "orchard",
                principalTable: "fruits",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
