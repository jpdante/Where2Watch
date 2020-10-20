﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Where2Watch;

namespace Where2Watch.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    partial class DatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Where2Watch.Models.Account", b =>
                {
                    b.Property<long>("Id")
                        .HasColumnType("bigint");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4")
                        .HasMaxLength(255);

                    b.Property<string>("Guid")
                        .IsRequired()
                        .HasColumnType("varchar(32) CHARACTER SET utf8mb4")
                        .HasMaxLength(32);

                    b.Property<DateTime>("LastAccess")
                        .HasColumnType("TIMESTAMP");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("varchar(32) CHARACTER SET utf8mb4")
                        .HasMaxLength(32);

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("Guid")
                        .IsUnique();

                    b.HasIndex("Username")
                        .IsUnique();

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("Where2Watch.Models.Episode", b =>
                {
                    b.Property<long>("Id")
                        .HasColumnType("bigint");

                    b.Property<int>("Number")
                        .HasColumnType("int");

                    b.Property<string>("OriginalName")
                        .IsRequired()
                        .HasColumnType("varchar(256) CHARACTER SET utf8mb4")
                        .HasMaxLength(256);

                    b.Property<long>("SeasonId")
                        .HasColumnType("bigint");

                    b.Property<long>("TitleId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("SeasonId");

                    b.HasIndex("TitleId");

                    b.ToTable("Episodes");
                });

            modelBuilder.Entity("Where2Watch.Models.EpisodeName", b =>
                {
                    b.Property<long>("Id")
                        .HasColumnType("bigint");

                    b.Property<int>("Country")
                        .HasColumnType("int");

                    b.Property<long>("EpisodeId")
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(256) CHARACTER SET utf8mb4")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("Country");

                    b.HasIndex("EpisodeId");

                    b.ToTable("EpisodeNames");
                });

            modelBuilder.Entity("Where2Watch.Models.Platform", b =>
                {
                    b.Property<long>("Id")
                        .HasColumnType("bigint");

                    b.Property<int>("Country")
                        .HasColumnType("int");

                    b.Property<string>("Icon")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Link")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(128) CHARACTER SET utf8mb4")
                        .HasMaxLength(128);

                    b.HasKey("Id");

                    b.HasIndex("Country");

                    b.ToTable("Platforms");
                });

            modelBuilder.Entity("Where2Watch.Models.Season", b =>
                {
                    b.Property<long>("Id")
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .HasColumnType("varchar(256) CHARACTER SET utf8mb4")
                        .HasMaxLength(256);

                    b.Property<int>("Number")
                        .HasColumnType("int");

                    b.Property<long>("TitleId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("TitleId");

                    b.ToTable("Seasons");
                });

            modelBuilder.Entity("Where2Watch.Models.Title", b =>
                {
                    b.Property<long>("Id")
                        .HasColumnType("bigint");

                    b.Property<uint>("Dislikes")
                        .HasColumnType("int unsigned");

                    b.Property<int>("Episodes")
                        .HasColumnType("int");

                    b.Property<int>("Length")
                        .HasColumnType("int");

                    b.Property<uint>("Likes")
                        .HasColumnType("int unsigned");

                    b.Property<string>("OriginalName")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Poster")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("Seasons")
                        .HasColumnType("int");

                    b.Property<string>("Summary")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Titles");
                });

            modelBuilder.Entity("Where2Watch.Models.TitleAvailability", b =>
                {
                    b.Property<long>("Id")
                        .HasColumnType("bigint");

                    b.Property<int>("Country")
                        .HasColumnType("int");

                    b.Property<string>("Link")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<long>("PlatformId")
                        .HasColumnType("bigint");

                    b.Property<long>("TitleId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("Country");

                    b.HasIndex("PlatformId");

                    b.HasIndex("TitleId");

                    b.ToTable("TitleAvailabilities");
                });

            modelBuilder.Entity("Where2Watch.Models.TitleLike", b =>
                {
                    b.Property<long>("AccountId")
                        .HasColumnType("bigint");

                    b.Property<long>("TitleId")
                        .HasColumnType("bigint");

                    b.Property<bool?>("IsLike")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime>("LastUpdate")
                        .HasColumnType("TIMESTAMP");

                    b.HasKey("AccountId", "TitleId");

                    b.HasIndex("AccountId");

                    b.HasIndex("TitleId");

                    b.ToTable("TitleLikes");
                });

            modelBuilder.Entity("Where2Watch.Models.TitleName", b =>
                {
                    b.Property<long>("Id")
                        .HasColumnType("bigint");

                    b.Property<int>("Country")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<long>("TitleId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("Country");

                    b.HasIndex("TitleId");

                    b.ToTable("TitleNames");
                });

            modelBuilder.Entity("Where2Watch.Models.Episode", b =>
                {
                    b.HasOne("Where2Watch.Models.Season", "Season")
                        .WithMany()
                        .HasForeignKey("SeasonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Where2Watch.Models.Title", "Title")
                        .WithMany()
                        .HasForeignKey("TitleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Where2Watch.Models.EpisodeName", b =>
                {
                    b.HasOne("Where2Watch.Models.Episode", "Episode")
                        .WithMany()
                        .HasForeignKey("EpisodeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Where2Watch.Models.Season", b =>
                {
                    b.HasOne("Where2Watch.Models.Title", "Title")
                        .WithMany()
                        .HasForeignKey("TitleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Where2Watch.Models.TitleAvailability", b =>
                {
                    b.HasOne("Where2Watch.Models.Platform", "Platform")
                        .WithMany()
                        .HasForeignKey("PlatformId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Where2Watch.Models.Title", "Title")
                        .WithMany()
                        .HasForeignKey("TitleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Where2Watch.Models.TitleLike", b =>
                {
                    b.HasOne("Where2Watch.Models.Account", "Account")
                        .WithMany()
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Where2Watch.Models.Title", "Title")
                        .WithMany()
                        .HasForeignKey("TitleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Where2Watch.Models.TitleName", b =>
                {
                    b.HasOne("Where2Watch.Models.Title", "Title")
                        .WithMany()
                        .HasForeignKey("TitleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
