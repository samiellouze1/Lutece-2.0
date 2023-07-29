using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StockService.Migrations
{
    /// <inheritdoc />
    public partial class fixeddiagram : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Stocks_StockId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_StockId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "StockId",
                table: "Orders");

            migrationBuilder.AddColumn<int>(
                name: "StockId",
                table: "OriginalOrders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_OriginalOrders_StockId",
                table: "OriginalOrders",
                column: "StockId");

            migrationBuilder.AddForeignKey(
                name: "FK_OriginalOrders_Stocks_StockId",
                table: "OriginalOrders",
                column: "StockId",
                principalTable: "Stocks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OriginalOrders_Stocks_StockId",
                table: "OriginalOrders");

            migrationBuilder.DropIndex(
                name: "IX_OriginalOrders_StockId",
                table: "OriginalOrders");

            migrationBuilder.DropColumn(
                name: "StockId",
                table: "OriginalOrders");

            migrationBuilder.AddColumn<int>(
                name: "StockId",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_StockId",
                table: "Orders",
                column: "StockId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Stocks_StockId",
                table: "Orders",
                column: "StockId",
                principalTable: "Stocks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
