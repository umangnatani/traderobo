using Microsoft.EntityFrameworkCore.Migrations;

namespace TradeRobo.Migrations
{
    public partial class ma2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "ma8",
                table: "PieDetail",
                type: "decimal(10,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,4)");

            migrationBuilder.AlterColumn<decimal>(
                name: "ma50",
                table: "PieDetail",
                type: "decimal(10,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,4)");

            migrationBuilder.AlterColumn<decimal>(
                name: "ma5",
                table: "PieDetail",
                type: "decimal(10,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,4)");

            migrationBuilder.AlterColumn<decimal>(
                name: "ma21",
                table: "PieDetail",
                type: "decimal(10,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,4)");

            migrationBuilder.AlterColumn<decimal>(
                name: "ma200",
                table: "PieDetail",
                type: "decimal(10,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,4)");

            migrationBuilder.AlterColumn<decimal>(
                name: "ma13",
                table: "PieDetail",
                type: "decimal(10,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,4)");

            migrationBuilder.AlterColumn<decimal>(
                name: "ma10",
                table: "PieDetail",
                type: "decimal(10,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,4)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Weight",
                table: "PieDetail",
                type: "decimal(10,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,4)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "ma8",
                table: "PieDetail",
                type: "decimal(10,4)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "ma50",
                table: "PieDetail",
                type: "decimal(10,4)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "ma5",
                table: "PieDetail",
                type: "decimal(10,4)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "ma21",
                table: "PieDetail",
                type: "decimal(10,4)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "ma200",
                table: "PieDetail",
                type: "decimal(10,4)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "ma13",
                table: "PieDetail",
                type: "decimal(10,4)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "ma10",
                table: "PieDetail",
                type: "decimal(10,4)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Weight",
                table: "PieDetail",
                type: "decimal(10,4)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)");
        }
    }
}
