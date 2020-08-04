﻿// <auto-generated />
using System;
using DrinkFinder.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DrinkFinder.Infrastructure.Persistence.Migrations
{
    [DbContext(typeof(DrinkFinderContext))]
    partial class DrinkFinderContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("Domain")
                .HasAnnotation("ProductVersion", "3.1.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DrinkFinder.Infrastructure.Persistence.Entities.BusinessHours", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("AddedDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<TimeSpan?>("ClosingHour")
                        .HasColumnType("time");

                    b.Property<int>("Day")
                        .HasColumnType("int");

                    b.Property<Guid?>("EstablishmentId")
                        .IsRequired()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("ModifiedDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<TimeSpan?>("OpeningHour")
                        .HasColumnType("time");

                    b.HasKey("Id");

                    b.HasIndex("EstablishmentId");

                    b.ToTable("BusinessHours");
                });

            modelBuilder.Entity("DrinkFinder.Infrastructure.Persistence.Entities.Establishment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("AddedDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Banner")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Logo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset?>("ModifiedDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ShortCode")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("VATNumber")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Website")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ShortCode")
                        .IsUnique()
                        .HasFilter("[ShortCode] IS NOT NULL");

                    b.HasIndex("UserId");

                    b.HasIndex("VATNumber")
                        .IsUnique()
                        .HasFilter("[VATNumber] IS NOT NULL");

                    b.ToTable("Establishment");
                });

            modelBuilder.Entity("DrinkFinder.Infrastructure.Persistence.Entities.News", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("AddedDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Banner")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Content")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("EstablishmentId")
                        .IsRequired()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("ModifiedDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("EstablishmentId");

                    b.HasIndex("UserId");

                    b.ToTable("News");
                });

            modelBuilder.Entity("DrinkFinder.Infrastructure.Persistence.Entities.Picture", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("AddedDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<Guid?>("EstablishmentId")
                        .IsRequired()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Location")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTimeOffset?>("ModifiedDate")
                        .HasColumnType("datetimeoffset");

                    b.HasKey("Id");

                    b.HasIndex("EstablishmentId");

                    b.HasIndex("Location")
                        .IsUnique()
                        .HasFilter("[Location] IS NOT NULL");

                    b.ToTable("Picture");
                });

            modelBuilder.Entity("DrinkFinder.Infrastructure.Persistence.Entities.BusinessHours", b =>
                {
                    b.HasOne("DrinkFinder.Infrastructure.Persistence.Entities.Establishment", "Establishment")
                        .WithMany("BusinessHours")
                        .HasForeignKey("EstablishmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DrinkFinder.Infrastructure.Persistence.Entities.Establishment", b =>
                {
                    b.OwnsOne("DrinkFinder.Common.ValueObjects.Address", "Address", b1 =>
                        {
                            b1.Property<Guid>("EstablishmentId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("BoxNumber")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("City")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("Country")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("PostalCode")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("Street")
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("EstablishmentId");

                            b1.ToTable("Establishment");

                            b1.WithOwner()
                                .HasForeignKey("EstablishmentId");
                        });

                    b.OwnsOne("DrinkFinder.Common.ValueObjects.ContactInfo", "ContactInfo", b1 =>
                        {
                            b1.Property<Guid>("EstablishmentId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("PhoneNumber")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("ProfessionalEmail")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("PublicEmail")
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("EstablishmentId");

                            b1.ToTable("Establishment");

                            b1.WithOwner()
                                .HasForeignKey("EstablishmentId");
                        });

                    b.OwnsOne("DrinkFinder.Common.ValueObjects.Socials", "Socials", b1 =>
                        {
                            b1.Property<Guid>("EstablishmentId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Facebook")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("Instagram")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("LinkedIn")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("Twitter")
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("EstablishmentId");

                            b1.ToTable("Establishment");

                            b1.WithOwner()
                                .HasForeignKey("EstablishmentId");
                        });
                });

            modelBuilder.Entity("DrinkFinder.Infrastructure.Persistence.Entities.News", b =>
                {
                    b.HasOne("DrinkFinder.Infrastructure.Persistence.Entities.Establishment", "Establishment")
                        .WithMany("News")
                        .HasForeignKey("EstablishmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DrinkFinder.Infrastructure.Persistence.Entities.Picture", b =>
                {
                    b.HasOne("DrinkFinder.Infrastructure.Persistence.Entities.Establishment", "Establishment")
                        .WithMany("Pictures")
                        .HasForeignKey("EstablishmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
