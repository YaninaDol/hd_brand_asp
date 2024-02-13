using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace hd_brand_asp.Migrations
{
    /// <inheritdoc />
    public partial class update_product2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "product",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Video",
                table: "product",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "product");

            migrationBuilder.DropColumn(
                name: "Video",
                table: "product");
        }
    }
}
