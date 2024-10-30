﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using VictuzWeb.Persistence;

#nullable disable

namespace VictuzWeb.Migrations
{
    [DbContext(typeof(VictuzWebDatabaseContext))]
    partial class VictuzWebDatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("UserGathering", b =>
                {
                    b.Property<decimal>("GatheringIdentifier")
                        .HasColumnType("decimal(20,0)");

                    b.Property<decimal>("UsersIdentifier")
                        .HasColumnType("decimal(20,0)");

                    b.HasKey("GatheringIdentifier", "UsersIdentifier");

                    b.HasIndex("UsersIdentifier");

                    b.ToTable("UserGathering", (string)null);
                });

            modelBuilder.Entity("VictuzWeb.Models.Club", b =>
                {
                    b.Property<decimal>("Identifier")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("decimal(20,0)");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<decimal>("Identifier"));

                    b.Property<bool>("Accepted")
                        .HasColumnType("bit");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("OwnerIdentifier")
                        .HasColumnType("decimal(20,0)");

                    b.HasKey("Identifier");

                    b.HasIndex("OwnerIdentifier");

                    b.ToTable("Clubs", (string)null);
                });

            modelBuilder.Entity("VictuzWeb.Models.Role", b =>
                {
                    b.Property<decimal>("Identifier")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("decimal(20,0)");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<decimal>("Identifier"));

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Identifier");

                    b.ToTable("Roles", (string)null);
                });

            modelBuilder.Entity("VictuzWeb.Models.Suggestion", b =>
                {
                    b.Property<decimal>("Identifier")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("decimal(20,0)");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<decimal>("Identifier"));

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasMaxLength(13)
                        .HasColumnType("nvarchar(13)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("SuggestedByIdentifier")
                        .HasColumnType("decimal(20,0)");

                    b.HasKey("Identifier");

                    b.HasIndex("SuggestedByIdentifier");

                    b.ToTable("Suggestions", (string)null);

                    b.HasDiscriminator().HasValue("Suggestion");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("VictuzWeb.Models.User", b =>
                {
                    b.Property<decimal>("Identifier")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("decimal(20,0)");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<decimal>("Identifier"));

                    b.Property<DateOnly>("BirthDate")
                        .HasColumnType("date");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime2");

                    b.Property<string>("Firstname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("RoleIdentifier")
                        .HasColumnType("decimal(20,0)");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Identifier");

                    b.HasIndex("RoleIdentifier");

                    b.ToTable("Users", (string)null);
                });

            modelBuilder.Entity("VictuzWeb.Models.Gathering", b =>
                {
                    b.HasBaseType("VictuzWeb.Models.Suggestion");

                    b.Property<DateTime>("BeginDateTime")
                        .HasColumnType("datetime2");

                    b.Property<DateOnly>("DeadlineDate")
                        .HasColumnType("date");

                    b.Property<DateTime>("EndDateTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("MaxUsers")
                        .HasColumnType("int");

                    b.HasDiscriminator().HasValue("Gathering");
                });

            modelBuilder.Entity("UserGathering", b =>
                {
                    b.HasOne("VictuzWeb.Models.Gathering", null)
                        .WithMany()
                        .HasForeignKey("GatheringIdentifier")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("VictuzWeb.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UsersIdentifier")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("VictuzWeb.Models.Club", b =>
                {
                    b.HasOne("VictuzWeb.Models.User", "Owner")
                        .WithMany()
                        .HasForeignKey("OwnerIdentifier")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("VictuzWeb.Models.Suggestion", b =>
                {
                    b.HasOne("VictuzWeb.Models.User", "SuggestedBy")
                        .WithMany("Suggestions")
                        .HasForeignKey("SuggestedByIdentifier")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("SuggestedBy");
                });

            modelBuilder.Entity("VictuzWeb.Models.User", b =>
                {
                    b.HasOne("VictuzWeb.Models.Role", "Role")
                        .WithMany("UsersWithRole")
                        .HasForeignKey("RoleIdentifier")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("VictuzWeb.Models.Role", b =>
                {
                    b.Navigation("UsersWithRole");
                });

            modelBuilder.Entity("VictuzWeb.Models.User", b =>
                {
                    b.Navigation("Suggestions");
                });
#pragma warning restore 612, 618
        }
    }
}
