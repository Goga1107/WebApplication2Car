using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication2Car.Data.Migrations
{
    /// <inheritdoc />
    public partial class removedmodel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Rents_CarId",
                table: "Rents",
                column: "CarId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rents_cars_CarId",
                table: "Rents",
                column: "CarId",
                principalTable: "cars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rents_cars_CarId",
                table: "Rents");

            migrationBuilder.DropIndex(
                name: "IX_Rents_CarId",
                table: "Rents");
        }
    }
}
