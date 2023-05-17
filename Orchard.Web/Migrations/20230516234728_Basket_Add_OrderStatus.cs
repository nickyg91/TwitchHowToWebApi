using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Orchard.Web.Migrations
{
    /// <inheritdoc />
    public partial class Basket_Add_OrderStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte>(
                name: "order_status",
                schema: "orchard",
                table: "basket",
                type: "smallint",
                nullable: false,
                defaultValue: (byte)1);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "order_status",
                schema: "orchard",
                table: "basket");
        }
    }
}
