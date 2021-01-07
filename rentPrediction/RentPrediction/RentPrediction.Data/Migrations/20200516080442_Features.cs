using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RentPrediction.Data.Migrations
{
    public partial class Features : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AvailableDate",
                table: "Properties");

            migrationBuilder.AddColumn<string>(
                name: "AvailableTime",
                table: "Features",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BuildingSeniority",
                table: "Features",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BuildingType",
                table: "Features",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Endowment",
                table: "Features",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Finish",
                table: "Features",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AvailableTime",
                table: "Features");

            migrationBuilder.DropColumn(
                name: "BuildingSeniority",
                table: "Features");

            migrationBuilder.DropColumn(
                name: "BuildingType",
                table: "Features");

            migrationBuilder.DropColumn(
                name: "Endowment",
                table: "Features");

            migrationBuilder.DropColumn(
                name: "Finish",
                table: "Features");

            migrationBuilder.AddColumn<DateTime>(
                name: "AvailableDate",
                table: "Properties",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
