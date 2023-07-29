﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using StockService.Data;

#nullable disable

namespace StockService.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20230729121040_fixeddiagram")]
    partial class fixeddiagram
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("StockService.Models.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("DateExecution")
                        .HasColumnType("datetime2");

                    b.Property<double?>("ExecutedPrice")
                        .HasPrecision(6, 2)
                        .HasColumnType("float(6)");

                    b.Property<int>("OrderStatus")
                        .HasColumnType("int");

                    b.Property<int>("OriginalOrderId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("OriginalOrderId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("StockService.Models.OriginalOrder", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DateDeposit")
                        .HasColumnType("datetime2");

                    b.Property<int>("OrderType")
                        .HasColumnType("int");

                    b.Property<int>("OriginalQuantity")
                        .HasColumnType("int");

                    b.Property<double>("Price")
                        .HasPrecision(6, 2)
                        .HasColumnType("float(6)");

                    b.Property<int>("RemainingQuantity")
                        .HasColumnType("int");

                    b.Property<int>("StockId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("StockId");

                    b.HasIndex("UserId");

                    b.ToTable("OriginalOrders");
                });

            modelBuilder.Entity("StockService.Models.Stock", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<double>("AveragePrice")
                        .HasPrecision(6, 2)
                        .HasColumnType("float(6)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Stocks");
                });

            modelBuilder.Entity("StockService.Models.StockUnit", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DateBought")
                        .HasColumnType("datetime2");

                    b.Property<int>("StockId")
                        .HasColumnType("int");

                    b.Property<int>("StockUnitStatus")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("StockId");

                    b.HasIndex("UserId");

                    b.ToTable("StockUnits");
                });

            modelBuilder.Entity("StockService.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<double>("Balance")
                        .HasPrecision(6, 2)
                        .HasColumnType("float(6)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserType")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("StockService.Models.Order", b =>
                {
                    b.HasOne("StockService.Models.OriginalOrder", "OriginalOrder")
                        .WithMany("Orders")
                        .HasForeignKey("OriginalOrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("OriginalOrder");
                });

            modelBuilder.Entity("StockService.Models.OriginalOrder", b =>
                {
                    b.HasOne("StockService.Models.Stock", "Stock")
                        .WithMany("OriginalOrders")
                        .HasForeignKey("StockId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StockService.Models.User", "User")
                        .WithMany("OriginalOrders")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Stock");

                    b.Navigation("User");
                });

            modelBuilder.Entity("StockService.Models.StockUnit", b =>
                {
                    b.HasOne("StockService.Models.Stock", "Stock")
                        .WithMany("StockUnits")
                        .HasForeignKey("StockId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StockService.Models.User", "User")
                        .WithMany("StockUnits")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Stock");

                    b.Navigation("User");
                });

            modelBuilder.Entity("StockService.Models.OriginalOrder", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("StockService.Models.Stock", b =>
                {
                    b.Navigation("OriginalOrders");

                    b.Navigation("StockUnits");
                });

            modelBuilder.Entity("StockService.Models.User", b =>
                {
                    b.Navigation("OriginalOrders");

                    b.Navigation("StockUnits");
                });
#pragma warning restore 612, 618
        }
    }
}