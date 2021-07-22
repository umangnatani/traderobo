using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TradeRobo.Migrations
{
    public partial class update18 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Strategy",
                table: "TradeBatch",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CoverTimeStamp",
                table: "Trade",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Strategy",
                table: "TradeBatch");

            migrationBuilder.DropColumn(
                name: "CoverTimeStamp",
                table: "Trade");
        }
    }
}
