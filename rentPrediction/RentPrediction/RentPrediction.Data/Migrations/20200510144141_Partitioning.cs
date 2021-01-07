using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RentPrediction.Data.Migrations
{
    public partial class Partitioning : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Partitioning",
                table: "Properties");

            migrationBuilder.AddColumn<bool>(
                name: "IsForSale",
                table: "Features",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "PartitioningId",
                table: "Features",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Partitionings",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsArchived = table.Column<bool>(nullable: false),
                    ArchivedDate = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Partitionings", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Features_PartitioningId",
                table: "Features",
                column: "PartitioningId");

            migrationBuilder.AddForeignKey(
                name: "FK_Features_Partitionings_PartitioningId",
                table: "Features",
                column: "PartitioningId",
                principalTable: "Partitionings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Features_Partitionings_PartitioningId",
                table: "Features");

            migrationBuilder.DropTable(
                name: "Partitionings");

            migrationBuilder.DropIndex(
                name: "IX_Features_PartitioningId",
                table: "Features");

            migrationBuilder.DropColumn(
                name: "IsForSale",
                table: "Features");

            migrationBuilder.DropColumn(
                name: "PartitioningId",
                table: "Features");

            migrationBuilder.AddColumn<string>(
                name: "Partitioning",
                table: "Properties",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
