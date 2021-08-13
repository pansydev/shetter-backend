﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using PansyDev.Shetter.Infrastructure.Data;

namespace PansyDev.Shetter.Infrastructure.Migrations
{
    [DbContext(typeof(ShetterDbContext))]
    [Migration("20210730140224_AddImagesToPostVersion")]
    partial class AddImagesToPostVersion
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.7")
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

                    b.Property<Guid?>("PostId")
                        .HasColumnType("uuid");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("text");

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
                        .WithMany()
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

            modelBuilder.Entity("PansyDev.Shetter.Domain.Aggregates.PostAggregate.PostVersion", b =>
                {
                    b.HasOne("PansyDev.Shetter.Domain.Aggregates.PostAggregate.Post", null)
                        .WithMany("PreviousVersions")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("PansyDev.Shetter.Domain.Aggregates.PostAggregate.Post", b =>
                {
                    b.Navigation("PreviousVersions");
                });
#pragma warning restore 612, 618
        }
    }
}
