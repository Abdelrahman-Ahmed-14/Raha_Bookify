using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookifyHotelSystem.Migrations
{
    /// <inheritdoc />
    public partial class RenameEntDateToEndDate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EntDate",
                table: "Bookings",
                newName: "EndDate");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EndDate",
                table: "Bookings",
                newName: "EntDate");
        }
    }
}
