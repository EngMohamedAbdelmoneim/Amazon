using System.Reflection;
using Amazon.Core.Entities;
using Amazon.Core.Entities.OrderAggregate;
using Microsoft.EntityFrameworkCore;

namespace Amazon.Core.DBContext
{
    public class AmazonDbContext : DbContext
    {
        public AmazonDbContext(DbContextOptions<AmazonDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            modelBuilder.Entity<ParentCategory>().HasData(
                  new {
                      Id = 1,
                      Name = "Electronics" },
                  new {
                      Id = 2,
                      Name = "Home & Kitchen" }
              //new ParentCategory { Id = 5, Name = "Toys & Games" },
              //new ParentCategory { Id = 6, Name = "Health & Personal Care" },
              //new ParentCategory { Id = 7, Name = "Sports & Outdoors" },
              //new ParentCategory { Id = 9, Name = "Beauty" },
              //new ParentCategory { Id = 10, Name = "Office Supplies" },
              //new ParentCategory { Id = 13, Name = "Jewelry" },
              //new ParentCategory { Id = 14, Name = "Shoes" },
              );

            modelBuilder.Entity<Category>().HasData(
                    new {
                        Id = 1,
                        Name = "Phones", ParentCategoryId = 1 },
                    new {
                        Id = 2,
                        Name = "Laptops", ParentCategoryId = 1 },
                    new {
                        Id = 3,
                        Name = "Tablets", ParentCategoryId = 1 },
                    new {
                        Id = 4,
                        Name = "Air Conditioners", ParentCategoryId = 2 },
                    new {
                        Id = 5,
                        Name = "Washing Machines", ParentCategoryId = 2 },
                    new {
                        Id = 6,
                        Name = "Ovens", ParentCategoryId = 2 }

                );
            modelBuilder.Entity<Brand>().HasData(
                    new 
                    { 
                        Id = 1,
                        Name = "Samsung"
                    },
                    new 
                    {
                        Id = 2,
                        Name = "Apple" 
                    },
                    new 
                    {
                        Id = 3,
                        Name = "Lenovo" 
                    },
                    new 
                    {
                        Id = 4,
                        Name = "LG"
                    }
                );

            modelBuilder.Entity<Product>().HasData(
                new
                {
                    Id = 1,
                    CategoryId = 1,
                    Name = "iPhone 13 Pro",
                    BrandId = 2,
                    Description = "Latest model with A15 Bionic chip and triple-camera system.",
                    Price = 999.99m,
                    QuantityInStock = 50,
                    PictureUrl = "apple-iphone-13-pro-128gb-blue.webp",
                    IsAccepted = true,
                    QuantitySold = 0,
					SellerEmail = "Admin@gmail.com",
					SellerName = "Amazon",
                },
                new
                {
                    Id = 2,
                    CategoryId = 1,
                    Name = "Galaxy S21 Ultra",
                    BrandId = 1,
                    Description = "Flagship phone with 108MP camera and 8K video recording.",
                    Price = 1199.99m,
                    QuantityInStock = 30,
                    PictureUrl = "eg-galaxy-s21-ultra-5g-g988-sm-g998bzkgmea-368371553.jpg",
                    IsAccepted = true,
                    QuantitySold = 0,
                    SellerEmail = "Admin@gmail.com",
                    SellerName = "Amazon",
                },
                new
                {
                    Id = 3,
                    CategoryId = 2,
                    Name = "MacBook Pro 16",
                    BrandId = 2,
                    Description = "Apple's latest MacBook Pro with M1 chip, 16-inch Retina display.",
                    Price = 2499.99m,
                    QuantityInStock = 30,
                    PictureUrl = "111901_mbp16-gray.png",
                    IsAccepted = true,
                    QuantitySold = 0,
                    SellerEmail = "Admin@gmail.com",
                    SellerName = "Amazon",
                },
                new
                {
                    Id = 4,
                    CategoryId = 2,
                    Name = "Lenovo ThinkPad X1 Carbon",
                    BrandId = 3,
                    Description = "Business laptop with a 14-inch display, 11th Gen Intel Core i7, and long battery life.",
                    Price = 1799.99m,
                    QuantityInStock = 20,
                    PictureUrl = "pw5jy11vn8u0jbi3rdu3aq1ij4bl15411237.jpg",
                    IsAccepted = true,
                    QuantitySold = 0,
                    SellerEmail = "Admin@gmail.com",
                    SellerName = "Amazon",
                },
                new
                {
                    Id = 5,
                    CategoryId = 3,
                    Name = "iPad Pro 12.9",
                    BrandId = 2,
                    Description = "High-end tablet with M1 chip and 12.9-inch Liquid Retina XDR display.",
                    Price = 1099.99m,
                    QuantityInStock = 40,
                    PictureUrl = "6adf3e75_d5edc742_iPadPro12-wifi-Silver.png",
                    IsAccepted = true,
                    QuantitySold = 0,
                    SellerEmail = "Admin@gmail.com",
                    SellerName = "Amazon",
                },
                new
                {
                    Id = 6,
                    CategoryId = 3,
                    Name = "Samsung Galaxy Tab S7+",
                    BrandId = 1,
                    Description = "Premium Android tablet with 12.4-inch AMOLED display and S Pen.",
                    Price = 849.99m,
                    QuantityInStock = 30,
                    PictureUrl = "nz-galaxy-tab-s7-plus-wifi-t970-sm-t970nzkexnz-frontmysticblack-thumb-284684467.jpg",
                    IsAccepted = true,
                    QuantitySold = 0,
                    SellerEmail = "Admin@gmail.com",
                    SellerName = "Amazon",
                },
                new
                {
                    Id = 7,
                    CategoryId = 4,
                    Name = "LG Dual Inverter",
                    BrandId = 4,
                    Description = "Energy-efficient air conditioner with dual inverter compressor and fast cooling.",
                    Price = 699.99m,
                    QuantityInStock = 25,
                    PictureUrl = "lg-dualcool-inverter-compressor-artcool-15-hp-cooling-and-heating-uvnano-faster-cooling-energy-saving-s4-w12jarma.jpg",
                    IsAccepted = true,
                    QuantitySold = 0,
                    SellerEmail = "Admin@gmail.com",
                    SellerName = "Amazon",
                },
                new
                {
                    Id = 8,
                    CategoryId = 4,
                    Name = "Samsung Wind-Free AC",
                    BrandId = 1,
                    Description = "Wind-free cooling technology with energy-efficient inverter compressor.",
                    Price = 799.99m,
                    QuantityInStock = 20,
                    PictureUrl = "levant-ar9500t-ac-windfree-ar24cxfcabt-jo-536285193.jpg",
                    IsAccepted = true,
                    QuantitySold = 0,
                    SellerEmail = "Admin@gmail.com",
                    SellerName = "Amazon",
                },
                new
                {
                    Id = 9,
                    CategoryId = 5,
                    Name = "LG Front Load Washer",
                    BrandId = 4,
                    Description = "High-efficiency front-load washer with TurboWash technology.",
                    Price = 799.99m,
                    QuantityInStock = 20,
                    PictureUrl = "lg-washing-machine-front-load-15kg-6-motion-direct-drive-inverter-direct-drive-f0l9dyp2e.jpg",
                    IsAccepted = true,
                    QuantitySold = 0,
                    SellerEmail = "Admin@gmail.com",
                    SellerName = "Amazon",
                },
                new
                {
                    Id = 10,
                    CategoryId = 5,
                    Name = "Samsung Top Load Washer",
                    BrandId = 1,
                    Description = "Top-load washer with active water jet and super speed wash.",
                    Price = 699.99m,
                    QuantityInStock = 15,
                    PictureUrl = "samsung-washing-machine-top-loading-11-kg-gray-wa11dg5410bdas.jpg",
                    IsAccepted = true,
                    QuantitySold = 0,
                    SellerEmail = "Admin@gmail.com",
                    SellerName = "Amazon",
                },
                new
                {
                    Id = 11,
                    CategoryId = 6,
                    Name = "Samsung Convection Oven",
                    BrandId = 1,
                    Description = "Convection oven with smart dial controls and air fry technology.",
                    Price = 599.99m,
                    QuantityInStock = 20,
                    PictureUrl = "images.jpg",
                    IsAccepted = true,
                    QuantitySold = 0,
                    SellerEmail = "Admin@gmail.com",
                    SellerName = "Amazon",
                },
                new
                {
                    Id = 12,
                    CategoryId = 6,
                    Name = "LG Smart Oven",
                    BrandId = 4,
                    Description = "Smart oven with AI technology and precise temperature control.",
                    Price = 799.99m,
                    QuantityInStock = 15,
                    PictureUrl = "MH7636GIS-Microwave-Ovens-d-1.jpg",
                    IsAccepted = true,
                    QuantitySold = 0,
                    SellerEmail = "Admin@gmail.com",
                    SellerName = "Amazon",
                }
                );

            modelBuilder.Entity<ProductImages>().HasData(
                new { Id = 1, ProductId = 1, ImagePath = "13proBLUEviewNO.jpg" },
                new { Id = 2, ProductId = 1, ImagePath = "5de0be127651483.614622c90bc5a.png" },
                new { Id = 3, ProductId = 1, ImagePath = "iPhone13pro-box_2048x.jpg" },
                new { Id = 4, ProductId = 2, ImagePath = "4-42-600x600.webp" },
                new { Id = 5, ProductId = 2, ImagePath = "Samsung_Galaxy_S21_Ultra_5G_gallery_41.png" },
                new { Id = 6, ProductId = 2, ImagePath = "EuBklEWXEAAWTDl.jpg" },
                new { Id = 7, ProductId = 3, ImagePath = "APPLE-MacBook-Pro-2019-mit-Touch-Bar-Notebook-16-_-512-GB-SSD-Space-Grey-1.png" },
                new { Id = 8, ProductId = 3, ImagePath = "c990d6e0-fba9-40a4-97d5-8e4808efc4df.png" },
                new { Id = 9, ProductId = 3, ImagePath = "macspec.jpg" },
                new { Id = 10, ProductId = 4, ImagePath = "03.01.17_Lenovo_X1_teaser.png" },
                new { Id = 11, ProductId = 4, ImagePath = "pw5jy11vn8u0jbi3rdu3aq1ij4bl15411237.jpg" },
                new { Id = 12, ProductId = 4, ImagePath = "lenovospec.jpg" },
                new { Id = 13, ProductId = 5, ImagePath = "111841_ipad-pro-4gen-mainimage.png" },
                new { Id = 14, ProductId = 5, ImagePath = "Apple-iPad-Pro-Magic-Keyboard-M2-hero-2up-221018.jpg.og.jpg" },
                new { Id = 15, ProductId = 5, ImagePath = "iPad_Pro_Cellular_12-9_in_6th_generation_PDP_Image_Position-6_5G__WWEN_f2b4e2cd-5326-46f2-a161-575731395fbc.jpg" },
                new { Id = 16, ProductId = 6, ImagePath = "nz-galaxy-tab-s7-t875-sm-t875nzkexnz-thumb-426768376.jpg" },
                new { Id = 17, ProductId = 6, ImagePath = "ie-galaxy-tab-s7-plus-keyboard-cover-dt970-ef-dt970bbeggb-533878535.png" },
                new { Id = 18, ProductId = 6, ImagePath = "hQbzJtjrDTRW6kXve8oJM3-1200-80.jpg" },
                new { Id = 19, ProductId = 7, ImagePath = "Z-03.jpg" },
                new { Id = 20, ProductId = 7, ImagePath = "80aa2726f731f5250fe11ea3221545b6.png" },
                new { Id = 21, ProductId = 8, ImagePath = "my-wall-mount-f-ar1-0byeaawk-front-white-531730030.jpg" },
                new { Id = 22, ProductId = 8, ImagePath = "sa-en-ac9000wind-free4way-376396-376396-ac024tx4pch-su-395920849.jpg" },
                new { Id = 23, ProductId = 8, ImagePath = "E43ZSo8XMAc6agp.jpg" },
                new { Id = 24, ProductId = 9, ImagePath = "lg-front-load-2011-kg-washer-dryer-6-motion-dd-motor-steam-turbowash-turbodry-f0l2crv2tc.jpg" },
                new { Id = 25, ProductId = 9, ImagePath = "1dd902458e7e18b3b807c7bd313ef38f.jpg" },
                new { Id = 26, ProductId = 10, ImagePath = "samsung-washing-machine-22kg-top-loading-digital-inverter-motor-black-stainless-wa22m8700gv.jpg" },
                new { Id = 27, ProductId = 10, ImagePath = "15a77b97380033.Y3JvcCwxMTU0LDkwMywxMjQwLDMx.jpg" },
                new { Id = 28, ProductId = 10, ImagePath = "digital-inverter2-img2.webp" },
                new { Id = 29, ProductId = 11, ImagePath = "sg-mw8000r-mc35r8088lc-sp-lperspectiveblack-225860856.jpg" },
                new { Id = 30, ProductId = 11, ImagePath = "sg-mw8000r-mc35r8088lc-sp-controlpanelfrontopenblack-225860858.webp" },
                new { Id = 31, ProductId = 12, ImagePath = "MH7636GIS-Microwave-Ovens-d-4.jpg" }
                
                
                );
        }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ParentCategory> ParentCategories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImages> ProductImages { get; set; }

		public DbSet<Order> Orders { get; set; }
		public DbSet<OrderItem> OrderItems { get; set; }
		public DbSet<DeliveryMethod> DeliveryMethods { get; set; }
		public DbSet<PaymentMethod> PaymentMethods { get; set; }
		public DbSet<Review> Reviews { get; set; }

	}
}
