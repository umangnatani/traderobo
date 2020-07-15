using Microsoft.EntityFrameworkCore.Migrations;

namespace TradeRobo.Migrations
{
    public partial class update6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RHTocken",
                table: "User");

            migrationBuilder.AddColumn<string>(
                name: "RHNumber",
                table: "User",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RHToken",
                table: "User",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TDNumber",
                table: "User",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RHNumber",
                table: "User");

            migrationBuilder.DropColumn(
                name: "RHToken",
                table: "User");

            migrationBuilder.DropColumn(
                name: "TDNumber",
                table: "User");

            migrationBuilder.AddColumn<string>(
                name: "RHTocken",
                table: "User",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
