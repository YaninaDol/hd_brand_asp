using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace hd_brand_asp.Migrations
{
    /// <inheritdoc />
    public partial class m2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           

            migrationBuilder.CreateTable(
                name: "Productssizes",
                columns: table => new
                {
                    Productid = table.Column<int>(type: "integer", nullable: false),
                    Size = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Image = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Price = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Productssizes", x => x.Productid);
                    table.ForeignKey(
                        name: "FK_Productssizes_product_Productid",
                        column: x => x.Productid,
                        principalTable: "product",
                        principalColumn: "id");
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Productssizes");

            migrationBuilder.CreateTable(
                name: "productsize",
                columns: table => new
                {
                    productid = table.Column<int>(type: "integer", nullable: false),
                    sizeid = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("productsize_pkey", x => new { x.productid, x.sizeid });
                    table.ForeignKey(
                        name: "productsize_productid_fkey",
                        column: x => x.productid,
                        principalTable: "product",
                        principalColumn: "id");
                });
        }
    }
}
