﻿// <auto-generated />
using System;
using FrizzApp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FrizzApp.Data.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20220512033233_init")]
    partial class init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.16");

            modelBuilder.Entity("FrizzApp.Data.Entities.Category", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("TEXT");

                    b.HasKey("CategoryId");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            CategoryId = 100,
                            CategoryName = "Panaderia"
                        },
                        new
                        {
                            CategoryId = 101,
                            CategoryName = "Salado"
                        },
                        new
                        {
                            CategoryId = 102,
                            CategoryName = "Dulce"
                        });
                });

            modelBuilder.Entity("FrizzApp.Data.Entities.Order", b =>
                {
                    b.Property<int>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ClientName")
                        .HasColumnType("TEXT");

                    b.Property<string>("ClientPhone")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Date")
                        .HasColumnType("TEXT");

                    b.Property<string>("DeliveryAddress")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsDelivery")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsPaid")
                        .HasColumnType("INTEGER");

                    b.Property<int>("OrderStatusId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("PaymentTypeId")
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("TotalPrice")
                        .HasColumnType("TEXT");

                    b.HasKey("OrderId");

                    b.HasIndex("OrderStatusId");

                    b.HasIndex("PaymentTypeId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("FrizzApp.Data.Entities.OrderStatus", b =>
                {
                    b.Property<int>("OrderStatusId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("StatusName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("TEXT");

                    b.HasKey("OrderStatusId");

                    b.ToTable("ProductStates");

                    b.HasData(
                        new
                        {
                            OrderStatusId = 1,
                            StatusName = "Pending"
                        },
                        new
                        {
                            OrderStatusId = 2,
                            StatusName = "Done"
                        },
                        new
                        {
                            OrderStatusId = 3,
                            StatusName = "Canceled"
                        });
                });

            modelBuilder.Entity("FrizzApp.Data.Entities.PaymentType", b =>
                {
                    b.Property<int>("PaymentTypeId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("PaymentTypeName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("TEXT");

                    b.HasKey("PaymentTypeId");

                    b.ToTable("PaymentTypes");

                    b.HasData(
                        new
                        {
                            PaymentTypeId = 10,
                            PaymentTypeName = "Cash"
                        },
                        new
                        {
                            PaymentTypeId = 11,
                            PaymentTypeName = "MercadoPago"
                        },
                        new
                        {
                            PaymentTypeId = 13,
                            PaymentTypeName = "Debit"
                        },
                        new
                        {
                            PaymentTypeId = 14,
                            PaymentTypeName = "Credit"
                        });
                });

            modelBuilder.Entity("FrizzApp.Data.Entities.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("CategoryId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .HasMaxLength(120)
                        .HasColumnType("TEXT");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsPromo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasDefaultValue(false);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<string>("Notes")
                        .HasMaxLength(150)
                        .HasColumnType("TEXT");

                    b.Property<decimal?>("OldPrice")
                        .HasPrecision(7, 2)
                        .HasColumnType("TEXT");

                    b.Property<string>("Presentation")
                        .HasMaxLength(20)
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Price")
                        .ValueGeneratedOnAdd()
                        .HasPrecision(7, 2)
                        .HasColumnType("TEXT")
                        .HasDefaultValue(0m);

                    b.Property<int>("ProductStatusId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("ProductStatusId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("FrizzApp.Data.Entities.ProductStatus", b =>
                {
                    b.Property<int>("ProductStatusId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("TEXT");

                    b.HasKey("ProductStatusId");

                    b.ToTable("ProductStatus");

                    b.HasData(
                        new
                        {
                            ProductStatusId = 1,
                            Name = "Avaiable"
                        },
                        new
                        {
                            ProductStatusId = 2,
                            Name = "WithoutStock"
                        },
                        new
                        {
                            ProductStatusId = 3,
                            Name = "Deleted"
                        });
                });

            modelBuilder.Entity("FrizzApp.Data.Entities.Order", b =>
                {
                    b.HasOne("FrizzApp.Data.Entities.OrderStatus", "OrderStatus")
                        .WithMany()
                        .HasForeignKey("OrderStatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FrizzApp.Data.Entities.PaymentType", "PaymentType")
                        .WithMany()
                        .HasForeignKey("PaymentTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("OrderStatus");

                    b.Navigation("PaymentType");
                });

            modelBuilder.Entity("FrizzApp.Data.Entities.Product", b =>
                {
                    b.HasOne("FrizzApp.Data.Entities.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId");

                    b.HasOne("FrizzApp.Data.Entities.ProductStatus", "ProductStatus")
                        .WithMany()
                        .HasForeignKey("ProductStatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("ProductStatus");
                });
#pragma warning restore 612, 618
        }
    }
}