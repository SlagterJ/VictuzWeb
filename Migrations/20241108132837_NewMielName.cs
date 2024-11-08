using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VictuzWeb.Migrations
{
    /// <inheritdoc />
    public partial class NewMielName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Identifier",
                keyValue: 2L,
                column: "Username",
                value: "MielTheKing");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Identifier",
                keyValue: 2L,
                column: "Username",
                value: "DirtyDaddy");
        }
    }
}
