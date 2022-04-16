using Microsoft.EntityFrameworkCore.Migrations;

namespace TradeRobo.Migrations
{
    public partial class ma4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SortOrder",
                table: "Menu",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SortOrder",
                table: "Menu");
        }
    }
}
