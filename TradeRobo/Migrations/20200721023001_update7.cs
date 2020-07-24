using Microsoft.EntityFrameworkCore.Migrations;

namespace TradeRobo.Migrations
{
    public partial class update7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Desc",
                table: "Pie",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "FavStocks",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Pie_UserId",
                table: "Pie",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_FavStocks_UserId",
                table: "FavStocks",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_FavStocks_User_UserId",
                table: "FavStocks",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Pie_User_UserId",
                table: "Pie",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FavStocks_User_UserId",
                table: "FavStocks");

            migrationBuilder.DropForeignKey(
                name: "FK_Pie_User_UserId",
                table: "Pie");

            migrationBuilder.DropIndex(
                name: "IX_Pie_UserId",
                table: "Pie");

            migrationBuilder.DropIndex(
                name: "IX_FavStocks_UserId",
                table: "FavStocks");

            migrationBuilder.DropColumn(
                name: "Desc",
                table: "Pie");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "FavStocks");
        }
    }
}
