using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace hd_brand_asp.Migrations
{
    /// <inheritdoc />
    public partial class M5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Productssizes",
                table: "Productssizes");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Productssizes",
                table: "Productssizes",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Productssizes_Productid",
                table: "Productssizes",
                column: "Productid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Productssizes",
                table: "Productssizes");

            migrationBuilder.DropIndex(
                name: "IX_Productssizes_Productid",
                table: "Productssizes");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Productssizes",
                table: "Productssizes",
                column: "Productid");
        }
    }
}
