using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Orchard.Web.Migrations
{
    /// <inheritdoc />
    public partial class RestructureTablesForUserBaskets : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "fruit_shape",
                schema: "orchard",
                table: "fruit");

            migrationBuilder.DropColumn(
                name: "fruit_type",
                schema: "orchard",
                table: "fruit");

            migrationBuilder.DropColumn(
                name: "name",
                schema: "orchard",
                table: "basket");

            migrationBuilder.AddColumn<string>(
                name: "country_of_origin",
                schema: "orchard",
                table: "fruit",
                type: "character varying(128)",
                maxLength: 128,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "name",
                schema: "orchard",
                table: "fruit",
                type: "character varying(512)",
                maxLength: 512,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "sku_number",
                schema: "orchard",
                table: "fruit",
                type: "character varying(512)",
                maxLength: 512,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "user_id",
                schema: "orchard",
                table: "basket",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_basket_user_id",
                schema: "orchard",
                table: "basket",
                column: "user_id");

            migrationBuilder.AddForeignKey(
                name: "fk_basket_user",
                schema: "orchard",
                table: "basket",
                column: "user_id",
                principalSchema: "orchard",
                principalTable: "user",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_basket_user",
                schema: "orchard",
                table: "basket");

            migrationBuilder.DropIndex(
                name: "IX_basket_user_id",
                schema: "orchard",
                table: "basket");

            migrationBuilder.DropColumn(
                name: "country_of_origin",
                schema: "orchard",
                table: "fruit");

            migrationBuilder.DropColumn(
                name: "name",
                schema: "orchard",
                table: "fruit");

            migrationBuilder.DropColumn(
                name: "sku_number",
                schema: "orchard",
                table: "fruit");

            migrationBuilder.DropColumn(
                name: "user_id",
                schema: "orchard",
                table: "basket");

            migrationBuilder.AddColumn<byte>(
                name: "fruit_shape",
                schema: "orchard",
                table: "fruit",
                type: "smallint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<byte>(
                name: "fruit_type",
                schema: "orchard",
                table: "fruit",
                type: "smallint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<string>(
                name: "name",
                schema: "orchard",
                table: "basket",
                type: "character varying(512)",
                maxLength: 512,
                nullable: false,
                defaultValue: "");
        }
    }
}
