﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Web.GameStoreMVC.Data;

#nullable disable

namespace Web.GameStoreMVC.Migrations
{
    [DbContext(typeof(GameStoreDbContext))]
    partial class GameStoreDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("GameGenre", b =>
                {
                    b.Property<Guid>("GamesId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("GenresId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("GamesId", "GenresId");

                    b.HasIndex("GenresId");

                    b.ToTable("GameGenre");
                });

            modelBuilder.Entity("GameLanguage", b =>
                {
                    b.Property<Guid>("GamesId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("LanguagesId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("GamesId", "LanguagesId");

                    b.HasIndex("LanguagesId");

                    b.ToTable("GameLanguage");
                });

            modelBuilder.Entity("GamePlatform", b =>
                {
                    b.Property<Guid>("GamesId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("PlatformsId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("GamesId", "PlatformsId");

                    b.HasIndex("PlatformsId");

                    b.ToTable("GamePlatform");
                });

            modelBuilder.Entity("Web.GameStoreMVC.Models.Domain.Game", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Developer")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("ShortDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SystemRequirements")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateOnly>("YearOfRelease")
                        .HasColumnType("date");

                    b.HasKey("Id");

                    b.ToTable("Games");
                });

            modelBuilder.Entity("Web.GameStoreMVC.Models.Domain.Genre", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Genres");
                });

            modelBuilder.Entity("Web.GameStoreMVC.Models.Domain.Language", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Languages");
                });

            modelBuilder.Entity("Web.GameStoreMVC.Models.Domain.Platform", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Platforms");
                });

            modelBuilder.Entity("GameGenre", b =>
                {
                    b.HasOne("Web.GameStoreMVC.Models.Domain.Game", null)
                        .WithMany()
                        .HasForeignKey("GamesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Web.GameStoreMVC.Models.Domain.Genre", null)
                        .WithMany()
                        .HasForeignKey("GenresId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("GameLanguage", b =>
                {
                    b.HasOne("Web.GameStoreMVC.Models.Domain.Game", null)
                        .WithMany()
                        .HasForeignKey("GamesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Web.GameStoreMVC.Models.Domain.Language", null)
                        .WithMany()
                        .HasForeignKey("LanguagesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("GamePlatform", b =>
                {
                    b.HasOne("Web.GameStoreMVC.Models.Domain.Game", null)
                        .WithMany()
                        .HasForeignKey("GamesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Web.GameStoreMVC.Models.Domain.Platform", null)
                        .WithMany()
                        .HasForeignKey("PlatformsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
