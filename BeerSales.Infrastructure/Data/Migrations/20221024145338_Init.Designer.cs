﻿// <auto-generated />
using System;
using BeerSales.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BeerSales.Infrastructure.Migrations
{
    [DbContext(typeof(BeerSaleDbContext))]
    [Migration("20221024145338_Init")]
    partial class Init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("BeerSales.Domain.Entities.Beer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("AlcoholContent")
                        .HasColumnType("decimal(18,2)");

                    b.Property<Guid>("BreweryId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Currency")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("BreweryId");

                    b.ToTable("Beers");

                    b.HasData(
                        new
                        {
                            Id = new Guid("7f395fd9-0ca9-4ec9-9c93-87a491f139de"),
                            AlcoholContent = 6.6m,
                            BreweryId = new Guid("f1b76026-c4a7-4cb7-9b71-dda1e1df49f5"),
                            Currency = "EUR",
                            Name = "Leffe Blonde",
                            Price = 2.3m
                        },
                        new
                        {
                            Id = new Guid("5669de71-bd93-41ec-9ce6-d25846120ec9"),
                            AlcoholContent = 4.5m,
                            BreweryId = new Guid("64d3ad32-d588-41c9-af1e-6b79fc92592c"),
                            Currency = "EUR",
                            Name = "Heineken Silver",
                            Price = 1.5m
                        },
                        new
                        {
                            Id = new Guid("f42e81f1-5129-4dde-a333-de52ebacfbc1"),
                            AlcoholContent = 5.6m,
                            BreweryId = new Guid("844ee1af-f882-4b35-85b9-6916f288432c"),
                            Currency = "EUR",
                            Name = "Guinness Draught",
                            Price = 2.6m
                        });
                });

            modelBuilder.Entity("BeerSales.Domain.Entities.Brewery", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Breweries");

                    b.HasData(
                        new
                        {
                            Id = new Guid("f1b76026-c4a7-4cb7-9b71-dda1e1df49f5"),
                            Name = "Abbaye de Leffe"
                        },
                        new
                        {
                            Id = new Guid("64d3ad32-d588-41c9-af1e-6b79fc92592c"),
                            Name = "Heineken"
                        },
                        new
                        {
                            Id = new Guid("844ee1af-f882-4b35-85b9-6916f288432c"),
                            Name = "Gunniess"
                        });
                });

            modelBuilder.Entity("BeerSales.Domain.Entities.Discount", b =>
                {
                    b.Property<Guid>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("DiscountPercentage")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("TierFrom")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.ToTable("Discounts");

                    b.HasData(
                        new
                        {
                            id = new Guid("0babea85-7d1f-4d57-aa17-06906215d6ed"),
                            DiscountPercentage = 10m,
                            TierFrom = 11
                        },
                        new
                        {
                            id = new Guid("a56c5ce6-1892-4c4a-92ea-b4c705fcb737"),
                            DiscountPercentage = 20m,
                            TierFrom = 21
                        });
                });

            modelBuilder.Entity("BeerSales.Domain.Entities.Stock", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("BeerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<Guid>("WholesalerId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("BeerId");

                    b.HasIndex("WholesalerId");

                    b.ToTable("Stocks");

                    b.HasData(
                        new
                        {
                            Id = new Guid("d8ebd9e9-c83a-4c24-b264-22fa580ea1a0"),
                            BeerId = new Guid("7f395fd9-0ca9-4ec9-9c93-87a491f139de"),
                            Quantity = 100,
                            WholesalerId = new Guid("ce22239c-17af-4330-833d-3040f13773a9")
                        },
                        new
                        {
                            Id = new Guid("b9b7c9f5-43ed-42b7-8d62-9e6abf73405a"),
                            BeerId = new Guid("5669de71-bd93-41ec-9ce6-d25846120ec9"),
                            Quantity = 50,
                            WholesalerId = new Guid("ce22239c-17af-4330-833d-3040f13773a9")
                        },
                        new
                        {
                            Id = new Guid("a58c6e9c-7328-47fc-91c2-b5019d10e9fe"),
                            BeerId = new Guid("7f395fd9-0ca9-4ec9-9c93-87a491f139de"),
                            Quantity = 30,
                            WholesalerId = new Guid("f148a4c7-76c4-40dd-b829-b6ee3442756e")
                        },
                        new
                        {
                            Id = new Guid("1e4e36b4-6e0e-436b-bfa9-bddacd7e00de"),
                            BeerId = new Guid("5669de71-bd93-41ec-9ce6-d25846120ec9"),
                            Quantity = 200,
                            WholesalerId = new Guid("f148a4c7-76c4-40dd-b829-b6ee3442756e")
                        },
                        new
                        {
                            Id = new Guid("2afdf137-86ff-4ea3-a138-52a12b3d4fea"),
                            BeerId = new Guid("f42e81f1-5129-4dde-a333-de52ebacfbc1"),
                            Quantity = 70,
                            WholesalerId = new Guid("f148a4c7-76c4-40dd-b829-b6ee3442756e")
                        },
                        new
                        {
                            Id = new Guid("d7ce00e3-5225-41c2-854a-1ce71a02999b"),
                            BeerId = new Guid("7f395fd9-0ca9-4ec9-9c93-87a491f139de"),
                            Quantity = 300,
                            WholesalerId = new Guid("214d5662-1051-40c3-95af-926fc66a3032")
                        },
                        new
                        {
                            Id = new Guid("5c1a5bea-7eda-4a98-8c9e-43b8d6c80b8f"),
                            BeerId = new Guid("5669de71-bd93-41ec-9ce6-d25846120ec9"),
                            Quantity = 20,
                            WholesalerId = new Guid("214d5662-1051-40c3-95af-926fc66a3032")
                        },
                        new
                        {
                            Id = new Guid("46b9645d-d44f-4797-afeb-411c1eeaf933"),
                            BeerId = new Guid("f42e81f1-5129-4dde-a333-de52ebacfbc1"),
                            Quantity = 40,
                            WholesalerId = new Guid("214d5662-1051-40c3-95af-926fc66a3032")
                        });
                });

            modelBuilder.Entity("BeerSales.Domain.Entities.Wholesaler", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Wholesalers");

                    b.HasData(
                        new
                        {
                            Id = new Guid("ce22239c-17af-4330-833d-3040f13773a9"),
                            Name = "GeneDrinks"
                        },
                        new
                        {
                            Id = new Guid("f148a4c7-76c4-40dd-b829-b6ee3442756e"),
                            Name = "AllBeerSales"
                        },
                        new
                        {
                            Id = new Guid("214d5662-1051-40c3-95af-926fc66a3032"),
                            Name = "Forever Beer"
                        });
                });

            modelBuilder.Entity("BeerSales.Domain.Entities.Beer", b =>
                {
                    b.HasOne("BeerSales.Domain.Entities.Brewery", "Brewery")
                        .WithMany("Beers")
                        .HasForeignKey("BreweryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Brewery");
                });

            modelBuilder.Entity("BeerSales.Domain.Entities.Stock", b =>
                {
                    b.HasOne("BeerSales.Domain.Entities.Beer", "Beer")
                        .WithMany("Stocks")
                        .HasForeignKey("BeerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BeerSales.Domain.Entities.Wholesaler", "Wholesaler")
                        .WithMany("Stocks")
                        .HasForeignKey("WholesalerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Beer");

                    b.Navigation("Wholesaler");
                });

            modelBuilder.Entity("BeerSales.Domain.Entities.Beer", b =>
                {
                    b.Navigation("Stocks");
                });

            modelBuilder.Entity("BeerSales.Domain.Entities.Brewery", b =>
                {
                    b.Navigation("Beers");
                });

            modelBuilder.Entity("BeerSales.Domain.Entities.Wholesaler", b =>
                {
                    b.Navigation("Stocks");
                });
#pragma warning restore 612, 618
        }
    }
}