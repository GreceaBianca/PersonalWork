using Microsoft.EntityFrameworkCore.Migrations;

namespace RentPrediction.Data.Migrations
{
    public partial class favoritesfixes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Favorites_Users_UserId2",
                table: "Favorites");

            migrationBuilder.DropIndex(
                name: "IX_Favorites_UserId2",
                table: "Favorites");

            migrationBuilder.DropColumn(
                name: "UserId2",
                table: "Favorites");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId2",
                table: "Favorites",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Favorites_UserId2",
                table: "Favorites",
                column: "UserId2");

            migrationBuilder.AddForeignKey(
                name: "FK_Favorites_Users_UserId2",
                table: "Favorites",
                column: "UserId2",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
