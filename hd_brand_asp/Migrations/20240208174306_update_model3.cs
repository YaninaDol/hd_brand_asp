using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace hd_brand_asp.Migrations
{
    /// <inheritdoc />
    public partial class update_model3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Material",
                table: "product");

           

            migrationBuilder.AddColumn<int>(
                name: "Materialid",
                table: "product",
                type: "integer",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Materialid",
                table: "product");

           
            migrationBuilder.AddColumn<string>(
                name: "Material",
                table: "product",
                type: "text",
                nullable: true);
        }
    }
}
