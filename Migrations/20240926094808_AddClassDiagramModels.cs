using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VictuzWeb.Migrations
{
    /// <inheritdoc />
    public partial class AddClassDiagramModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Identifier = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                    Identifier = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Firstname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BirthDate = table.Column<DateOnly>(type: "date", nullable: false),
                    RoleIdentifier = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Suggestions",
                columns: table => new
                {
                    Identifier = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SuggestedByIdentifier = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GatheringUser",
                columns: table => new
                {
                    RegisteredForGatheringsIdentifier = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RegisteredUsersIdentifier = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GatheringUser", x => new { x.RegisteredForGatheringsIdentifier, x.RegisteredUsersIdentifier });
                    table.ForeignKey(
                        name: "FK_GatheringUser_Suggestions_RegisteredForGatheringsIdentifier",
                        column: x => x.RegisteredForGatheringsIdentifier,
                        principalTable: "Suggestions",
                        principalColumn: "Identifier",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GatheringUser_Users_RegisteredUsersIdentifier",
                        column: x => x.RegisteredUsersIdentifier,
                        principalTable: "Users",
                        principalColumn: "Identifier",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GatheringUser_RegisteredUsersIdentifier",
                table: "GatheringUser",
                column: "RegisteredUsersIdentifier");

            migrationBuilder.CreateIndex(
                name: "IX_Suggestions_SuggestedByIdentifier",
                table: "Suggestions",
                column: "SuggestedByIdentifier");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleIdentifier",
                table: "Users",
                column: "RoleIdentifier");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GatheringUser");

            migrationBuilder.DropTable(
                name: "Suggestions");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
