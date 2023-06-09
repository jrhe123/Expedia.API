﻿// <auto-generated />
using System;
using Expedia.API.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Expedia.API.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20230522040012_updateOrderMigration")]
    partial class updateOrderMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Expedia.API.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "fb6d4f10-79ed-4aff-a915-4ce29dc9c7e0",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "a7de4f9a-155d-40a6-b438-a3f7c31a7889",
                            Email = "admin@testorg.com",
                            EmailConfirmed = true,
                            LockoutEnabled = false,
                            NormalizedEmail = "ADMIN@TESTORG.COM",
                            NormalizedUserName = "ADMIN@TESTORG.COM",
                            PasswordHash = "AQAAAAIAAYagAAAAEN/jcCUntmh5wxwsKIsB66MFBHCuSh1Z6VnRow6/+MJkPkai0vvXcX4DKYBO4Y+FyQ==",
                            PhoneNumber = "1234567890",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "2c31444e-136a-4f9f-b018-c7df697be8c4",
                            TwoFactorEnabled = false,
                            UserName = "admin@testorg.com"
                        });
                });

            modelBuilder.Entity("Expedia.API.Models.LineItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<double?>("DiscountPercent")
                        .HasColumnType("float");

                    b.Property<Guid?>("OrderId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("OriginalPrice")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<Guid?>("ShoppingCartId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("TouristRouteId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.HasIndex("ShoppingCartId");

                    b.HasIndex("TouristRouteId");

                    b.ToTable("LineItems");
                });

            modelBuilder.Entity("Expedia.API.Models.Order", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreateDateUTC")
                        .HasColumnType("datetime2");

                    b.Property<int>("State")
                        .HasColumnType("int");

                    b.Property<string>("TransactionMetadata")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("Expedia.API.Models.ShoppingCart", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("ShoppingCarts");
                });

            modelBuilder.Entity("Expedia.API.Models.TouristRoute", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("DepartureCity")
                        .HasColumnType("int");

                    b.Property<DateTime?>("DepartureTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(1500)
                        .HasColumnType("nvarchar(1500)");

                    b.Property<double?>("DiscountPercent")
                        .HasColumnType("float");

                    b.Property<string>("Features")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Fees")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Notes")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("OriginalPrice")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<double?>("Rating")
                        .HasColumnType("float");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int?>("TravelDays")
                        .HasColumnType("int");

                    b.Property<int?>("TripType")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdateTime")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("TouristRoutes");

                    b.HasData(
                        new
                        {
                            Id = new Guid("fb6d4f10-79ed-4aff-a915-4ce29dc9c7e1"),
                            CreateTime = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DepartureCity = 0,
                            Description = "this is description 1",
                            DiscountPercent = 0.01,
                            Features = "<div class\"bd\"><p style=\"text-align:center\"><span>features </span><strong></strong></p></div>",
                            Fees = "<div class\"bd\"><dl class=\"mod_info_box\"><dt>fee includes: 1</dt><dd><div>123</div></dd></dl></div>",
                            Notes = "<div class\"bd\"><dl class=\"mod_info_box\"><dt>notes includes: 1</dt><dd><div>321</div></dd></dl></div>",
                            OriginalPrice = 1999.99m,
                            Rating = 3.5,
                            Title = "this is title 1",
                            TravelDays = 8,
                            TripType = 0
                        },
                        new
                        {
                            Id = new Guid("fb6d4f10-79ed-4aff-a915-4ce29dc9c7e2"),
                            CreateTime = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DepartureCity = 1,
                            Description = "this is description 2",
                            DiscountPercent = 0.01,
                            Features = "<div class\"bd\"><p style=\"text-align:center\"><span>features 2</span><strong></strong></p></div>",
                            Fees = "<div class\"bd\"><dl class=\"mod_info_box\"><dt>fee includes: 2</dt><dd><div>123</div></dd></dl></div>",
                            Notes = "<div class\"bd\"><dl class=\"mod_info_box\"><dt>notes includes: 2</dt><dd><div>321</div></dd></dl></div>",
                            OriginalPrice = 1999.99m,
                            Rating = 3.7999999999999998,
                            Title = "this is title 2",
                            TravelDays = 8,
                            TripType = 2
                        },
                        new
                        {
                            Id = new Guid("fb6d4f10-79ed-4aff-a915-4ce29dc9c7e3"),
                            CreateTime = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DepartureCity = 1,
                            Description = "this is description 3",
                            DiscountPercent = 0.01,
                            Features = "<div class\"bd\"><p style=\"text-align:center\"><span>features 3</span><strong></strong></p></div>",
                            Fees = "<div class\"bd\"><dl class=\"mod_info_box\"><dt>fee includes: 3</dt><dd><div>123</div></dd></dl></div>",
                            Notes = "<div class\"bd\"><dl class=\"mod_info_box\"><dt>notes includes: 3</dt><dd><div>321</div></dd></dl></div>",
                            OriginalPrice = 1999.99m,
                            Rating = 4.5,
                            Title = "this is title 3",
                            TravelDays = 6,
                            TripType = 2
                        },
                        new
                        {
                            Id = new Guid("fb6d4f10-79ed-4aff-a915-4ce29dc9c7e4"),
                            CreateTime = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DepartureCity = 3,
                            Description = "this is description 4",
                            DiscountPercent = 0.01,
                            Features = "<div class\"bd\"><p style=\"text-align:center\"><span>features 4</span><strong></strong></p></div>",
                            Fees = "<div class\"bd\"><dl class=\"mod_info_box\"><dt>fee includes: 4</dt><dd><div>123</div></dd></dl></div>",
                            Notes = "<div class\"bd\"><dl class=\"mod_info_box\"><dt>notes includes: 4</dt><dd><div>321</div></dd></dl></div>",
                            OriginalPrice = 1999.99m,
                            Rating = 5.0,
                            Title = "this is title 4",
                            TravelDays = 5,
                            TripType = 3
                        },
                        new
                        {
                            Id = new Guid("fb6d4f10-79ed-4aff-a915-4ce29dc9c7e5"),
                            CreateTime = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DepartureCity = 3,
                            Description = "this is description 5",
                            DiscountPercent = 0.01,
                            Features = "<div class\"bd\"><p style=\"text-align:center\"><span>features 5</span><strong></strong></p></div>",
                            Fees = "<div class\"bd\"><dl class=\"mod_info_box\"><dt>fee includes: 5</dt><dd><div>123</div></dd></dl></div>",
                            Notes = "<div class\"bd\"><dl class=\"mod_info_box\"><dt>notes includes: 5</dt><dd><div>321</div></dd></dl></div>",
                            OriginalPrice = 1999.99m,
                            Rating = 3.0,
                            Title = "this is title 5",
                            TravelDays = 5,
                            TripType = 0
                        },
                        new
                        {
                            Id = new Guid("fb6d4f10-79ed-4aff-a915-4ce29dc9c7e6"),
                            CreateTime = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DepartureCity = 3,
                            Description = "this is description 6",
                            DiscountPercent = 0.01,
                            Features = "<div class\"bd\"><p style=\"text-align:center\"><span>features 6</span><strong></strong></p></div>",
                            Fees = "<div class\"bd\"><dl class=\"mod_info_box\"><dt>fee includes: 6</dt><dd><div>123</div></dd></dl></div>",
                            Notes = "<div class\"bd\"><dl class=\"mod_info_box\"><dt>notes includes: 6</dt><dd><div>321</div></dd></dl></div>",
                            OriginalPrice = 1999.99m,
                            Rating = 4.5,
                            Title = "this is title 6",
                            TravelDays = 3,
                            TripType = 0
                        });
                });

            modelBuilder.Entity("Expedia.API.Models.TouristRoutePicture", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<Guid>("TouristRouteId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("TouristRouteId");

                    b.ToTable("TouristRoutePictures");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            TouristRouteId = new Guid("fb6d4f10-79ed-4aff-a915-4ce29dc9c7e1"),
                            Url = "../../assets/images/1.jpg"
                        },
                        new
                        {
                            Id = 2,
                            TouristRouteId = new Guid("fb6d4f10-79ed-4aff-a915-4ce29dc9c7e1"),
                            Url = "../../assets/images/2.jpg"
                        },
                        new
                        {
                            Id = 3,
                            TouristRouteId = new Guid("fb6d4f10-79ed-4aff-a915-4ce29dc9c7e1"),
                            Url = "../../assets/images/3.jpg"
                        },
                        new
                        {
                            Id = 4,
                            TouristRouteId = new Guid("fb6d4f10-79ed-4aff-a915-4ce29dc9c7e1"),
                            Url = "../../assets/images/4.jpg"
                        },
                        new
                        {
                            Id = 5,
                            TouristRouteId = new Guid("fb6d4f10-79ed-4aff-a915-4ce29dc9c7e2"),
                            Url = "../../assets/images/5.jpg"
                        },
                        new
                        {
                            Id = 6,
                            TouristRouteId = new Guid("fb6d4f10-79ed-4aff-a915-4ce29dc9c7e3"),
                            Url = "../../assets/images/6.jpg"
                        },
                        new
                        {
                            Id = 7,
                            TouristRouteId = new Guid("fb6d4f10-79ed-4aff-a915-4ce29dc9c7e4"),
                            Url = "../../assets/images/7.jpg"
                        },
                        new
                        {
                            Id = 8,
                            TouristRouteId = new Guid("fb6d4f10-79ed-4aff-a915-4ce29dc9c7e5"),
                            Url = "../../assets/images/8.jpg"
                        },
                        new
                        {
                            Id = 9,
                            TouristRouteId = new Guid("fb6d4f10-79ed-4aff-a915-4ce29dc9c7e6"),
                            Url = "../../assets/images/9.jpg"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "fb6d4f10-79ed-4aff-a915-4ce29dc9c7e9",
                            Name = "Admin",
                            NormalizedName = "ADMIN"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ApplicationUserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("ApplicationUserId");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ApplicationUserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("ApplicationUserId");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ApplicationUserId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("ApplicationUserId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);

                    b.HasData(
                        new
                        {
                            UserId = "fb6d4f10-79ed-4aff-a915-4ce29dc9c7e0",
                            RoleId = "fb6d4f10-79ed-4aff-a915-4ce29dc9c7e9"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ApplicationUserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.HasIndex("ApplicationUserId");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("Expedia.API.Models.LineItem", b =>
                {
                    b.HasOne("Expedia.API.Models.Order", null)
                        .WithMany("OrderItems")
                        .HasForeignKey("OrderId");

                    b.HasOne("Expedia.API.Models.ShoppingCart", null)
                        .WithMany("ShoppingCartItems")
                        .HasForeignKey("ShoppingCartId");

                    b.HasOne("Expedia.API.Models.TouristRoute", "TouristRoute")
                        .WithMany()
                        .HasForeignKey("TouristRouteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TouristRoute");
                });

            modelBuilder.Entity("Expedia.API.Models.Order", b =>
                {
                    b.HasOne("Expedia.API.Models.ApplicationUser", "User")
                        .WithMany("Orders")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Expedia.API.Models.ShoppingCart", b =>
                {
                    b.HasOne("Expedia.API.Models.ApplicationUser", "User")
                        .WithOne("ShoppingCart")
                        .HasForeignKey("Expedia.API.Models.ShoppingCart", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Expedia.API.Models.TouristRoutePicture", b =>
                {
                    b.HasOne("Expedia.API.Models.TouristRoute", "TouristRoute")
                        .WithMany("TouristRoutePictures")
                        .HasForeignKey("TouristRouteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TouristRoute");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Expedia.API.Models.ApplicationUser", null)
                        .WithMany("Claims")
                        .HasForeignKey("ApplicationUserId");

                    b.HasOne("Expedia.API.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Expedia.API.Models.ApplicationUser", null)
                        .WithMany("Logins")
                        .HasForeignKey("ApplicationUserId");

                    b.HasOne("Expedia.API.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Expedia.API.Models.ApplicationUser", null)
                        .WithMany("UserRoles")
                        .HasForeignKey("ApplicationUserId");

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Expedia.API.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Expedia.API.Models.ApplicationUser", null)
                        .WithMany("Tokens")
                        .HasForeignKey("ApplicationUserId");

                    b.HasOne("Expedia.API.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Expedia.API.Models.ApplicationUser", b =>
                {
                    b.Navigation("Claims");

                    b.Navigation("Logins");

                    b.Navigation("Orders");

                    b.Navigation("ShoppingCart")
                        .IsRequired();

                    b.Navigation("Tokens");

                    b.Navigation("UserRoles");
                });

            modelBuilder.Entity("Expedia.API.Models.Order", b =>
                {
                    b.Navigation("OrderItems");
                });

            modelBuilder.Entity("Expedia.API.Models.ShoppingCart", b =>
                {
                    b.Navigation("ShoppingCartItems");
                });

            modelBuilder.Entity("Expedia.API.Models.TouristRoute", b =>
                {
                    b.Navigation("TouristRoutePictures");
                });
#pragma warning restore 612, 618
        }
    }
}
