using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ordering.Server.Migrations
{
    /// <inheritdoc />
    public partial class databasewithProdctData2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 1,
                columns: new[] { "Price", "ProductName" },
                values: new object[] { 5.0, "socks" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 1,
                columns: new[] { "Price", "ProductName" },
                values: new object[] { 32.0, "dress" });
        }
    }
}
