using Microsoft.EntityFrameworkCore.Migrations;

namespace TradeRobo.Migrations
{
    public partial class update3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PieDetails_Pies_PieId",
                table: "PieDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Pies",
                table: "Pies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PieDetails",
                table: "PieDetails");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "User");

            migrationBuilder.RenameTable(
                name: "Pies",
                newName: "Pie");

            migrationBuilder.RenameTable(
                name: "PieDetails",
                newName: "PieDetail");

            migrationBuilder.RenameIndex(
                name: "IX_PieDetails_PieId",
                table: "PieDetail",
                newName: "IX_PieDetail_PieId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_User",
                table: "User",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Pie",
                table: "Pie",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PieDetail",
                table: "PieDetail",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PieDetail_Pie_PieId",
                table: "PieDetail",
                column: "PieId",
                principalTable: "Pie",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PieDetail_Pie_PieId",
                table: "PieDetail");

            migrationBuilder.DropPrimaryKey(
                name: "PK_User",
                table: "User");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PieDetail",
                table: "PieDetail");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Pie",
                table: "Pie");

            migrationBuilder.RenameTable(
                name: "User",
                newName: "Users");

            migrationBuilder.RenameTable(
                name: "PieDetail",
                newName: "PieDetails");

            migrationBuilder.RenameTable(
                name: "Pie",
                newName: "Pies");

            migrationBuilder.RenameIndex(
                name: "IX_PieDetail_PieId",
                table: "PieDetails",
                newName: "IX_PieDetails_PieId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PieDetails",
                table: "PieDetails",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Pies",
                table: "Pies",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PieDetails_Pies_PieId",
                table: "PieDetails",
                column: "PieId",
                principalTable: "Pies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
