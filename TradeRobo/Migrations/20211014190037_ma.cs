using Microsoft.EntityFrameworkCore.Migrations;

namespace TradeRobo.Migrations
{
    public partial class ma : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "ma10",
                table: "PieDetail",
                type: "decimal(10,4)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "ma13",
                table: "PieDetail",
                type: "decimal(10,4)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "ma200",
                table: "PieDetail",
                type: "decimal(10,4)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "ma21",
                table: "PieDetail",
                type: "decimal(10,4)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "ma5",
                table: "PieDetail",
                type: "decimal(10,4)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "ma50",
                table: "PieDetail",
                type: "decimal(10,4)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "ma8",
                table: "PieDetail",
                type: "decimal(10,4)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ma10",
                table: "PieDetail");

            migrationBuilder.DropColumn(
                name: "ma13",
                table: "PieDetail");

            migrationBuilder.DropColumn(
                name: "ma200",
                table: "PieDetail");

            migrationBuilder.DropColumn(
                name: "ma21",
                table: "PieDetail");

            migrationBuilder.DropColumn(
                name: "ma5",
                table: "PieDetail");

            migrationBuilder.DropColumn(
                name: "ma50",
                table: "PieDetail");

            migrationBuilder.DropColumn(
                name: "ma8",
                table: "PieDetail");
        }
    }
}
