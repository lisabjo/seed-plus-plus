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
    [Migration("20240213211848_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.15");

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
                            Right = 14
                        },
                        new
                        {
                            Id = 2,
                            Left = 2,
                            Name = "Electronics",
                            ParentId = 1,
                            Right = 11
                        },
                        new
                        {
                            Id = 4,
                            Left = 3,
                            Name = "Computers",
                            ParentId = 2,
                            Right = 8
                        },
                        new
                        {
                            Id = 6,
                            Left = 4,
                            Name = "Apple Computers",
                            ParentId = 4,
                            Right = 5
                        },
                        new
                        {
                            Id = 7,
                            Left = 6,
                            Name = "PCs",
                            ParentId = 4,
                            Right = 7
                        },
                        new
                        {
                            Id = 5,
                            Left = 9,
                            Name = "Smartphones",
                            ParentId = 2,
                            Right = 10
                        },
                        new
                        {
                            Id = 3,
                            Left = 12,
                            Name = "Books",
                            ParentId = 1,
                            Right = 13
                        });
                });

            modelBuilder.Entity("SeedPlusPlus.Core.Products.ProductCategory", b =>
                {
                    b.HasOne("SeedPlusPlus.Core.Products.ProductCategory", "Parent")
                        .WithMany("Children")
                        .HasForeignKey("ParentId");

                    b.Navigation("Parent");
                });

            modelBuilder.Entity("SeedPlusPlus.Core.Products.ProductCategory", b =>
                {
                    b.Navigation("Children");
                });
#pragma warning restore 612, 618
        }
    }
}