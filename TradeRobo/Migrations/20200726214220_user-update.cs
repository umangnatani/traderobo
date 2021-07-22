using Microsoft.EntityFrameworkCore.Migrations;

namespace TradeRobo.Migrations
{
    public partial class userupdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "TDRefreshToken",
                table: "User");

            migrationBuilder.DropColumn(
                name: "TDToken",
                table: "User");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RHNumber",
                table: "User",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RHToken",
                table: "User",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TDNumber",
                table: "User",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TDRefreshToken",
                table: "User",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TDToken",
                table: "User",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
