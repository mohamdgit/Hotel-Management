using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hotel_Management.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class kk : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookRoom");

            migrationBuilder.DropColumn(
                name: "RoomId",
                table: "Books");

            migrationBuilder.CreateTable(
                name: "BookRooms",
                columns: table => new
                {
                    BookingsId = table.Column<int>(type: "int", nullable: false),
                    RoomBookedId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookRooms", x => new { x.BookingsId, x.RoomBookedId });
                    table.ForeignKey(
                        name: "FK_BookRooms_Books_BookingsId",
                        column: x => x.BookingsId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookRooms_Rooms_RoomBookedId",
                        column: x => x.RoomBookedId,
                        principalTable: "Rooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookRooms_RoomBookedId",
                table: "BookRooms",
                column: "RoomBookedId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookRooms");

            migrationBuilder.AddColumn<int>(
                name: "RoomId",
                table: "Books",
                type: "int",
                nullable: false,
                defaultValue: 0);

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
    }
}
