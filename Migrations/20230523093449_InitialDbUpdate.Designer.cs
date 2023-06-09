﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TestApi.Persistence.Data;

#nullable disable

namespace TestApi.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20230523093449_InitialDbUpdate")]
    partial class InitialDbUpdate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("dbo")
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("TestApi.Application.Models.Customer", b =>
                {
                    b.Property<long>("customerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("customerId"));

                    b.Property<int?>("birthDate")
                        .HasColumnType("int");

                    b.Property<long?>("identityNo")
                        .HasColumnType("bigint");

                    b.Property<bool?>("identityVerified")
                        .HasColumnType("bit");

                    b.Property<string>("name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("status")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("surname")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("customerId");

                    b.ToTable("Customer", "dbo");
                });

            modelBuilder.Entity("TestApi.Application.Models.Transaction", b =>
                {
                    b.Property<long>("transactionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("transactionId"));

                    b.Property<string>("amount")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("cardPan")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("customerId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("orderId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("responseCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("responseMessage")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("status")
                        .HasColumnType("int");

                    b.Property<string>("typeId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("transactionId");

                    b.ToTable("Transaction", "dbo");
                });
#pragma warning restore 612, 618
        }
    }
}
