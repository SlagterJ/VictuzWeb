using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VictuzWeb.Migrations
{
    /// <inheritdoc />
    public partial class Addimg : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Suggestions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Clubs",
                type: "nvarchar(max)",
                nullable: true);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Suggestions");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "Clubs");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Identifier",
                keyValue: 1L,
                column: "BirthDate",
                value: new DateOnly(2004, 11, 7));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Identifier",
                keyValue: 2L,
                column: "BirthDate",
                value: new DateOnly(2004, 11, 7));
        }
    }
}
