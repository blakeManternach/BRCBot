using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BRCBotApi.Migrations
{
    /// <inheritdoc />
    public partial class ChatHistoryText : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ChatMessageText",
                table: "ChatMessageHistories",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChatMessageText",
                table: "ChatMessageHistories");
        }
    }
}
