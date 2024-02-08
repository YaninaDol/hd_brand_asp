using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace hd_brand_asp.Migrations
{
    /// <inheritdoc />
    public partial class update_model : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "brand",
                table: "product",
                newName: "shoetype");

            migrationBuilder.AddColumn<string>(
                name: "Material",
                table: "product",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Material",
                table: "product");

            migrationBuilder.RenameColumn(
                name: "shoetype",
                table: "product",
                newName: "brand");
        }
    }
}
