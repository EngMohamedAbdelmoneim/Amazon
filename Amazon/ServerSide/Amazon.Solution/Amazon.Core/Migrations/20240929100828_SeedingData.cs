using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Amazon.Core.Migrations
{
    /// <inheritdoc />
    public partial class SeedingData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Brands",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brands", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ParentCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParentCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ParentCategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Categories_ParentCategories_ParentCategoryId",
                        column: x => x.ParentCategoryId,
                        principalTable: "ParentCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<decimal>(type: "money", nullable: false),
                    PictureUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QuantityInStock = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    BrandId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Brands_BrandId",
                        column: x => x.BrandId,
                        principalTable: "Brands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductImages_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Brands",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Samsung" },
                    { 2, "Apple" },
                    { 3, "Lenovo" },
                    { 4, "LG" }
                });

            migrationBuilder.InsertData(
                table: "ParentCategories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Electronics" },
                    { 2, "Home & Kitchen" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name", "ParentCategoryId" },
                values: new object[,]
                {
                    { 1, "Phones", 1 },
                    { 2, "Laptops", 1 },
                    { 3, "Tablets", 1 },
                    { 4, "Air Conditioners", 2 },
                    { 5, "Washing Machines", 2 },
                    { 6, "Ovens", 2 }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "BrandId", "CategoryId", "Description", "Name", "PictureUrl", "Price", "QuantityInStock" },
                values: new object[,]
                {
                    { 1, 2, 1, "Latest model with A15 Bionic chip and triple-camera system.", "iPhone 13 Pro", "apple-iphone-13-pro-128gb-blue.webp", 999.99m, 50 },
                    { 2, 1, 1, "Flagship phone with 108MP camera and 8K video recording.", "Galaxy S21 Ultra", "eg-galaxy-s21-ultra-5g-g988-sm-g998bzkgmea-368371553.jpg", 1199.99m, 30 },
                    { 3, 2, 2, "Apple's latest MacBook Pro with M1 chip, 16-inch Retina display.", "MacBook Pro 16", "111901_mbp16-gray.png", 2499.99m, 30 },
                    { 4, 3, 2, "Business laptop with a 14-inch display, 11th Gen Intel Core i7, and long battery life.", "Lenovo ThinkPad X1 Carbon", "pw5jy11vn8u0jbi3rdu3aq1ij4bl15411237.jpg", 1799.99m, 20 },
                    { 5, 2, 3, "High-end tablet with M1 chip and 12.9-inch Liquid Retina XDR display.", "iPad Pro 12.9", "6adf3e75_d5edc742_iPadPro12-wifi-Silver.png", 1099.99m, 40 },
                    { 6, 1, 3, "Premium Android tablet with 12.4-inch AMOLED display and S Pen.", "Samsung Galaxy Tab S7+", "nz-galaxy-tab-s7-plus-wifi-t970-sm-t970nzkexnz-frontmysticblack-thumb-284684467.jpg", 849.99m, 30 },
                    { 7, 4, 4, "Energy-efficient air conditioner with dual inverter compressor and fast cooling.", "LG Dual Inverter", "lg-dualcool-inverter-compressor-artcool-15-hp-cooling-and-heating-uvnano-faster-cooling-energy-saving-s4-w12jarma.jpg", 699.99m, 25 },
                    { 8, 1, 4, "Wind-free cooling technology with energy-efficient inverter compressor.", "Samsung Wind-Free AC", "levant-ar9500t-ac-windfree-ar24cxfcabt-jo-536285193.jpg", 799.99m, 20 },
                    { 9, 4, 5, "High-efficiency front-load washer with TurboWash technology.", "LG Front Load Washer", "lg-washing-machine-front-load-15kg-6-motion-direct-drive-inverter-direct-drive-f0l9dyp2e.jpg", 799.99m, 20 },
                    { 10, 1, 5, "Top-load washer with active water jet and super speed wash.", "Samsung Top Load Washer", "samsung-washing-machine-top-loading-11-kg-gray-wa11dg5410bdas.jpg", 699.99m, 15 },
                    { 11, 1, 6, "Convection oven with smart dial controls and air fry technology.", "Samsung Convection Oven", "images.jpg", 599.99m, 20 },
                    { 12, 4, 6, "Smart oven with AI technology and precise temperature control.", "LG Smart Oven", "MH7636GIS-Microwave-Ovens-d-1.jpg", 799.99m, 15 }
                });

            migrationBuilder.InsertData(
                table: "ProductImages",
                columns: new[] { "Id", "ImagePath", "ProductId" },
                values: new object[,]
                {
                    { 1, "13proBLUEviewNO.jpg", 1 },
                    { 2, "5de0be127651483.614622c90bc5a.png", 1 },
                    { 3, "iPhone13pro-box_2048x.jpg", 1 },
                    { 4, "4-42-600x600.webp", 2 },
                    { 5, "Samsung_Galaxy_S21_Ultra_5G_gallery_41.png", 2 },
                    { 6, "EuBklEWXEAAWTDl.jpg", 2 },
                    { 7, "APPLE-MacBook-Pro-2019-mit-Touch-Bar-Notebook-16-_-512-GB-SSD-Space-Grey-1.png", 3 },
                    { 8, "c990d6e0-fba9-40a4-97d5-8e4808efc4df.png", 3 },
                    { 9, "macspec.jpg", 3 },
                    { 10, "03.01.17_Lenovo_X1_teaser.png", 4 },
                    { 11, "pw5jy11vn8u0jbi3rdu3aq1ij4bl15411237.jpg", 4 },
                    { 12, "lenovospec.jpg", 4 },
                    { 13, "111841_ipad-pro-4gen-mainimage.png", 5 },
                    { 14, "Apple-iPad-Pro-Magic-Keyboard-M2-hero-2up-221018.jpg.og.jpg", 5 },
                    { 15, "iPad_Pro_Cellular_12-9_in_6th_generation_PDP_Image_Position-6_5G__WWEN_f2b4e2cd-5326-46f2-a161-575731395fbc.jpg", 5 },
                    { 16, "nz-galaxy-tab-s7-t875-sm-t875nzkexnz-thumb-426768376.jpg", 6 },
                    { 17, "ie-galaxy-tab-s7-plus-keyboard-cover-dt970-ef-dt970bbeggb-533878535.png", 6 },
                    { 18, "hQbzJtjrDTRW6kXve8oJM3-1200-80.jpg", 6 },
                    { 19, "Z-03.jpg", 7 },
                    { 20, "80aa2726f731f5250fe11ea3221545b6.png", 7 },
                    { 21, "my-wall-mount-f-ar1-0byeaawk-front-white-531730030.jpg", 8 },
                    { 22, "sa-en-ac9000wind-free4way-376396-376396-ac024tx4pch-su-395920849.jpg", 8 },
                    { 23, "E43ZSo8XMAc6agp.jpg", 8 },
                    { 24, "lg-front-load-2011-kg-washer-dryer-6-motion-dd-motor-steam-turbowash-turbodry-f0l2crv2tc.jpg", 9 },
                    { 25, "1dd902458e7e18b3b807c7bd313ef38f.jpg", 9 },
                    { 26, "samsung-washing-machine-22kg-top-loading-digital-inverter-motor-black-stainless-wa22m8700gv.jpg", 10 },
                    { 27, "15a77b97380033.Y3JvcCwxMTU0LDkwMywxMjQwLDMx.jpg", 10 },
                    { 28, "digital-inverter2-img2.webp", 10 },
                    { 29, "sg-mw8000r-mc35r8088lc-sp-lperspectiveblack-225860856.jpg", 11 },
                    { 30, "sg-mw8000r-mc35r8088lc-sp-controlpanelfrontopenblack-225860858.webp", 11 },
                    { 31, "MH7636GIS-Microwave-Ovens-d-4.jpg", 12 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Categories_ParentCategoryId",
                table: "Categories",
                column: "ParentCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductImages_ProductId",
                table: "ProductImages",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_BrandId",
                table: "Products",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductImages");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Brands");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "ParentCategories");
        }
    }
}
