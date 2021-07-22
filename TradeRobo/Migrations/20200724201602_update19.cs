using Microsoft.EntityFrameworkCore.Migrations;

namespace TradeRobo.Migrations
{
    public partial class update19 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trade_TradeBatch_BatchId",
                table: "Trade");

            migrationBuilder.DropIndex(
                name: "IX_Trade_BatchId",
                table: "Trade");

            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                table: "TradeBatch",
                type: "decimal(16,4)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AddColumn<int>(
                name: "OrderGroupId",
                table: "Trade",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Trade_OrderGroupId",
                table: "Trade",
                column: "OrderGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Trade_TradeBatch_OrderGroupId",
                table: "Trade",
                column: "OrderGroupId",
                principalTable: "TradeBatch",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trade_TradeBatch_OrderGroupId",
                table: "Trade");

            migrationBuilder.DropIndex(
                name: "IX_Trade_OrderGroupId",
                table: "Trade");

            migrationBuilder.DropColumn(
                name: "OrderGroupId",
                table: "Trade");

            migrationBuilder.AlterColumn<double>(
                name: "Amount",
                table: "TradeBatch",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(16,4)");

            migrationBuilder.CreateIndex(
                name: "IX_Trade_BatchId",
                table: "Trade",
                column: "BatchId");

            migrationBuilder.AddForeignKey(
                name: "FK_Trade_TradeBatch_BatchId",
                table: "Trade",
                column: "BatchId",
                principalTable: "TradeBatch",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
