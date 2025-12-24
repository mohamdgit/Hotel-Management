using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hotel_Management.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class editphotos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Size",
                table: "HotelPhotos");

            migrationBuilder.RenameColumn(
                name: "Url",
                table: "HotelPhotos",
                newName: "Name");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "HotelPhotos",
                newName: "Url");

            migrationBuilder.AddColumn<decimal>(
                name: "Size",
                table: "HotelPhotos",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
