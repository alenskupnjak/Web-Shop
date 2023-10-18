using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Movies.data.Migrations
{
    /// <inheritdoc />
    public partial class init02 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "Id", "Active", "Description", "Price", "Quantity", "Title" },
                values: new object[,]
                {
                    { 50, true, "dddd", 10m, 10m, "Rambo" },
                    { 51, true, "Drugi nesto", 16m, 177m, "Rambo 2" },
                    { 52, true, "Tracu rambo nesto", 41m, 333m, "Rambo 3" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 50);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 51);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 52);
        }
    }
}
