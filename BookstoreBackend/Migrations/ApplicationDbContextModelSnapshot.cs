﻿// <auto-generated />
using BookstoreBackend.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BookstoreBackend.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BookstoreBackend.Models.Book", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Author")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NumberOfPages")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Books");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Author = "Author 1",
                            Description = "Test Book 1",
                            NumberOfPages = 101,
                            Title = "Test Book 1"
                        },
                        new
                        {
                            Id = 2,
                            Author = "Author 2",
                            Description = "Test Book 2",
                            NumberOfPages = 202,
                            Title = "Test Book 2"
                        },
                        new
                        {
                            Id = 3,
                            Author = "Author 3",
                            Description = "Test Book 3",
                            NumberOfPages = 303,
                            Title = "Test Book 3"
                        },
                        new
                        {
                            Id = 4,
                            Author = "Author 4",
                            Description = "Test Book 4",
                            NumberOfPages = 404,
                            Title = "Test Book 4"
                        },
                        new
                        {
                            Id = 5,
                            Author = "Author 5",
                            Description = "Test Book 5",
                            NumberOfPages = 505,
                            Title = "Test Book 5"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}