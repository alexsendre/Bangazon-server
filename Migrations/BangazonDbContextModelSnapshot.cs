﻿// <auto-generated />
using System;
using BangazonBE;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BangazonBE.Migrations
{
    [DbContext(typeof(BangazonDbContext))]
    partial class BangazonDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("BangazonBE.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Title = "Appliances"
                        },
                        new
                        {
                            Id = 2,
                            Title = "Electronics"
                        },
                        new
                        {
                            Id = 3,
                            Title = "Musical Instruments"
                        });
                });

            modelBuilder.Entity("BangazonBE.Models.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("CustomerId")
                        .HasColumnType("integer");

                    b.Property<bool>("IsComplete")
                        .HasColumnType("boolean");

                    b.Property<int>("PaymentTypeId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Orders");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CustomerId = 3,
                            IsComplete = true,
                            PaymentTypeId = 2
                        },
                        new
                        {
                            Id = 2,
                            CustomerId = 2,
                            IsComplete = true,
                            PaymentTypeId = 3
                        });
                });

            modelBuilder.Entity("BangazonBE.Models.PaymentType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("PaymentTypes");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Type = "Credit"
                        },
                        new
                        {
                            Id = 2,
                            Type = "Debit"
                        },
                        new
                        {
                            Id = 3,
                            Type = "PayPal"
                        });
                });

            modelBuilder.Entity("BangazonBE.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("CategoryId")
                        .HasColumnType("integer");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsAvailable")
                        .HasColumnType("boolean");

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric");

                    b.Property<int>("QuantityAvailable")
                        .HasColumnType("integer");

                    b.Property<int>("SellerId")
                        .HasColumnType("integer");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CategoryId = 1,
                            Description = "Heat it up bro",
                            ImageUrl = "https://images.thdstatic.com/productImages/45192fb2-63e1-429d-a340-eea0644cadec/svn/stainless-steel-whirlpool-over-the-range-microwaves-wmh31017hs-64_600.jpg",
                            IsAvailable = true,
                            Price = 299.99m,
                            QuantityAvailable = 3,
                            SellerId = 2,
                            Title = "Microwave"
                        },
                        new
                        {
                            Id = 2,
                            CategoryId = 2,
                            Description = "Type me up bro",
                            ImageUrl = "https://cdn.thewirecutter.com/wp-content/media/2023/11/mechanicalkeyboards-2048px-9138.jpg?auto=webp&quality=75&crop=1.91:1&width=1200",
                            IsAvailable = true,
                            Price = 149.99m,
                            QuantityAvailable = 2,
                            SellerId = 1,
                            Title = "Keyboard"
                        },
                        new
                        {
                            Id = 3,
                            CategoryId = 3,
                            Description = "Strum me bro",
                            ImageUrl = "https://m.media-amazon.com/images/I/714kO41XjDL.jpg",
                            IsAvailable = false,
                            Price = 649.99m,
                            QuantityAvailable = 0,
                            SellerId = 2,
                            Title = "Guitar"
                        });
                });

            modelBuilder.Entity("BangazonBE.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Bio")
                        .HasColumnType("text");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsSeller")
                        .HasColumnType("boolean");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ProfileImageUrl")
                        .HasColumnType("text");

                    b.Property<string>("Uid")
                        .HasColumnType("text");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Bio = "what up! new here",
                            DateCreated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "johnny@test.com",
                            FirstName = "John",
                            IsSeller = true,
                            LastName = "Doe",
                            ProfileImageUrl = "",
                            Username = "testuser"
                        },
                        new
                        {
                            Id = 2,
                            Bio = "i love raisin canes",
                            DateCreated = new DateTime(2024, 2, 27, 0, 0, 0, 0, DateTimeKind.Local),
                            Email = "carlcane@test.com",
                            FirstName = "Carl",
                            IsSeller = true,
                            LastName = "Cane",
                            ProfileImageUrl = "",
                            Username = "broodski"
                        },
                        new
                        {
                            Id = 3,
                            Bio = "I am Jackson Black",
                            DateCreated = new DateTime(2024, 2, 27, 20, 25, 49, 343, DateTimeKind.Local).AddTicks(7174),
                            Email = "jackblack@test.com",
                            FirstName = "Jack",
                            IsSeller = false,
                            LastName = "Black",
                            ProfileImageUrl = "",
                            Username = "superbasic"
                        });
                });

            modelBuilder.Entity("OrderProduct", b =>
                {
                    b.Property<int>("OrdersId")
                        .HasColumnType("integer");

                    b.Property<int>("ProductsId")
                        .HasColumnType("integer");

                    b.HasKey("OrdersId", "ProductsId");

                    b.HasIndex("ProductsId");

                    b.ToTable("OrderProduct");
                });

            modelBuilder.Entity("OrderProduct", b =>
                {
                    b.HasOne("BangazonBE.Models.Order", null)
                        .WithMany()
                        .HasForeignKey("OrdersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BangazonBE.Models.Product", null)
                        .WithMany()
                        .HasForeignKey("ProductsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
