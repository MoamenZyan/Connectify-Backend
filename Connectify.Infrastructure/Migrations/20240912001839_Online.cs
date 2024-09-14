using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Connectify.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Online : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsOnline",
                table: "Users",
                type: "BIT",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedAt",
                table: "FriendRequests",
                type: "VARCHAR(48)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsOnline",
                table: "Users");

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
