﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MusicWorld.Data;

namespace MusicWorld.Data.Migrations
{
    [DbContext(typeof(MusicWorldContext))]
    [Migration("20200725191728_Migra")]
    partial class Migra
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MusicWorld.Data.Models.Album", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ArtistId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(64)")
                        .HasMaxLength(64);

                    b.Property<DateTime>("ReleaseDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("ArtistId");

                    b.ToTable("Albums");
                });

            modelBuilder.Entity("MusicWorld.Data.Models.Artist", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(64)")
                        .HasMaxLength(64);

                    b.HasKey("Id");

                    b.ToTable("Artists");
                });

            modelBuilder.Entity("MusicWorld.Data.Models.Catalog", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(64)")
                        .HasMaxLength(64);

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Catalogs");
                });

            modelBuilder.Entity("MusicWorld.Data.Models.Song", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("AlbumId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ArtistId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CatalogId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<double>("Duration")
                        .HasColumnType("float");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AlbumId");

                    b.HasIndex("ArtistId");

                    b.HasIndex("CatalogId");

                    b.ToTable("Songs");
                });

            modelBuilder.Entity("MusicWorld.Data.Models.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsAdmin")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = "c2b43fad-0de9-4e30-94dd-9d8280f3e8b5",
                            FirstName = "Admin",
                            IsAdmin = true,
                            LastName = "Adminov",
                            Password = "123",
                            Username = "admin"
                        },
                        new
                        {
                            Id = "81c44422-e2bf-400d-bc76-a7485f985ac7",
                            FirstName = "User",
                            IsAdmin = false,
                            LastName = "Userov",
                            Password = "123",
                            Username = "user"
                        });
                });

            modelBuilder.Entity("MusicWorld.Data.Models.Album", b =>
                {
                    b.HasOne("MusicWorld.Data.Models.Artist", "Artist")
                        .WithMany("Albums")
                        .HasForeignKey("ArtistId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MusicWorld.Data.Models.Catalog", b =>
                {
                    b.HasOne("MusicWorld.Data.Models.User", "User")
                        .WithMany("Catalogs")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MusicWorld.Data.Models.Song", b =>
                {
                    b.HasOne("MusicWorld.Data.Models.Album", "Album")
                        .WithMany("Songs")
                        .HasForeignKey("AlbumId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("MusicWorld.Data.Models.Artist", "Artist")
                        .WithMany("Songs")
                        .HasForeignKey("ArtistId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("MusicWorld.Data.Models.Catalog", null)
                        .WithMany("Songs")
                        .HasForeignKey("CatalogId");
                });
#pragma warning restore 612, 618
        }
    }
}
