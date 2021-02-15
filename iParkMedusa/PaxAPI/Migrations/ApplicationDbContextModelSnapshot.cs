﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PaxAPI.Contexts;

namespace PaxAPI.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("Identity")
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.2");

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

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("PaxAPI.Entities.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

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

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("PaxAPI.Entities.Reservation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<bool>("ChargingOption")
                        .HasColumnType("bit");

                    b.Property<bool>("CoverOption")
                        .HasColumnType("bit");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("End")
                        .HasColumnType("datetime2");

                    b.Property<bool>("OutsideOption")
                        .HasColumnType("bit");

                    b.Property<int>("SlotId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Start")
                        .HasColumnType("datetime2");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("ValletOption")
                        .HasColumnType("bit");

                    b.Property<double>("Value")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("SlotId");

                    b.HasIndex("UserId");

                    b.ToTable("Reservations");
                });

            modelBuilder.Entity("PaxAPI.Entities.Slot", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<bool>("HasVallet")
                        .HasColumnType("bit");

                    b.Property<bool>("IsChargingAvailable")
                        .HasColumnType("bit");

                    b.Property<bool>("IsCovered")
                        .HasColumnType("bit");

                    b.Property<bool>("IsCovidFreeCertified")
                        .HasColumnType("bit");

                    b.Property<bool>("IsOutside")
                        .HasColumnType("bit");

                    b.Property<string>("Locator")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("PricePerHour")
                        .HasColumnType("float");

                    b.Property<bool>("PrioritySlot")
                        .HasColumnType("bit");

                    b.Property<bool>("SocialDistanceFlag")
                        .HasColumnType("bit");

                    b.Property<int>("StatusId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("StatusId");

                    b.ToTable("Slots");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            HasVallet = true,
                            IsChargingAvailable = true,
                            IsCovered = true,
                            IsCovidFreeCertified = true,
                            IsOutside = false,
                            Locator = "A01",
                            PricePerHour = 5.0,
                            PrioritySlot = true,
                            SocialDistanceFlag = false,
                            StatusId = 1
                        },
                        new
                        {
                            Id = 2,
                            HasVallet = true,
                            IsChargingAvailable = true,
                            IsCovered = true,
                            IsCovidFreeCertified = true,
                            IsOutside = false,
                            Locator = "A02",
                            PricePerHour = 5.0,
                            PrioritySlot = true,
                            SocialDistanceFlag = false,
                            StatusId = 1
                        },
                        new
                        {
                            Id = 3,
                            HasVallet = true,
                            IsChargingAvailable = true,
                            IsCovered = true,
                            IsCovidFreeCertified = true,
                            IsOutside = false,
                            Locator = "A03",
                            PricePerHour = 5.0,
                            PrioritySlot = false,
                            SocialDistanceFlag = false,
                            StatusId = 2
                        },
                        new
                        {
                            Id = 4,
                            HasVallet = true,
                            IsChargingAvailable = true,
                            IsCovered = true,
                            IsCovidFreeCertified = true,
                            IsOutside = false,
                            Locator = "A04",
                            PricePerHour = 5.0,
                            PrioritySlot = false,
                            SocialDistanceFlag = false,
                            StatusId = 1
                        },
                        new
                        {
                            Id = 5,
                            HasVallet = true,
                            IsChargingAvailable = true,
                            IsCovered = true,
                            IsCovidFreeCertified = true,
                            IsOutside = false,
                            Locator = "A05",
                            PricePerHour = 5.0,
                            PrioritySlot = false,
                            SocialDistanceFlag = false,
                            StatusId = 1
                        },
                        new
                        {
                            Id = 6,
                            HasVallet = true,
                            IsChargingAvailable = true,
                            IsCovered = true,
                            IsCovidFreeCertified = true,
                            IsOutside = false,
                            Locator = "A06",
                            PricePerHour = 5.0,
                            PrioritySlot = false,
                            SocialDistanceFlag = false,
                            StatusId = 1
                        },
                        new
                        {
                            Id = 7,
                            HasVallet = false,
                            IsChargingAvailable = true,
                            IsCovered = true,
                            IsCovidFreeCertified = false,
                            IsOutside = false,
                            Locator = "B01",
                            PricePerHour = 2.0,
                            PrioritySlot = false,
                            SocialDistanceFlag = false,
                            StatusId = 1
                        },
                        new
                        {
                            Id = 8,
                            HasVallet = false,
                            IsChargingAvailable = true,
                            IsCovered = true,
                            IsCovidFreeCertified = false,
                            IsOutside = false,
                            Locator = "B02",
                            PricePerHour = 2.0,
                            PrioritySlot = false,
                            SocialDistanceFlag = false,
                            StatusId = 1
                        },
                        new
                        {
                            Id = 9,
                            HasVallet = false,
                            IsChargingAvailable = true,
                            IsCovered = true,
                            IsCovidFreeCertified = false,
                            IsOutside = false,
                            Locator = "B03",
                            PricePerHour = 2.0,
                            PrioritySlot = false,
                            SocialDistanceFlag = false,
                            StatusId = 1
                        },
                        new
                        {
                            Id = 10,
                            HasVallet = false,
                            IsChargingAvailable = true,
                            IsCovered = true,
                            IsCovidFreeCertified = false,
                            IsOutside = false,
                            Locator = "B04",
                            PricePerHour = 2.0,
                            PrioritySlot = false,
                            SocialDistanceFlag = false,
                            StatusId = 1
                        },
                        new
                        {
                            Id = 11,
                            HasVallet = false,
                            IsChargingAvailable = true,
                            IsCovered = true,
                            IsCovidFreeCertified = false,
                            IsOutside = false,
                            Locator = "B05",
                            PricePerHour = 2.0,
                            PrioritySlot = false,
                            SocialDistanceFlag = false,
                            StatusId = 1
                        },
                        new
                        {
                            Id = 12,
                            HasVallet = false,
                            IsChargingAvailable = true,
                            IsCovered = true,
                            IsCovidFreeCertified = false,
                            IsOutside = false,
                            Locator = "B06",
                            PricePerHour = 2.0,
                            PrioritySlot = false,
                            SocialDistanceFlag = false,
                            StatusId = 1
                        },
                        new
                        {
                            Id = 13,
                            HasVallet = false,
                            IsChargingAvailable = false,
                            IsCovered = false,
                            IsCovidFreeCertified = false,
                            IsOutside = false,
                            Locator = "C01",
                            PricePerHour = 1.2,
                            PrioritySlot = false,
                            SocialDistanceFlag = false,
                            StatusId = 1
                        },
                        new
                        {
                            Id = 14,
                            HasVallet = false,
                            IsChargingAvailable = false,
                            IsCovered = false,
                            IsCovidFreeCertified = false,
                            IsOutside = false,
                            Locator = "C02",
                            PricePerHour = 1.2,
                            PrioritySlot = false,
                            SocialDistanceFlag = false,
                            StatusId = 1
                        },
                        new
                        {
                            Id = 15,
                            HasVallet = false,
                            IsChargingAvailable = false,
                            IsCovered = false,
                            IsCovidFreeCertified = false,
                            IsOutside = false,
                            Locator = "C03",
                            PricePerHour = 1.2,
                            PrioritySlot = false,
                            SocialDistanceFlag = false,
                            StatusId = 1
                        },
                        new
                        {
                            Id = 16,
                            HasVallet = false,
                            IsChargingAvailable = false,
                            IsCovered = false,
                            IsCovidFreeCertified = false,
                            IsOutside = false,
                            Locator = "C04",
                            PricePerHour = 1.2,
                            PrioritySlot = false,
                            SocialDistanceFlag = false,
                            StatusId = 1
                        },
                        new
                        {
                            Id = 17,
                            HasVallet = false,
                            IsChargingAvailable = false,
                            IsCovered = false,
                            IsCovidFreeCertified = false,
                            IsOutside = false,
                            Locator = "C05",
                            PricePerHour = 1.2,
                            PrioritySlot = false,
                            SocialDistanceFlag = false,
                            StatusId = 1
                        },
                        new
                        {
                            Id = 18,
                            HasVallet = false,
                            IsChargingAvailable = false,
                            IsCovered = false,
                            IsCovidFreeCertified = false,
                            IsOutside = false,
                            Locator = "C06",
                            PricePerHour = 1.2,
                            PrioritySlot = false,
                            SocialDistanceFlag = false,
                            StatusId = 1
                        });
                });

            modelBuilder.Entity("PaxAPI.Entities.Status", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Status");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Available"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Reserved"
                        });
                });

            modelBuilder.Entity("PaxAPI.Entities.VehicleType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("SlotId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SlotId");

                    b.ToTable("VehicleType");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "Motorcycles",
                            Name = "A"
                        },
                        new
                        {
                            Id = 2,
                            Description = "Cars",
                            Name = "B"
                        },
                        new
                        {
                            Id = 3,
                            Description = "Trucks",
                            Name = "C"
                        },
                        new
                        {
                            Id = 4,
                            Description = "With Trailer",
                            Name = "D"
                        });
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
                    b.HasOne("PaxAPI.Entities.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("PaxAPI.Entities.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PaxAPI.Entities.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("PaxAPI.Entities.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("PaxAPI.Entities.Reservation", b =>
                {
                    b.HasOne("PaxAPI.Entities.Slot", "Slot")
                        .WithMany()
                        .HasForeignKey("SlotId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PaxAPI.Entities.ApplicationUser", "ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("ApplicationUser");

                    b.Navigation("Slot");
                });

            modelBuilder.Entity("PaxAPI.Entities.Slot", b =>
                {
                    b.HasOne("PaxAPI.Entities.Status", "Status")
                        .WithMany()
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Status");
                });

            modelBuilder.Entity("PaxAPI.Entities.VehicleType", b =>
                {
                    b.HasOne("PaxAPI.Entities.Slot", null)
                        .WithMany("VehicleType")
                        .HasForeignKey("SlotId");
                });

            modelBuilder.Entity("PaxAPI.Entities.Slot", b =>
                {
                    b.Navigation("VehicleType");
                });
#pragma warning restore 612, 618
        }
    }
}
