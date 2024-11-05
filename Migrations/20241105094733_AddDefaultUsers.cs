using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace VictuzWeb.Migrations
{
    /// <inheritdoc />
    public partial class AddDefaultUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Identifier", "Name", "UsersWithRoleIdentifiers" },
                values: new object[,]
                {
                    { 1m, "User", "[1]" },
                    { 2m, "Admin", "[2]" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Identifier", "BirthDate", "Firstname", "OwnerOfIdentifiers", "PasswordHash", "RegisteredForGatheringsIdentifiers", "RoleIdentifier", "SuggestionsIdentifiers", "Surname", "Username" },
                values: new object[,]
                {
                    { 1m, new DateOnly(2004, 11, 5), "Nicky", "[]", "password123", "[]", 1m, "[]", "Jaspers", "GigaChad" },
                    { 2m, new DateOnly(2004, 11, 5), "Miel", "[]", "password123456", "[]", 2m, "[]", "Noelanders", "DirtyDaddy" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Identifier",
                keyValue: 1m);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Identifier",
                keyValue: 2m);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Identifier",
                keyValue: 1m);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Identifier",
                keyValue: 2m);
        }
    }
}
