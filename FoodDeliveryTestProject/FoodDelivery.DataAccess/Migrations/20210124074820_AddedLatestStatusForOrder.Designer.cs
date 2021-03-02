﻿// <auto-generated />
using System;
using FoodDelivery.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FoodDelivery.DataAccess.Migrations
{
    [DbContext(typeof(FoodDeliveryDbContext))]
    [Migration("20210124074820_AddedLatestStatusForOrder")]
    partial class AddedLatestStatusForOrder
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.1");

            modelBuilder.Entity("FoodDelivery.Business.Entities.Account", b =>
                {
                    b.Property<int>("AccountId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Address")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("HashedPassword")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.Property<string>("Surname")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("UserName")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("AccountId");

                    b.ToTable("AccountsSet");
                });

            modelBuilder.Entity("FoodDelivery.Business.Entities.BlockedUsers", b =>
                {
                    b.Property<int>("BlockedUserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("AccountId")
                        .HasColumnType("int");

                    b.Property<int>("OwnerId")
                        .HasColumnType("int");

                    b.HasKey("BlockedUserId");

                    b.HasIndex("OwnerId");

                    b.ToTable("BlockedUsersSet");
                });

            modelBuilder.Entity("FoodDelivery.Business.Entities.Meal", b =>
                {
                    b.Property<int>("MealId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Description")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Name")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(10,4)");

                    b.Property<int>("RestaurantId")
                        .HasColumnType("int");

                    b.HasKey("MealId");

                    b.HasIndex("RestaurantId");

                    b.ToTable("MealsSet");
                });

            modelBuilder.Entity("FoodDelivery.Business.Entities.MealForOrder", b =>
                {
                    b.Property<int>("MealForOrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("AmmountOfMeals")
                        .HasColumnType("int");

                    b.Property<string>("MealName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("MealPrice")
                        .HasColumnType("decimal(10,4)");

                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.HasKey("MealForOrderId");

                    b.HasIndex("OrderId");

                    b.ToTable("MealsForOrdersSet");
                });

            modelBuilder.Entity("FoodDelivery.Business.Entities.MealImage", b =>
                {
                    b.Property<int>("MealImageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<byte[]>("Image")
                        .HasColumnType("image");

                    b.Property<int>("MealId")
                        .HasColumnType("int");

                    b.HasKey("MealImageId");

                    b.HasIndex("MealId")
                        .IsUnique();

                    b.ToTable("MealsImagesSet");
                });

            modelBuilder.Entity("FoodDelivery.Business.Entities.Order", b =>
                {
                    b.Property<int>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<int>("LatestOrderStatus")
                        .HasColumnType("int");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(14,4)");

                    b.Property<int>("RestaurantId")
                        .HasColumnType("int");

                    b.HasKey("OrderId");

                    b.HasIndex("CustomerId");

                    b.ToTable("OrdersSet");
                });

            modelBuilder.Entity("FoodDelivery.Business.Entities.OrderStatus", b =>
                {
                    b.Property<int>("OrderStatusId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<DateTime>("StatusChangeTime")
                        .HasColumnType("datetime2");

                    b.HasKey("OrderStatusId");

                    b.HasIndex("OrderId");

                    b.ToTable("OrderStatusesSet");
                });

            modelBuilder.Entity("FoodDelivery.Business.Entities.Restaurant", b =>
                {
                    b.Property<int>("RestaurantId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Description")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Name")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("OwnerId")
                        .HasColumnType("int");

                    b.HasKey("RestaurantId");

                    b.HasIndex("OwnerId");

                    b.ToTable("RestaurantsSet");
                });

            modelBuilder.Entity("FoodDelivery.Business.Entities.RestaurantImage", b =>
                {
                    b.Property<int>("RestaurantImageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<byte[]>("Image")
                        .HasColumnType("image");

                    b.Property<int>("RestaurantId")
                        .HasColumnType("int");

                    b.HasKey("RestaurantImageId");

                    b.HasIndex("RestaurantId")
                        .IsUnique();

                    b.ToTable("RestaurantsImagesSet");
                });

            modelBuilder.Entity("FoodDelivery.Business.Entities.BlockedUsers", b =>
                {
                    b.HasOne("FoodDelivery.Business.Entities.Account", "Owner")
                        .WithMany("BlockedUsers")
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("FoodDelivery.Business.Entities.Meal", b =>
                {
                    b.HasOne("FoodDelivery.Business.Entities.Restaurant", "Restaurant")
                        .WithMany("Meals")
                        .HasForeignKey("RestaurantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Restaurant");
                });

            modelBuilder.Entity("FoodDelivery.Business.Entities.MealForOrder", b =>
                {
                    b.HasOne("FoodDelivery.Business.Entities.Order", "Order")
                        .WithMany("MealsForOrder")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Order");
                });

            modelBuilder.Entity("FoodDelivery.Business.Entities.MealImage", b =>
                {
                    b.HasOne("FoodDelivery.Business.Entities.Meal", "Meal")
                        .WithOne("Image")
                        .HasForeignKey("FoodDelivery.Business.Entities.MealImage", "MealId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Meal");
                });

            modelBuilder.Entity("FoodDelivery.Business.Entities.Order", b =>
                {
                    b.HasOne("FoodDelivery.Business.Entities.Account", "Customer")
                        .WithMany("Orders")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("FoodDelivery.Business.Entities.OrderStatus", b =>
                {
                    b.HasOne("FoodDelivery.Business.Entities.Order", "Order")
                        .WithMany("OrderStatuses")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");
                });

            modelBuilder.Entity("FoodDelivery.Business.Entities.Restaurant", b =>
                {
                    b.HasOne("FoodDelivery.Business.Entities.Account", "Owner")
                        .WithMany("Restaurants")
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("FoodDelivery.Business.Entities.RestaurantImage", b =>
                {
                    b.HasOne("FoodDelivery.Business.Entities.Restaurant", "Restaurant")
                        .WithOne("Image")
                        .HasForeignKey("FoodDelivery.Business.Entities.RestaurantImage", "RestaurantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Restaurant");
                });

            modelBuilder.Entity("FoodDelivery.Business.Entities.Account", b =>
                {
                    b.Navigation("BlockedUsers");

                    b.Navigation("Orders");

                    b.Navigation("Restaurants");
                });

            modelBuilder.Entity("FoodDelivery.Business.Entities.Meal", b =>
                {
                    b.Navigation("Image");
                });

            modelBuilder.Entity("FoodDelivery.Business.Entities.Order", b =>
                {
                    b.Navigation("MealsForOrder");

                    b.Navigation("OrderStatuses");
                });

            modelBuilder.Entity("FoodDelivery.Business.Entities.Restaurant", b =>
                {
                    b.Navigation("Image");

                    b.Navigation("Meals");
                });
#pragma warning restore 612, 618
        }
    }
}
