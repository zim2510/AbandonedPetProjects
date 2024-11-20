using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TalkOTC.Data.Migrations
{
    public partial class AddforeignkeycolumninUserRoom : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RoomIdentifier",
                table: "UserRooms");

            migrationBuilder.AddColumn<int>(
                name: "RoomId",
                table: "UserRooms",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_UserRooms_RoomId",
                table: "UserRooms",
                column: "RoomId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserRooms_Rooms_RoomId",
                table: "UserRooms",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserRooms_Rooms_RoomId",
                table: "UserRooms");

            migrationBuilder.DropIndex(
                name: "IX_UserRooms_RoomId",
                table: "UserRooms");

            migrationBuilder.DropColumn(
                name: "RoomId",
                table: "UserRooms");

            migrationBuilder.AddColumn<string>(
                name: "RoomIdentifier",
                table: "UserRooms",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
