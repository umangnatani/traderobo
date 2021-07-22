using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TradeRobo.Migrations
{
    public partial class update14 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TradeBatch",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(nullable: false),
                    PieId = table.Column<int>(nullable: true),
                    Amount = table.Column<double>(nullable: false),
                    Broker = table.Column<string>(nullable: true),
                    StartTime = table.Column<DateTime>(nullable: false),
                    EndTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TradeBatch", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TradeBatch_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Trade",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BatchId = table.Column<int>(nullable: false),
                    Symbol = table.Column<string>(nullable: true),
                    Quantity = table.Column<double>(nullable: false),
                    Amount = table.Column<double>(nullable: false),
                    Price = table.Column<double>(nullable: false),
                    TimeStamp = table.Column<DateTime>(nullable: false),
                    Result = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trade", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Trade_TradeBatch_BatchId",
                        column: x => x.BatchId,
                        principalTable: "TradeBatch",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Trade_BatchId",
                table: "Trade",
                column: "BatchId");

            migrationBuilder.CreateIndex(
                name: "IX_TradeBatch_UserId",
                table: "TradeBatch",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Trade");

            migrationBuilder.DropTable(
                name: "TradeBatch");
        }
    }
}
