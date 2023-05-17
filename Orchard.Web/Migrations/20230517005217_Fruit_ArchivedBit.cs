using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Orchard.Web.Migrations
{
    /// <inheritdoc />
    public partial class Fruit_ArchivedBit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "is_archived",
                schema: "orchard",
                table: "fruit",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "is_archived",
                schema: "orchard",
                table: "fruit");
        }
    }
}
