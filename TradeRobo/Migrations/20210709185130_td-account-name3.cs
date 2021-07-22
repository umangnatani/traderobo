using Microsoft.EntityFrameworkCore.Migrations;

namespace TradeRobo.Migrations
{
    public partial class tdaccountname3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TDAccount_User_UserId",
                table: "TDAccount");

            migrationBuilder.DropIndex(
                name: "IX_TDAccount_UserId",
                table: "TDAccount");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_TDAccount_UserId",
                table: "TDAccount",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_TDAccount_User_UserId",
                table: "TDAccount",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
