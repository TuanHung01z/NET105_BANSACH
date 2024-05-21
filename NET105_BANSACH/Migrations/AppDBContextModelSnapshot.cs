﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NET105_BANSACH.Models;

#nullable disable

namespace NET105_BANSACH.Migrations
{
    [DbContext(typeof(AppDBContext))]
    partial class AppDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("NET105_BANSACH.Models.Account", b =>
                {
                    b.Property<string>("Username")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Username");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("NET105_BANSACH.Models.Bill", b =>
                {
                    b.Property<Guid>("BillID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("BillID");

                    b.HasIndex("Username");

                    b.ToTable("Bills");
                });

            modelBuilder.Entity("NET105_BANSACH.Models.BillDetails", b =>
                {
                    b.Property<Guid>("BillDetailsID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("BillID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("BookID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("BillDetailsID");

                    b.HasIndex("BillID");

                    b.HasIndex("BookID");

                    b.ToTable("BillDetails");
                });

            modelBuilder.Entity("NET105_BANSACH.Models.Book", b =>
                {
                    b.Property<string>("BookID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Amount")
                        .HasColumnType("int");

                    b.Property<string>("Author")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("BookID");

                    b.ToTable("Books");
                });

            modelBuilder.Entity("NET105_BANSACH.Models.Cart", b =>
                {
                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(256)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Username");

                    b.ToTable("Carts");
                });

            modelBuilder.Entity("NET105_BANSACH.Models.CartDetails", b =>
                {
                    b.Property<Guid>("CartDetailsID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("BookID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CartUsername")
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("ProductID")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CartDetailsID");

                    b.HasIndex("BookID");

                    b.HasIndex("CartUsername");

                    b.ToTable("CartsDetails");
                });

            modelBuilder.Entity("NET105_BANSACH.Models.Bill", b =>
                {
                    b.HasOne("NET105_BANSACH.Models.Account", "Account")
                        .WithMany("Bills")
                        .HasForeignKey("Username");

                    b.Navigation("Account");
                });

            modelBuilder.Entity("NET105_BANSACH.Models.BillDetails", b =>
                {
                    b.HasOne("NET105_BANSACH.Models.Bill", "Bill")
                        .WithMany("BillDetails")
                        .HasForeignKey("BillID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NET105_BANSACH.Models.Book", "Book")
                        .WithMany("BillDetails")
                        .HasForeignKey("BookID");

                    b.Navigation("Bill");

                    b.Navigation("Book");
                });

            modelBuilder.Entity("NET105_BANSACH.Models.Cart", b =>
                {
                    b.HasOne("NET105_BANSACH.Models.Account", "Account")
                        .WithOne("Cart")
                        .HasForeignKey("NET105_BANSACH.Models.Cart", "Username")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");
                });

            modelBuilder.Entity("NET105_BANSACH.Models.CartDetails", b =>
                {
                    b.HasOne("NET105_BANSACH.Models.Book", "Book")
                        .WithMany("CartDetails")
                        .HasForeignKey("BookID");

                    b.HasOne("NET105_BANSACH.Models.Cart", "Cart")
                        .WithMany()
                        .HasForeignKey("CartUsername");

                    b.Navigation("Book");

                    b.Navigation("Cart");
                });

            modelBuilder.Entity("NET105_BANSACH.Models.Account", b =>
                {
                    b.Navigation("Bills");

                    b.Navigation("Cart");
                });

            modelBuilder.Entity("NET105_BANSACH.Models.Bill", b =>
                {
                    b.Navigation("BillDetails");
                });

            modelBuilder.Entity("NET105_BANSACH.Models.Book", b =>
                {
                    b.Navigation("BillDetails");

                    b.Navigation("CartDetails");
                });
#pragma warning restore 612, 618
        }
    }
}
