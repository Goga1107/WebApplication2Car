using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication2Car.Data.Migrations
{
    /// <inheritdoc />
    public partial class radio : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsForRent",
                table: "cars",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsForRent",
                table: "cars");
        }
    }
}
