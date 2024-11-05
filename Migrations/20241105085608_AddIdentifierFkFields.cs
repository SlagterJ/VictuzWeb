using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VictuzWeb.Migrations
{
    /// <inheritdoc />
    public partial class AddIdentifierFkFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OwnerOfIdentifiers",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "[]");

            migrationBuilder.AddColumn<string>(
                name: "RegisteredForGatheringsIdentifiers",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "[]");

            migrationBuilder.AddColumn<string>(
                name: "SuggestionsIdentifiers",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "[]");

            migrationBuilder.AddColumn<string>(
                name: "RegisteredUsersIdentifiers",
                table: "Suggestions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UsersWithRoleIdentifiers",
                table: "Roles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "[]");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OwnerOfIdentifiers",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "RegisteredForGatheringsIdentifiers",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "SuggestionsIdentifiers",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "RegisteredUsersIdentifiers",
                table: "Suggestions");

            migrationBuilder.DropColumn(
                name: "UsersWithRoleIdentifiers",
                table: "Roles");
        }
    }
}
