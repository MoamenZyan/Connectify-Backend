using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Connectify.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChatType : Migration
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

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Chats",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "Chats");

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
