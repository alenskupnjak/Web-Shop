using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Movies.data.Migrations
{
    /// <inheritdoc />
    public partial class slika : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "ProductImage",
                columns: new[] { "Id", "FileName", "IsMainImage", "ProductId", "Title" },
                values: new object[] { 51, "/images/products/15/woman.jpg", true, 51, "Rambo 2" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ProductImage",
                keyColumn: "Id",
                keyValue: 51);
        }
    }
}
