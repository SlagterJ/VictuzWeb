﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using VictuzWeb.Persistence;

#nullable disable

namespace VictuzWeb.Migrations
{
    [DbContext(typeof(VictuzWebDatabaseContext))]
    [Migration("20241105093936_AlsoAddRoleIdentifier")]
    partial class AlsoAddRoleIdentifier
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
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

                    b.ToTable("UserGathering");
                });

            modelBuilder.Entity("VictuzWeb.Models.Club", b =>
                {
                    b.Property<decimal>("Identifier")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("decimal(20,0)");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<decimal>("Identifier"));

                    b.Property<bool>("Accepted")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("CreatedAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("OwnerIdentifier")
                        .HasColumnType("decimal(20,0)");

                    b.HasKey("Identifier");

                    b.HasIndex("OwnerIdentifier");

                    b.ToTable("Clubs");
                });

            modelBuilder.Entity("VictuzWeb.Models.Role", b =>
                {
                    b.Property<decimal>("Identifier")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("decimal(20,0)");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<decimal>("Identifier"));

                    b.Property<DateTime?>("CreatedAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UsersWithRoleIdentifiers")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Identifier");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("VictuzWeb.Models.Suggestion", b =>
                {
                    b.Property<decimal>("Identifier")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("decimal(20,0)");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<decimal>("Identifier"));

                    b.Property<DateTime?>("CreatedAt")
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

                    b.ToTable("Suggestions");

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

                    b.Property<DateTime?>("CreatedAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime2");

                    b.Property<string>("Firstname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OwnerOfIdentifiers")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RegisteredForGatheringsIdentifiers")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("RoleIdentifier")
                        .HasColumnType("decimal(20,0)");

                    b.Property<string>("SuggestionsIdentifiers")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Identifier");

                    b.HasIndex("RoleIdentifier");

                    b.ToTable("Users");
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

                    b.Property<string>("RegisteredUsersIdentifiers")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

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
                        .WithMany("OwnerOf")
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
                    b.Navigation("OwnerOf");

                    b.Navigation("Suggestions");
                });
#pragma warning restore 612, 618
        }
    }
}
