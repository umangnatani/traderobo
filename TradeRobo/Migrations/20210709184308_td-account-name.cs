using Microsoft.EntityFrameworkCore.Migrations;

namespace TradeRobo.Migrations
{
    public partial class tdaccountname : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccoutName",
                table: "TDAccount");

            migrationBuilder.AddColumn<string>(
                name: "AccountName",
                table: "TDAccount",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccountName",
                table: "TDAccount");

            migrationBuilder.AddColumn<string>(
                name: "AccoutName",
                table: "TDAccount",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
