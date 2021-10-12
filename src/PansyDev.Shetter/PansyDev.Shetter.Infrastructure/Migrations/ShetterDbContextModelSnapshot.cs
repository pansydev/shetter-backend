﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using PansyDev.Shetter.Infrastructure.Data;

namespace PansyDev.Shetter.Infrastructure.Migrations
{
    [DbContext(typeof(ShetterDbContext))]
    partial class ShetterDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.8")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("PansyDev.Shetter.Domain.Aggregates.PostAggregate.Post", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("AuthorId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid>("CurrentVersionId")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("LastModificationTime")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.HasIndex("CurrentVersionId");

                    b.ToTable("Posts");
                });

            modelBuilder.Entity("PansyDev.Shetter.Domain.Aggregates.PostAggregate.PostLike", b =>
                {
                    b.Property<Guid>("PostId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("AuthorId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("PostId", "AuthorId");

                    b.HasIndex("AuthorId");

                    b.ToTable("PostLike");
                });

            modelBuilder.Entity("PansyDev.Shetter.Domain.Aggregates.PostAggregate.PostVersion", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Images")
                        .IsRequired()
                        .HasColumnType("jsonb");

                    b.Property<string>("OriginalText")
                        .HasColumnType("text");

                    b.Property<Guid?>("PostId")
                        .HasColumnType("uuid");

                    b.Property<string>("TextTokens")
                        .IsRequired()
                        .HasColumnType("jsonb");

                    b.HasKey("Id");

                    b.HasIndex("PostId");

                    b.ToTable("PostVersions");
                });

            modelBuilder.Entity("PansyDev.Shetter.Domain.Aggregates.PostAuthorAggregate.PostAuthor", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("AccountId")
                        .HasColumnType("uuid");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("PostAuthors");
                });

            modelBuilder.Entity("PansyDev.Shetter.Domain.Aggregates.PostAggregate.Post", b =>
                {
                    b.HasOne("PansyDev.Shetter.Domain.Aggregates.PostAuthorAggregate.PostAuthor", "Author")
                        .WithMany("Posts")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PansyDev.Shetter.Domain.Aggregates.PostAggregate.PostVersion", "CurrentVersion")
                        .WithMany()
                        .HasForeignKey("CurrentVersionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Author");

                    b.Navigation("CurrentVersion");
                });

            modelBuilder.Entity("PansyDev.Shetter.Domain.Aggregates.PostAggregate.PostLike", b =>
                {
                    b.HasOne("PansyDev.Shetter.Domain.Aggregates.PostAuthorAggregate.PostAuthor", "Author")
                        .WithMany()
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PansyDev.Shetter.Domain.Aggregates.PostAggregate.Post", null)
                        .WithMany("Likes")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Author");
                });

            modelBuilder.Entity("PansyDev.Shetter.Domain.Aggregates.PostAggregate.PostVersion", b =>
                {
                    b.HasOne("PansyDev.Shetter.Domain.Aggregates.PostAggregate.Post", null)
                        .WithMany("PreviousVersions")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("PansyDev.Shetter.Domain.Aggregates.PostAggregate.Post", b =>
                {
                    b.Navigation("Likes");

                    b.Navigation("PreviousVersions");
                });

            modelBuilder.Entity("PansyDev.Shetter.Domain.Aggregates.PostAuthorAggregate.PostAuthor", b =>
                {
                    b.Navigation("Posts");
                });
#pragma warning restore 612, 618
        }
    }
}
