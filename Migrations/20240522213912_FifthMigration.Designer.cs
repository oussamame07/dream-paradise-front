﻿// <auto-generated />
using System;
using DreamParadise.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DreamParadise.Migrations
{
    [DbContext(typeof(MyContext))]
    [Migration("20240522213912_FifthMigration")]
    partial class FifthMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("DreamParadise.Models.Rating", b =>
                {
                    b.Property<int>("RatingId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("RatingService")
                        .HasColumnType("int");

                    b.Property<string>("Suggestion")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<int?>("UserWhoRatedUserId")
                        .HasColumnType("int");

                    b.HasKey("RatingId");

                    b.HasIndex("UserWhoRatedUserId");

                    b.ToTable("Ratings");
                });

            modelBuilder.Entity("DreamParadise.Models.Reservation", b =>
                {
                    b.Property<int>("ReservationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("AdultsCount")
                        .HasColumnType("int");

                    b.Property<DateTime>("CheckIn")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("CheckOut")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("ChildCount")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("RoomType")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("TotalPrice")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<int?>("UserWhoReservedUserId")
                        .HasColumnType("int");

                    b.HasKey("ReservationId");

                    b.HasIndex("UserWhoReservedUserId");

                    b.ToTable("Reservations");
                });

            modelBuilder.Entity("DreamParadise.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("DreamParadise.Models.Rating", b =>
                {
                    b.HasOne("DreamParadise.Models.User", "UserWhoRated")
                        .WithMany("UserRatings")
                        .HasForeignKey("UserWhoRatedUserId");

                    b.Navigation("UserWhoRated");
                });

            modelBuilder.Entity("DreamParadise.Models.Reservation", b =>
                {
                    b.HasOne("DreamParadise.Models.User", "UserWhoReserved")
                        .WithMany("UserReservations")
                        .HasForeignKey("UserWhoReservedUserId");

                    b.Navigation("UserWhoReserved");
                });

            modelBuilder.Entity("DreamParadise.Models.User", b =>
                {
                    b.Navigation("UserRatings");

                    b.Navigation("UserReservations");
                });
#pragma warning restore 612, 618
        }
    }
}
