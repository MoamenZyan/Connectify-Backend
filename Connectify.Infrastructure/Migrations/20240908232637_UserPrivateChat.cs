using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Connectify.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UserPrivateChat : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "CreatedAt",
                table: "FriendRequests",
                type: "VARCHAR(48)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR");

            migrationBuilder.CreateTable(
                name: "UserPrivateChats",
                columns: table => new
                {
                    User1Id = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    User2Id = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    ChatId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPrivateChats", x => new { x.User1Id, x.User2Id });
                    table.ForeignKey(
                        name: "FK_UserPrivateChats_Chats_ChatId",
                        column: x => x.ChatId,
                        principalTable: "Chats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserPrivateChats_Users_User1Id",
                        column: x => x.User1Id,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserPrivateChats_Users_User2Id",
                        column: x => x.User2Id,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserPrivateChats_ChatId",
                table: "UserPrivateChats",
                column: "ChatId");

            migrationBuilder.CreateIndex(
                name: "IX_UserPrivateChats_User2Id",
                table: "UserPrivateChats",
                column: "User2Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserPrivateChats");

            migrationBuilder.AlterColumn<string>(
                name: "CreatedAt",
                table: "FriendRequests",
                type: "VARCHAR",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR(48)");
        }
    }
}
