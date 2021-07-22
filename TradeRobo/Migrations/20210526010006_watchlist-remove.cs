using Microsoft.EntityFrameworkCore.Migrations;

namespace TradeRobo.Migrations
{
    public partial class watchlistremove : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FavStocks");

            migrationBuilder.DropTable(
                name: "WatchlistSymbol");

            migrationBuilder.DropTable(
                name: "Watchlist");

            migrationBuilder.AddColumn<int>(
                name: "SortOrder",
                table: "Pie",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SortOrder",
                table: "Pie");

            migrationBuilder.CreateTable(
                name: "FavStocks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Symbol = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FavStocks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FavStocks_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Watchlist",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Watchlist", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Watchlist_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WatchlistSymbol",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Symbol = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WatchlistId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WatchlistSymbol", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WatchlistSymbol_Watchlist_WatchlistId",
                        column: x => x.WatchlistId,
                        principalTable: "Watchlist",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FavStocks_UserId",
                table: "FavStocks",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Watchlist_UserId",
                table: "Watchlist",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_WatchlistSymbol_WatchlistId",
                table: "WatchlistSymbol",
                column: "WatchlistId");
        }
    }
}
