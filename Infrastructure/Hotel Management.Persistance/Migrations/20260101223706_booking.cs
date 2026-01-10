using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hotel_Management.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class booking : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Rooms_RoomId",
                table: "Books");

            migrationBuilder.DropIndex(
                name: "IX_Books_RoomId",
                table: "Books");

            migrationBuilder.AddColumn<DateTime>(
                name: "Createdat",
                table: "Books",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "BookRoom",
                columns: table => new
                {
                    RoomBookedId = table.Column<int>(type: "int", nullable: false),
                    RoomId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookRoom", x => new { x.RoomBookedId, x.RoomId });
                    table.ForeignKey(
                        name: "FK_BookRoom_Books_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookRoom_Rooms_RoomBookedId",
                        column: x => x.RoomBookedId,
                        principalTable: "Rooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookRoom_RoomId",
                table: "BookRoom",
                column: "RoomId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookRoom");

            migrationBuilder.DropColumn(
                name: "Createdat",
                table: "Books");

            migrationBuilder.CreateIndex(
                name: "IX_Books_RoomId",
                table: "Books",
                column: "RoomId");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Rooms_RoomId",
                table: "Books",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id");
        }
    }
}
