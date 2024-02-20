using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace hd_brand_asp.Migrations
{
    /// <inheritdoc />
    public partial class updatemodel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Artikel",
                table: "product",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Image2",
                table: "product",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "SalePrice",
                table: "product",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "isDiscount",
                table: "product",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "isNew",
                table: "product",
                type: "boolean",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Artikel",
                table: "product");

            migrationBuilder.DropColumn(
                name: "Image2",
                table: "product");

            migrationBuilder.DropColumn(
                name: "SalePrice",
                table: "product");

            migrationBuilder.DropColumn(
                name: "isDiscount",
                table: "product");

            migrationBuilder.DropColumn(
                name: "isNew",
                table: "product");
        }
    }
}
