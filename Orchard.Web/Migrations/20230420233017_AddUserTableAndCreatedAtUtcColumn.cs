using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Orchard.Web.Migrations
{
    /// <inheritdoc />
    public partial class AddUserTableAndCreatedAtUtcColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                schema: "orchard",
                table: "basket",
                newName: "name");

            migrationBuilder.AddColumn<DateTime>(
                name: "created_at_utc",
                schema: "orchard",
                table: "fruit",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "timezone('UTC', now())");

            migrationBuilder.AddColumn<DateTime>(
                name: "created_at_utc",
                schema: "orchard",
                table: "basket_fruit",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "timezone('UTC', now())");

            migrationBuilder.AlterColumn<string>(
                name: "name",
                schema: "orchard",
                table: "basket",
                type: "character varying(512)",
                maxLength: 512,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<DateTime>(
                name: "created_at_utc",
                schema: "orchard",
                table: "basket",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "timezone('UTC', now())");

            migrationBuilder.CreateTable(
                name: "user",
                schema: "orchard",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    email = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: false),
                    hashed_password = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: false),
                    first_name = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    last_name = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    birth_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    email_verification_guid = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    created_at_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "timezone('UTC', now())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "user",
                schema: "orchard");

            migrationBuilder.DropColumn(
                name: "created_at_utc",
                schema: "orchard",
                table: "fruit");

            migrationBuilder.DropColumn(
                name: "created_at_utc",
                schema: "orchard",
                table: "basket_fruit");

            migrationBuilder.DropColumn(
                name: "created_at_utc",
                schema: "orchard",
                table: "basket");

            migrationBuilder.RenameColumn(
                name: "name",
                schema: "orchard",
                table: "basket",
                newName: "Name");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "orchard",
                table: "basket",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(512)",
                oldMaxLength: 512);
        }
    }
}
