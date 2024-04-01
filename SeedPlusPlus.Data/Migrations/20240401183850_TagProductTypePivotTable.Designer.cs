﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SeedPlusPlus.Data;

#nullable disable

namespace SeedPlusPlus.Data.Migrations
{
    [DbContext(typeof(SeedPlusPlusTestContext))]
    [Migration("20240401183850_TagProductTypePivotTable")]
    partial class TagProductTypePivotTable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.15");

            modelBuilder.Entity("ProductTypeTag", b =>
                {
                    b.Property<int>("ProductTypeId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("TagsId")
                        .HasColumnType("INTEGER");

                    b.HasKey("ProductTypeId", "TagsId");

                    b.HasIndex("TagsId");

                    b.ToTable("ProductTypeTag");
                });

            modelBuilder.Entity("SeedPlusPlus.Core.Products.Batch", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateOnly>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<DateOnly>("ExpiresAt")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Batch");
                });

            modelBuilder.Entity("SeedPlusPlus.Core.Products.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("CategoryId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Price")
                        .HasColumnType("TEXT");

                    b.Property<int>("TypeId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("TypeId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("SeedPlusPlus.Core.Products.ProductCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("Left")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int?>("ParentId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Right")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("ParentId");

                    b.ToTable("ProductCategories");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Left = 1,
                            Name = "Root",
                            Right = 18
                        },
                        new
                        {
                            Id = 2,
                            Left = 2,
                            Name = "Flowers",
                            ParentId = 1,
                            Right = 7
                        },
                        new
                        {
                            Id = 4,
                            Left = 3,
                            Name = "Annual flowers",
                            ParentId = 2,
                            Right = 4
                        },
                        new
                        {
                            Id = 5,
                            Left = 5,
                            Name = "Perennial flowers",
                            ParentId = 2,
                            Right = 6
                        },
                        new
                        {
                            Id = 3,
                            Left = 8,
                            Name = "Vegetables",
                            ParentId = 1,
                            Right = 17
                        },
                        new
                        {
                            Id = 6,
                            Left = 9,
                            Name = "Brassicas",
                            ParentId = 3,
                            Right = 10
                        },
                        new
                        {
                            Id = 7,
                            Left = 11,
                            Name = "Legumes",
                            ParentId = 3,
                            Right = 12
                        },
                        new
                        {
                            Id = 8,
                            Left = 13,
                            Name = "Leaf vegetables",
                            ParentId = 3,
                            Right = 14
                        },
                        new
                        {
                            Id = 9,
                            Left = 15,
                            Name = "Fruit vegetables",
                            ParentId = 3,
                            Right = 16
                        });
                });

            modelBuilder.Entity("SeedPlusPlus.Core.Products.ProductType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("ProductType");
                });

            modelBuilder.Entity("SeedPlusPlus.Core.Products.StockKeepingUnit", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("BatchId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("ProductId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Quantity")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("BatchId");

                    b.HasIndex("ProductId");

                    b.ToTable("StockKeepingUnit");
                });

            modelBuilder.Entity("SeedPlusPlus.Core.Products.Tag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Type")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Tag");
                });

            modelBuilder.Entity("ProductTypeTag", b =>
                {
                    b.HasOne("SeedPlusPlus.Core.Products.ProductType", null)
                        .WithMany()
                        .HasForeignKey("ProductTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SeedPlusPlus.Core.Products.Tag", null)
                        .WithMany()
                        .HasForeignKey("TagsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SeedPlusPlus.Core.Products.Product", b =>
                {
                    b.HasOne("SeedPlusPlus.Core.Products.ProductCategory", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SeedPlusPlus.Core.Products.ProductType", "Type")
                        .WithMany()
                        .HasForeignKey("TypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("Type");
                });

            modelBuilder.Entity("SeedPlusPlus.Core.Products.ProductCategory", b =>
                {
                    b.HasOne("SeedPlusPlus.Core.Products.ProductCategory", "Parent")
                        .WithMany("Children")
                        .HasForeignKey("ParentId");

                    b.Navigation("Parent");
                });

            modelBuilder.Entity("SeedPlusPlus.Core.Products.StockKeepingUnit", b =>
                {
                    b.HasOne("SeedPlusPlus.Core.Products.Batch", "Batch")
                        .WithMany()
                        .HasForeignKey("BatchId");

                    b.HasOne("SeedPlusPlus.Core.Products.Product", "Product")
                        .WithMany("Skus")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Batch");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("SeedPlusPlus.Core.Products.Product", b =>
                {
                    b.Navigation("Skus");
                });

            modelBuilder.Entity("SeedPlusPlus.Core.Products.ProductCategory", b =>
                {
                    b.Navigation("Children");
                });
#pragma warning restore 612, 618
        }
    }
}
