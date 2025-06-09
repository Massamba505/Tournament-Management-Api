using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tournament.Management.API.Migrations
{
    /// <inheritdoc />
    public partial class editsMade : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Members",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "Organiser");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Members",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "Manager");
        }
    }
}
