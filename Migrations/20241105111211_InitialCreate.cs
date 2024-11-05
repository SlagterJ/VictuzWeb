using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

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
                    Identifier = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Identifier);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Identifier = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Firstname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BirthDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RoleIdentifier = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
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
                name: "Clubs",
                columns: table => new
                {
                    Identifier = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Accepted = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OwnerIdentifier = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clubs", x => x.Identifier);
                    table.ForeignKey(
                        name: "FK_Clubs_Users_OwnerIdentifier",
                        column: x => x.OwnerIdentifier,
                        principalTable: "Users",
                        principalColumn: "Identifier",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Suggestions",
                columns: table => new
                {
                    Identifier = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SuggestedByIdentifier = table.Column<long>(type: "bigint", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false),
                    MaxUsers = table.Column<long>(type: "bigint", nullable: true),
                    DeadlineDate = table.Column<DateOnly>(type: "date", nullable: true),
                    BeginDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EndDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
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
                    GatheringIdentifier = table.Column<long>(type: "bigint", nullable: false),
                    UsersIdentifier = table.Column<long>(type: "bigint", nullable: false)
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

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Identifier", "Name" },
                values: new object[,]
                {
                    { 1L, "User" },
                    { 2L, "Admin" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Identifier", "BirthDate", "Firstname", "PasswordHash", "RoleIdentifier", "Surname", "Username" },
                values: new object[,]
                {
                    { 1L, new DateOnly(2004, 11, 5), "Nicky", "password123", 1L, "Jaspers", "GigaChad" },
                    { 2L, new DateOnly(2004, 11, 5), "Miel", "password123456", 2L, "Noelanders", "DirtyDaddy" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Clubs_OwnerIdentifier",
                table: "Clubs",
                column: "OwnerIdentifier");

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
                name: "Clubs");

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
