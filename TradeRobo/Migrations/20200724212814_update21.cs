using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TradeRobo.Migrations
{
    public partial class update21 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_OrderGroup_OrderGroupId",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "BatchId",
                table: "Order");

            migrationBuilder.AlterColumn<int>(
                name: "OrderGroupId",
                table: "Order",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CoverTimeStamp",
                table: "Order",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_OrderGroup_OrderGroupId",
                table: "Order",
                column: "OrderGroupId",
                principalTable: "OrderGroup",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_OrderGroup_OrderGroupId",
                table: "Order");

            migrationBuilder.AlterColumn<int>(
                name: "OrderGroupId",
                table: "Order",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CoverTimeStamp",
                table: "Order",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BatchId",
                table: "Order",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Order_OrderGroup_OrderGroupId",
                table: "Order",
                column: "OrderGroupId",
                principalTable: "OrderGroup",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
