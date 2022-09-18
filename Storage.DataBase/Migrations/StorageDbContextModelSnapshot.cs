﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Storage.DataBase.DataContext;

#nullable disable

namespace Storage.DataBase.Migrations
{
    [DbContext(typeof(StorageDbContext))]
    partial class StorageDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Storage.Core.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("Height")
                        .HasColumnType("integer");

                    b.Property<string>("ImageUrl")
                        .HasMaxLength(250)
                        .HasColumnType("character varying(250)");

                    b.Property<int>("Length")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric");

                    b.Property<int?>("ProductCategoryId")
                        .HasColumnType("integer");

                    b.Property<double>("Weight")
                        .HasColumnType("double precision");

                    b.Property<int>("Width")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ProductCategoryId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("Storage.Core.Models.ProductCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("character varying(25)");

                    b.HasKey("Id");

                    b.ToTable("ProductCategories");
                });

            modelBuilder.Entity("Storage.Core.Models.Storage.Area", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("character varying(15)");

                    b.HasKey("Id");

                    b.ToTable("Areas");
                });

            modelBuilder.Entity("Storage.Core.Models.Storage.Cell", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("AreaId")
                        .HasColumnType("integer");

                    b.Property<int?>("CellTypeId")
                        .HasColumnType("integer");

                    b.Property<int>("SubAreaHeightY")
                        .HasColumnType("integer");

                    b.Property<int>("SubAreaId")
                        .HasColumnType("integer");

                    b.Property<int>("SubAreaLenghtX")
                        .HasColumnType("integer");

                    b.Property<int>("SubAreaWidthZ")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("AreaId");

                    b.HasIndex("CellTypeId");

                    b.HasIndex("SubAreaId");

                    b.ToTable("Cells");
                });

            modelBuilder.Entity("Storage.Core.Models.Storage.CellType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("Height")
                        .HasColumnType("integer");

                    b.Property<int>("Length")
                        .HasColumnType("integer");

                    b.Property<double>("MaxWeight")
                        .HasColumnType("double precision");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("character varying(25)");

                    b.Property<int>("Width")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("CellTypes");
                });

            modelBuilder.Entity("Storage.Core.Models.Storage.StorageItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int?>("CellId")
                        .HasColumnType("integer");

                    b.Property<int>("ProductId")
                        .HasColumnType("integer");

                    b.Property<int?>("StateId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("CellId");

                    b.HasIndex("ProductId");

                    b.HasIndex("StateId");

                    b.ToTable("AllItems");
                });

            modelBuilder.Entity("Storage.Core.Models.Storage.StorageItemState", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasKey("Id");

                    b.ToTable("ItemsState");
                });

            modelBuilder.Entity("Storage.Core.Models.Storage.SubArea", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("AreaId")
                        .HasColumnType("integer");

                    b.Property<int>("HeightCells")
                        .HasColumnType("integer");

                    b.Property<int>("LengthCells")
                        .HasColumnType("integer");

                    b.Property<int>("NoOfSubArea")
                        .HasColumnType("integer");

                    b.Property<int>("WidthCells")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("AreaId");

                    b.ToTable("SubAreas");
                });

            modelBuilder.Entity("Storage.Core.Models.Product", b =>
                {
                    b.HasOne("Storage.Core.Models.ProductCategory", "ProductCategory")
                        .WithMany("Products")
                        .HasForeignKey("ProductCategoryId");

                    b.Navigation("ProductCategory");
                });

            modelBuilder.Entity("Storage.Core.Models.Storage.Cell", b =>
                {
                    b.HasOne("Storage.Core.Models.Storage.Area", "Area")
                        .WithMany("Cells")
                        .HasForeignKey("AreaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Storage.Core.Models.Storage.CellType", "CellType")
                        .WithMany("Cells")
                        .HasForeignKey("CellTypeId");

                    b.HasOne("Storage.Core.Models.Storage.SubArea", "SubArea")
                        .WithMany("Cells")
                        .HasForeignKey("SubAreaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Area");

                    b.Navigation("CellType");

                    b.Navigation("SubArea");
                });

            modelBuilder.Entity("Storage.Core.Models.Storage.StorageItem", b =>
                {
                    b.HasOne("Storage.Core.Models.Storage.Cell", "Cell")
                        .WithMany("Items")
                        .HasForeignKey("CellId");

                    b.HasOne("Storage.Core.Models.Product", "Product")
                        .WithMany("Items")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Storage.Core.Models.Storage.StorageItemState", "State")
                        .WithMany("Items")
                        .HasForeignKey("StateId");

                    b.Navigation("Cell");

                    b.Navigation("Product");

                    b.Navigation("State");
                });

            modelBuilder.Entity("Storage.Core.Models.Storage.SubArea", b =>
                {
                    b.HasOne("Storage.Core.Models.Storage.Area", "Area")
                        .WithMany("SubAreas")
                        .HasForeignKey("AreaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Area");
                });

            modelBuilder.Entity("Storage.Core.Models.Product", b =>
                {
                    b.Navigation("Items");
                });

            modelBuilder.Entity("Storage.Core.Models.ProductCategory", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("Storage.Core.Models.Storage.Area", b =>
                {
                    b.Navigation("Cells");

                    b.Navigation("SubAreas");
                });

            modelBuilder.Entity("Storage.Core.Models.Storage.Cell", b =>
                {
                    b.Navigation("Items");
                });

            modelBuilder.Entity("Storage.Core.Models.Storage.CellType", b =>
                {
                    b.Navigation("Cells");
                });

            modelBuilder.Entity("Storage.Core.Models.Storage.StorageItemState", b =>
                {
                    b.Navigation("Items");
                });

            modelBuilder.Entity("Storage.Core.Models.Storage.SubArea", b =>
                {
                    b.Navigation("Cells");
                });
#pragma warning restore 612, 618
        }
    }
}
