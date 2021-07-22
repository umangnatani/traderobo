using Microsoft.EntityFrameworkCore.Migrations;

namespace TradeRobo.Migrations
{
    public partial class update13 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "User",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "User",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TDRefreshToken",
                table: "User",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TDToken",
                table: "User",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "User");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "User");

            migrationBuilder.DropColumn(
                name: "TDRefreshToken",
                table: "User");

            migrationBuilder.DropColumn(
                name: "TDToken",
                table: "User");
        }
    }
}
