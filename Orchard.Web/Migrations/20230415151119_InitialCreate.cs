using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Orchard.Web.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "orchard");

            migrationBuilder.CreateTable(
                name: "basket",
                schema: "orchard",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_basket", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "fruits",
                schema: "orchard",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    fruit_type = table.Column<byte>(type: "smallint", nullable: false),
                    fruit_shape = table.Column<byte>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_fruit_id", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "basket_fruit",
                schema: "orchard",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    fruit_id = table.Column<int>(type: "integer", nullable: false),
                    basket_id = table.Column<int>(type: "integer", nullable: false),
                    amount = table.Column<int>(type: "integer", nullable: false),
                    fk_basket_fruit = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_basket_fruit_id", x => x.id);
                    table.ForeignKey(
                        name: "FK_basket_fruit_basket_fk_basket_fruit",
                        column: x => x.fk_basket_fruit,
                        principalSchema: "orchard",
                        principalTable: "basket",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_basket_fruit_fruits_fruit_id",
                        column: x => x.fruit_id,
                        principalSchema: "orchard",
                        principalTable: "fruits",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_basket_fruit_fk_basket_fruit",
                schema: "orchard",
                table: "basket_fruit",
                column: "fk_basket_fruit");

            migrationBuilder.CreateIndex(
                name: "IX_basket_fruit_fruit_id",
                schema: "orchard",
                table: "basket_fruit",
                column: "fruit_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "basket_fruit",
                schema: "orchard");

            migrationBuilder.DropTable(
                name: "basket",
                schema: "orchard");

            migrationBuilder.DropTable(
                name: "fruits",
                schema: "orchard");
        }
    }
}
