using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GymManagementSystemDAL.Migrations
{
    /// <inheritdoc />
    public partial class BookConfigUpdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Bookings",
                table: "Bookings");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Bookings",
                table: "Bookings",
                columns: new[] { "SessionId", "MemberId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Bookings",
                table: "Bookings");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Bookings",
                table: "Bookings",
                columns: new[] { "SessionId", "MemberId", "BookingDate" });
        }
    }
}
