﻿// <auto-generated />
using System;
using EFCoreTest.Models.second_dbfirstTry;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace EFCoreTest.Migrations.Employee_BookDB
{
    [DbContext(typeof(Employee_BookDBContext))]
    [Migration("20250130160024_initial_Postgres_migration")]
    partial class initial_Postgres_migration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("EFCoreTest.Models.second_dbfirstTry.Author", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Authors");
                });

            modelBuilder.Entity("EFCoreTest.Models.second_dbfirstTry.Book", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int?>("AuthorId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<int>("LanguageId")
                        .HasColumnType("integer");

                    b.Property<int>("NoOfPages")
                        .HasColumnType("integer");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "AuthorId" }, "IX_Books_AuthorId");

                    b.HasIndex(new[] { "LanguageId" }, "IX_Books_LanguageId");

                    b.ToTable("Books");
                });

            modelBuilder.Entity("EFCoreTest.Models.second_dbfirstTry.BookPrice", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("Amount")
                        .HasColumnType("integer");

                    b.Property<int>("BookId")
                        .HasColumnType("integer");

                    b.Property<int>("CurrencyId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "BookId" }, "IX_BookPrices_BookId");

                    b.HasIndex(new[] { "CurrencyId" }, "IX_BookPrices_CurrencyId");

                    b.ToTable("BookPrices");
                });

            modelBuilder.Entity("EFCoreTest.Models.second_dbfirstTry.Currency", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Currencies");
                });

            modelBuilder.Entity("EFCoreTest.Models.second_dbfirstTry.Language", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Languages");
                });

            modelBuilder.Entity("EFCoreTest.Models.second_dbfirstTry.Book", b =>
                {
                    b.HasOne("EFCoreTest.Models.second_dbfirstTry.Author", "Author")
                        .WithMany("Books")
                        .HasForeignKey("AuthorId");

                    b.HasOne("EFCoreTest.Models.second_dbfirstTry.Language", "Language")
                        .WithMany("Books")
                        .HasForeignKey("LanguageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Author");

                    b.Navigation("Language");
                });

            modelBuilder.Entity("EFCoreTest.Models.second_dbfirstTry.BookPrice", b =>
                {
                    b.HasOne("EFCoreTest.Models.second_dbfirstTry.Book", "Book")
                        .WithMany("BookPrices")
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EFCoreTest.Models.second_dbfirstTry.Currency", "Currency")
                        .WithMany("BookPrices")
                        .HasForeignKey("CurrencyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Book");

                    b.Navigation("Currency");
                });

            modelBuilder.Entity("EFCoreTest.Models.second_dbfirstTry.Author", b =>
                {
                    b.Navigation("Books");
                });

            modelBuilder.Entity("EFCoreTest.Models.second_dbfirstTry.Book", b =>
                {
                    b.Navigation("BookPrices");
                });

            modelBuilder.Entity("EFCoreTest.Models.second_dbfirstTry.Currency", b =>
                {
                    b.Navigation("BookPrices");
                });

            modelBuilder.Entity("EFCoreTest.Models.second_dbfirstTry.Language", b =>
                {
                    b.Navigation("Books");
                });
#pragma warning restore 612, 618
        }
    }
}
