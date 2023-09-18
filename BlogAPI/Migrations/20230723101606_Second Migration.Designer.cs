﻿// <auto-generated />
using System;
using BlogAPI.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BlogAPI.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20230723101606_Second Migration")]
    partial class SecondMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("BlogAPI.Models.Blog", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("author")
                        .HasColumnType("longtext");

                    b.Property<string>("content")
                        .HasColumnType("longtext");

                    b.Property<string>("imageUrl")
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("publishDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("summary")
                        .HasColumnType("longtext");

                    b.Property<string>("title")
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("updatedDate")
                        .HasColumnType("datetime(6)");

                    b.HasKey("id");

                    b.ToTable("Blog");
                });

            modelBuilder.Entity("BlogAPI.Models.User", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("email")
                        .HasColumnType("longtext");

                    b.Property<string>("firstName")
                        .HasColumnType("longtext");

                    b.Property<string>("lastName")
                        .HasColumnType("longtext");

                    b.Property<string>("password")
                        .HasColumnType("longtext");

                    b.Property<string>("role")
                        .HasColumnType("longtext");

                    b.Property<string>("token")
                        .HasColumnType("longtext");

                    b.Property<string>("username")
                        .HasColumnType("longtext");

                    b.HasKey("id");

                    b.ToTable("users", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}
