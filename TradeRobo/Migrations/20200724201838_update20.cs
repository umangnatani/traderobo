using Microsoft.EntityFrameworkCore.Migrations;

namespace TradeRobo.Migrations
{
    public partial class update20 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trade_TradeBatch_OrderGroupId",
                table: "Trade");

            migrationBuilder.DropForeignKey(
                name: "FK_TradeBatch_User_UserId",
                table: "TradeBatch");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TradeBatch",
                table: "TradeBatch");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Trade",
                table: "Trade");

            migrationBuilder.DropColumn(
                name: "Result",
                table: "Trade");

            migrationBuilder.RenameTable(
                name: "TradeBatch",
                newName: "OrderGroup");

            migrationBuilder.RenameTable(
                name: "Trade",
                newName: "Order");

            migrationBuilder.RenameIndex(
                name: "IX_TradeBatch_UserId",
                table: "OrderGroup",
                newName: "IX_OrderGroup_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Trade_OrderGroupId",
                table: "Order",
                newName: "IX_Order_OrderGroupId");

            migrationBuilder.AddColumn<string>(
                name: "ExecMessage",
                table: "Order",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Success",
                table: "Order",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderGroup",
                table: "OrderGroup",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Order",
                table: "Order",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_OrderGroup_OrderGroupId",
                table: "Order",
                column: "OrderGroupId",
                principalTable: "OrderGroup",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderGroup_User_UserId",
                table: "OrderGroup",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_OrderGroup_OrderGroupId",
                table: "Order");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderGroup_User_UserId",
                table: "OrderGroup");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderGroup",
                table: "OrderGroup");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Order",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "ExecMessage",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "Success",
                table: "Order");

            migrationBuilder.RenameTable(
                name: "OrderGroup",
                newName: "TradeBatch");

            migrationBuilder.RenameTable(
                name: "Order",
                newName: "Trade");

            migrationBuilder.RenameIndex(
                name: "IX_OrderGroup_UserId",
                table: "TradeBatch",
                newName: "IX_TradeBatch_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Order_OrderGroupId",
                table: "Trade",
                newName: "IX_Trade_OrderGroupId");

            migrationBuilder.AddColumn<string>(
                name: "Result",
                table: "Trade",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TradeBatch",
                table: "TradeBatch",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Trade",
                table: "Trade",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Trade_TradeBatch_OrderGroupId",
                table: "Trade",
                column: "OrderGroupId",
                principalTable: "TradeBatch",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TradeBatch_User_UserId",
                table: "TradeBatch",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
