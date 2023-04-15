using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Orchard.Web.Migrations
{
    /// <inheritdoc />
    public partial class FixForeignKeyNameBasketFruit_ConstraintName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_basket_fruit_basket_fk_basket_fruit",
                schema: "orchard",
                table: "basket_fruit");

            migrationBuilder.DropIndex(
                name: "IX_basket_fruit_fk_basket_fruit",
                schema: "orchard",
                table: "basket_fruit");

            migrationBuilder.DropColumn(
                name: "fk_basket_fruit",
                schema: "orchard",
                table: "basket_fruit");

            migrationBuilder.CreateIndex(
                name: "IX_basket_fruit_basket_id",
                schema: "orchard",
                table: "basket_fruit",
                column: "basket_id");

            migrationBuilder.AddForeignKey(
                name: "fk_basket_basket_fruit",
                schema: "orchard",
                table: "basket_fruit",
                column: "basket_id",
                principalSchema: "orchard",
                principalTable: "basket",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_basket_basket_fruit",
                schema: "orchard",
                table: "basket_fruit");

            migrationBuilder.DropIndex(
                name: "IX_basket_fruit_basket_id",
                schema: "orchard",
                table: "basket_fruit");

            migrationBuilder.AddColumn<int>(
                name: "fk_basket_fruit",
                schema: "orchard",
                table: "basket_fruit",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_basket_fruit_fk_basket_fruit",
                schema: "orchard",
                table: "basket_fruit",
                column: "fk_basket_fruit");

            migrationBuilder.AddForeignKey(
                name: "FK_basket_fruit_basket_fk_basket_fruit",
                schema: "orchard",
                table: "basket_fruit",
                column: "fk_basket_fruit",
                principalSchema: "orchard",
                principalTable: "basket",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
