using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VictuzWeb.Migrations
{
    /// <inheritdoc />
    public partial class IsMember : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsMember",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsMemberOnly",
                table: "Suggestions",
                type: "bit",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Identifier",
                keyValue: 1L,
                columns: new[] { "BirthDate", "IsMember" },
                values: new object[] { new DateOnly(2004, 11, 9), true });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Identifier",
                keyValue: 2L,
                columns: new[] { "BirthDate", "IsMember" },
                values: new object[] { new DateOnly(2004, 11, 9), true });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsMember",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "IsMemberOnly",
                table: "Suggestions");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Identifier",
                keyValue: 1L,
                column: "BirthDate",
                value: new DateOnly(2004, 11, 8));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Identifier",
                keyValue: 2L,
                column: "BirthDate",
                value: new DateOnly(2004, 11, 8));
        }
    }
}
