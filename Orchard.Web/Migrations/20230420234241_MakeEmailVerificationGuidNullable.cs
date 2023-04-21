using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Orchard.Web.Migrations
{
    /// <inheritdoc />
    public partial class MakeEmailVerificationGuidNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "email_verification_guid",
                schema: "orchard",
                table: "user",
                type: "uuid",
                nullable: true,
                defaultValueSql: "gen_random_uuid()",
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldDefaultValueSql: "gen_random_uuid()");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "email_verification_guid",
                schema: "orchard",
                table: "user",
                type: "uuid",
                nullable: false,
                defaultValueSql: "gen_random_uuid()",
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true,
                oldDefaultValueSql: "gen_random_uuid()");
        }
    }
}
