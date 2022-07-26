﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Super_Market_Management_System.Data;

#nullable disable

namespace Super_Market_Management_System.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0-preview.4.22229.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Super_Market_Management_System.Models.Brand", b =>
                {
                    b.Property<int>("BrandId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BrandId"), 1L, 1);

                    b.Property<string>("BrandName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HeadOffice")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("BrandId");

                    b.ToTable("brands");
                });

            modelBuilder.Entity("Super_Market_Management_System.Models.Cashier", b =>
                {
                    b.Property<int>("cashierid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("cashierid"), 1L, 1);

                    b.Property<DateTime>("created_at")
                        .HasColumnType("datetime2");

                    b.Property<string>("email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("cashierid");

                    b.ToTable("Cashier");
                });

            modelBuilder.Entity("Super_Market_Management_System.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("Super_Market_Management_System.Models.Order", b =>
                {
                    b.Property<int>("orderid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("orderid"), 1L, 1);

                    b.Property<int>("cashierid")
                        .HasColumnType("int");

                    b.Property<string>("customerAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("customerName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("customerPhone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("order_generated_at")
                        .HasColumnType("datetime2");

                    b.Property<int>("totalPrice")
                        .HasColumnType("int");

                    b.HasKey("orderid");

                    b.HasIndex("cashierid");

                    b.ToTable("orders");
                });

            modelBuilder.Entity("Super_Market_Management_System.Models.OrderProduct", b =>
                {
                    b.Property<int>("orderid")
                        .HasColumnType("int");

                    b.Property<int>("ProdId")
                        .HasColumnType("int");

                    b.Property<int>("quantity")
                        .HasColumnType("int");

                    b.HasKey("orderid", "ProdId");

                    b.HasIndex("ProdId");

                    b.ToTable("orderproduct");
                });

            modelBuilder.Entity("Super_Market_Management_System.Models.Product", b =>
                {
                    b.Property<int>("ProdId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProdId"), 1L, 1);

                    b.Property<int>("BrandId")
                        .HasColumnType("int");

                    b.Property<int>("Cat_Id")
                        .HasColumnType("int");

                    b.Property<string>("ProdName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ProdPrice")
                        .HasColumnType("int");

                    b.Property<int>("ProdQuantity")
                        .HasColumnType("int");

                    b.HasKey("ProdId");

                    b.HasIndex("BrandId")
                        .IsUnique();

                    b.HasIndex("Cat_Id");

                    b.ToTable("products");
                });

            modelBuilder.Entity("Super_Market_Management_System.Models.Supplier", b =>
                {
                    b.Property<int>("supplierid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("supplierid"), 1L, 1);

                    b.Property<DateTime>("created_at")
                        .HasColumnType("datetime2");

                    b.Property<string>("email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("supplierid");

                    b.ToTable("Supplier");
                });

            modelBuilder.Entity("Super_Market_Management_System.Models.Order", b =>
                {
                    b.HasOne("Super_Market_Management_System.Models.Cashier", "cashier")
                        .WithMany()
                        .HasForeignKey("cashierid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("cashier");
                });

            modelBuilder.Entity("Super_Market_Management_System.Models.OrderProduct", b =>
                {
                    b.HasOne("Super_Market_Management_System.Models.Product", "products")
                        .WithMany()
                        .HasForeignKey("ProdId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Super_Market_Management_System.Models.Order", "orders")
                        .WithMany()
                        .HasForeignKey("orderid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("orders");

                    b.Navigation("products");
                });

            modelBuilder.Entity("Super_Market_Management_System.Models.Product", b =>
                {
                    b.HasOne("Super_Market_Management_System.Models.Brand", "brands")
                        .WithOne("products")
                        .HasForeignKey("Super_Market_Management_System.Models.Product", "BrandId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Super_Market_Management_System.Models.Category", "categories")
                        .WithMany("products")
                        .HasForeignKey("Cat_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("brands");

                    b.Navigation("categories");
                });

            modelBuilder.Entity("Super_Market_Management_System.Models.Brand", b =>
                {
                    b.Navigation("products")
                        .IsRequired();
                });

            modelBuilder.Entity("Super_Market_Management_System.Models.Category", b =>
                {
                    b.Navigation("products");
                });
#pragma warning restore 612, 618
        }
    }
}
