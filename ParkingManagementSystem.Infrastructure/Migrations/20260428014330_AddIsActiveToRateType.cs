using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ParkingManagementSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddIsActiveToRateType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "RateTypes",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "RateTypes");
        }
    }
}
