using Microsoft.EntityFrameworkCore.Migrations;

namespace RentPrediction.Data.Migrations
{
    public partial class PredictedPrice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PredictedPrice",
                table: "Properties",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PredictedPrice",
                table: "Properties");
        }
    }
}
