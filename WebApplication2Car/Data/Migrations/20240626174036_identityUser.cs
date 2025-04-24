using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication2Car.Data.Migrations
{
    /// <inheritdoc />
    public partial class identityUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "cars",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_cars_UserId",
                table: "cars",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_cars_Users_UserId",
                table: "cars",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_cars_Users_UserId",
                table: "cars");

            migrationBuilder.DropIndex(
                name: "IX_cars_UserId",
                table: "cars");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "cars",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
