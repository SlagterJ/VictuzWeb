using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VictuzWeb.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Identifier = table.Column<decimal>(type: "decimal(20,0)", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Identifier);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Identifier = table.Column<decimal>(type: "decimal(20,0)", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Firstname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BirthDate = table.Column<DateOnly>(type: "date", nullable: false),
                    RoleIdentifier = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Identifier);
                    table.ForeignKey(
                        name: "FK_Users_Roles_RoleIdentifier",
                        column: x => x.RoleIdentifier,
                        principalTable: "Roles",
                        principalColumn: "Identifier",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Suggestions",
                columns: table => new
                {
                    Identifier = table.Column<decimal>(type: "decimal(20,0)", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SuggestedByIdentifier = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false),
                    MaxUsers = table.Column<int>(type: "int", nullable: true),
                    DeadlineDate = table.Column<DateOnly>(type: "date", nullable: true),
                    BeginDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EndDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suggestions", x => x.Identifier);
                    table.ForeignKey(
                        name: "FK_Suggestions_Users_SuggestedByIdentifier",
                        column: x => x.SuggestedByIdentifier,
                        principalTable: "Users",
                        principalColumn: "Identifier",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserGathering",
                columns: table => new
                {
                    GatheringIdentifier = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    UsersIdentifier = table.Column<decimal>(type: "decimal(20,0)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserGathering", x => new { x.GatheringIdentifier, x.UsersIdentifier });
                    table.ForeignKey(
                        name: "FK_UserGathering_Suggestions_GatheringIdentifier",
                        column: x => x.GatheringIdentifier,
                        principalTable: "Suggestions",
                        principalColumn: "Identifier",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserGathering_Users_UsersIdentifier",
                        column: x => x.UsersIdentifier,
                        principalTable: "Users",
                        principalColumn: "Identifier",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Suggestions_SuggestedByIdentifier",
                table: "Suggestions",
                column: "SuggestedByIdentifier");

            migrationBuilder.CreateIndex(
                name: "IX_UserGathering_UsersIdentifier",
                table: "UserGathering",
                column: "UsersIdentifier");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleIdentifier",
                table: "Users",
                column: "RoleIdentifier");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserGathering");

            migrationBuilder.DropTable(
                name: "Suggestions");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
