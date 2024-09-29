﻿// <auto-generated />
using Amazon.Core.DBContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Amazon.Core.Migrations
{
    [DbContext(typeof(AmazonDbContext))]
    [Migration("20240929100828_SeedingData")]
    partial class SeedingData
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Proxies:ChangeTracking", false)
                .HasAnnotation("Proxies:CheckEquality", false)
                .HasAnnotation("Proxies:LazyLoading", true)
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Amazon.Core.Entities.Brand", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Brands");

                    b.HasData(
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
                        });
                });

            modelBuilder.Entity("Amazon.Core.Entities.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ParentCategoryId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ParentCategoryId");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Phones",
                            ParentCategoryId = 1
                        },
                        new
                        {
                            Id = 2,
                            Name = "Laptops",
                            ParentCategoryId = 1
                        },
                        new
                        {
                            Id = 3,
                            Name = "Tablets",
                            ParentCategoryId = 1
                        },
                        new
                        {
                            Id = 4,
                            Name = "Air Conditioners",
                            ParentCategoryId = 2
                        },
                        new
                        {
                            Id = 5,
                            Name = "Washing Machines",
                            ParentCategoryId = 2
                        },
                        new
                        {
                            Id = 6,
                            Name = "Ovens",
                            ParentCategoryId = 2
                        });
                });

            modelBuilder.Entity("Amazon.Core.Entities.ParentCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ParentCategories");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Electronics"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Home & Kitchen"
                        });
                });

            modelBuilder.Entity("Amazon.Core.Entities.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BrandId")
                        .HasColumnType("int");

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PictureUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("money");

                    b.Property<int>("QuantityInStock")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BrandId");

                    b.HasIndex("CategoryId");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            BrandId = 2,
                            CategoryId = 1,
                            Description = "Latest model with A15 Bionic chip and triple-camera system.",
                            Name = "iPhone 13 Pro",
                            PictureUrl = "apple-iphone-13-pro-128gb-blue.webp",
                            Price = 999.99m,
                            QuantityInStock = 50
                        },
                        new
                        {
                            Id = 2,
                            BrandId = 1,
                            CategoryId = 1,
                            Description = "Flagship phone with 108MP camera and 8K video recording.",
                            Name = "Galaxy S21 Ultra",
                            PictureUrl = "eg-galaxy-s21-ultra-5g-g988-sm-g998bzkgmea-368371553.jpg",
                            Price = 1199.99m,
                            QuantityInStock = 30
                        },
                        new
                        {
                            Id = 3,
                            BrandId = 2,
                            CategoryId = 2,
                            Description = "Apple's latest MacBook Pro with M1 chip, 16-inch Retina display.",
                            Name = "MacBook Pro 16",
                            PictureUrl = "111901_mbp16-gray.png",
                            Price = 2499.99m,
                            QuantityInStock = 30
                        },
                        new
                        {
                            Id = 4,
                            BrandId = 3,
                            CategoryId = 2,
                            Description = "Business laptop with a 14-inch display, 11th Gen Intel Core i7, and long battery life.",
                            Name = "Lenovo ThinkPad X1 Carbon",
                            PictureUrl = "pw5jy11vn8u0jbi3rdu3aq1ij4bl15411237.jpg",
                            Price = 1799.99m,
                            QuantityInStock = 20
                        },
                        new
                        {
                            Id = 5,
                            BrandId = 2,
                            CategoryId = 3,
                            Description = "High-end tablet with M1 chip and 12.9-inch Liquid Retina XDR display.",
                            Name = "iPad Pro 12.9",
                            PictureUrl = "6adf3e75_d5edc742_iPadPro12-wifi-Silver.png",
                            Price = 1099.99m,
                            QuantityInStock = 40
                        },
                        new
                        {
                            Id = 6,
                            BrandId = 1,
                            CategoryId = 3,
                            Description = "Premium Android tablet with 12.4-inch AMOLED display and S Pen.",
                            Name = "Samsung Galaxy Tab S7+",
                            PictureUrl = "nz-galaxy-tab-s7-plus-wifi-t970-sm-t970nzkexnz-frontmysticblack-thumb-284684467.jpg",
                            Price = 849.99m,
                            QuantityInStock = 30
                        },
                        new
                        {
                            Id = 7,
                            BrandId = 4,
                            CategoryId = 4,
                            Description = "Energy-efficient air conditioner with dual inverter compressor and fast cooling.",
                            Name = "LG Dual Inverter",
                            PictureUrl = "lg-dualcool-inverter-compressor-artcool-15-hp-cooling-and-heating-uvnano-faster-cooling-energy-saving-s4-w12jarma.jpg",
                            Price = 699.99m,
                            QuantityInStock = 25
                        },
                        new
                        {
                            Id = 8,
                            BrandId = 1,
                            CategoryId = 4,
                            Description = "Wind-free cooling technology with energy-efficient inverter compressor.",
                            Name = "Samsung Wind-Free AC",
                            PictureUrl = "levant-ar9500t-ac-windfree-ar24cxfcabt-jo-536285193.jpg",
                            Price = 799.99m,
                            QuantityInStock = 20
                        },
                        new
                        {
                            Id = 9,
                            BrandId = 4,
                            CategoryId = 5,
                            Description = "High-efficiency front-load washer with TurboWash technology.",
                            Name = "LG Front Load Washer",
                            PictureUrl = "lg-washing-machine-front-load-15kg-6-motion-direct-drive-inverter-direct-drive-f0l9dyp2e.jpg",
                            Price = 799.99m,
                            QuantityInStock = 20
                        },
                        new
                        {
                            Id = 10,
                            BrandId = 1,
                            CategoryId = 5,
                            Description = "Top-load washer with active water jet and super speed wash.",
                            Name = "Samsung Top Load Washer",
                            PictureUrl = "samsung-washing-machine-top-loading-11-kg-gray-wa11dg5410bdas.jpg",
                            Price = 699.99m,
                            QuantityInStock = 15
                        },
                        new
                        {
                            Id = 11,
                            BrandId = 1,
                            CategoryId = 6,
                            Description = "Convection oven with smart dial controls and air fry technology.",
                            Name = "Samsung Convection Oven",
                            PictureUrl = "images.jpg",
                            Price = 599.99m,
                            QuantityInStock = 20
                        },
                        new
                        {
                            Id = 12,
                            BrandId = 4,
                            CategoryId = 6,
                            Description = "Smart oven with AI technology and precise temperature control.",
                            Name = "LG Smart Oven",
                            PictureUrl = "MH7636GIS-Microwave-Ovens-d-1.jpg",
                            Price = 799.99m,
                            QuantityInStock = 15
                        });
                });

            modelBuilder.Entity("Amazon.Core.Entities.ProductImages", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ImagePath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.ToTable("ProductImages");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            ImagePath = "13proBLUEviewNO.jpg",
                            ProductId = 1
                        },
                        new
                        {
                            Id = 2,
                            ImagePath = "5de0be127651483.614622c90bc5a.png",
                            ProductId = 1
                        },
                        new
                        {
                            Id = 3,
                            ImagePath = "iPhone13pro-box_2048x.jpg",
                            ProductId = 1
                        },
                        new
                        {
                            Id = 4,
                            ImagePath = "4-42-600x600.webp",
                            ProductId = 2
                        },
                        new
                        {
                            Id = 5,
                            ImagePath = "Samsung_Galaxy_S21_Ultra_5G_gallery_41.png",
                            ProductId = 2
                        },
                        new
                        {
                            Id = 6,
                            ImagePath = "EuBklEWXEAAWTDl.jpg",
                            ProductId = 2
                        },
                        new
                        {
                            Id = 7,
                            ImagePath = "APPLE-MacBook-Pro-2019-mit-Touch-Bar-Notebook-16-_-512-GB-SSD-Space-Grey-1.png",
                            ProductId = 3
                        },
                        new
                        {
                            Id = 8,
                            ImagePath = "c990d6e0-fba9-40a4-97d5-8e4808efc4df.png",
                            ProductId = 3
                        },
                        new
                        {
                            Id = 9,
                            ImagePath = "macspec.jpg",
                            ProductId = 3
                        },
                        new
                        {
                            Id = 10,
                            ImagePath = "03.01.17_Lenovo_X1_teaser.png",
                            ProductId = 4
                        },
                        new
                        {
                            Id = 11,
                            ImagePath = "pw5jy11vn8u0jbi3rdu3aq1ij4bl15411237.jpg",
                            ProductId = 4
                        },
                        new
                        {
                            Id = 12,
                            ImagePath = "lenovospec.jpg",
                            ProductId = 4
                        },
                        new
                        {
                            Id = 13,
                            ImagePath = "111841_ipad-pro-4gen-mainimage.png",
                            ProductId = 5
                        },
                        new
                        {
                            Id = 14,
                            ImagePath = "Apple-iPad-Pro-Magic-Keyboard-M2-hero-2up-221018.jpg.og.jpg",
                            ProductId = 5
                        },
                        new
                        {
                            Id = 15,
                            ImagePath = "iPad_Pro_Cellular_12-9_in_6th_generation_PDP_Image_Position-6_5G__WWEN_f2b4e2cd-5326-46f2-a161-575731395fbc.jpg",
                            ProductId = 5
                        },
                        new
                        {
                            Id = 16,
                            ImagePath = "nz-galaxy-tab-s7-t875-sm-t875nzkexnz-thumb-426768376.jpg",
                            ProductId = 6
                        },
                        new
                        {
                            Id = 17,
                            ImagePath = "ie-galaxy-tab-s7-plus-keyboard-cover-dt970-ef-dt970bbeggb-533878535.png",
                            ProductId = 6
                        },
                        new
                        {
                            Id = 18,
                            ImagePath = "hQbzJtjrDTRW6kXve8oJM3-1200-80.jpg",
                            ProductId = 6
                        },
                        new
                        {
                            Id = 19,
                            ImagePath = "Z-03.jpg",
                            ProductId = 7
                        },
                        new
                        {
                            Id = 20,
                            ImagePath = "80aa2726f731f5250fe11ea3221545b6.png",
                            ProductId = 7
                        },
                        new
                        {
                            Id = 21,
                            ImagePath = "my-wall-mount-f-ar1-0byeaawk-front-white-531730030.jpg",
                            ProductId = 8
                        },
                        new
                        {
                            Id = 22,
                            ImagePath = "sa-en-ac9000wind-free4way-376396-376396-ac024tx4pch-su-395920849.jpg",
                            ProductId = 8
                        },
                        new
                        {
                            Id = 23,
                            ImagePath = "E43ZSo8XMAc6agp.jpg",
                            ProductId = 8
                        },
                        new
                        {
                            Id = 24,
                            ImagePath = "lg-front-load-2011-kg-washer-dryer-6-motion-dd-motor-steam-turbowash-turbodry-f0l2crv2tc.jpg",
                            ProductId = 9
                        },
                        new
                        {
                            Id = 25,
                            ImagePath = "1dd902458e7e18b3b807c7bd313ef38f.jpg",
                            ProductId = 9
                        },
                        new
                        {
                            Id = 26,
                            ImagePath = "samsung-washing-machine-22kg-top-loading-digital-inverter-motor-black-stainless-wa22m8700gv.jpg",
                            ProductId = 10
                        },
                        new
                        {
                            Id = 27,
                            ImagePath = "15a77b97380033.Y3JvcCwxMTU0LDkwMywxMjQwLDMx.jpg",
                            ProductId = 10
                        },
                        new
                        {
                            Id = 28,
                            ImagePath = "digital-inverter2-img2.webp",
                            ProductId = 10
                        },
                        new
                        {
                            Id = 29,
                            ImagePath = "sg-mw8000r-mc35r8088lc-sp-lperspectiveblack-225860856.jpg",
                            ProductId = 11
                        },
                        new
                        {
                            Id = 30,
                            ImagePath = "sg-mw8000r-mc35r8088lc-sp-controlpanelfrontopenblack-225860858.webp",
                            ProductId = 11
                        },
                        new
                        {
                            Id = 31,
                            ImagePath = "MH7636GIS-Microwave-Ovens-d-4.jpg",
                            ProductId = 12
                        });
                });

            modelBuilder.Entity("Amazon.Core.Entities.Category", b =>
                {
                    b.HasOne("Amazon.Core.Entities.ParentCategory", "ParentCategory")
                        .WithMany("Categories")
                        .HasForeignKey("ParentCategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ParentCategory");
                });

            modelBuilder.Entity("Amazon.Core.Entities.Product", b =>
                {
                    b.HasOne("Amazon.Core.Entities.Brand", "Brand")
                        .WithMany()
                        .HasForeignKey("BrandId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Amazon.Core.Entities.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Brand");

                    b.Navigation("Category");
                });

            modelBuilder.Entity("Amazon.Core.Entities.ProductImages", b =>
                {
                    b.HasOne("Amazon.Core.Entities.Product", "Product")
                        .WithMany("Images")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Amazon.Core.Entities.ParentCategory", b =>
                {
                    b.Navigation("Categories");
                });

            modelBuilder.Entity("Amazon.Core.Entities.Product", b =>
                {
                    b.Navigation("Images");
                });
#pragma warning restore 612, 618
        }
    }
}
