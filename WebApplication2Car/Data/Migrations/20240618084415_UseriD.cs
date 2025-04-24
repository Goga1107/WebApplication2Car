using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication2Car.Data.Migrations
{
    /// <inheritdoc />
    public partial class UseriD : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "cars",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "cars");
        }
    }
}
