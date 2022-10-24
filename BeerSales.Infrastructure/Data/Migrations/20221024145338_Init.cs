using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BeerSales.Infrastructure.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Breweries",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Breweries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Discounts",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TierFrom = table.Column<int>(type: "int", nullable: false),
                    DiscountPercentage = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Discounts", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Wholesalers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wholesalers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Beers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BreweryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AlcoholContent = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Currency = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Beers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Beers_Breweries_BreweryId",
                        column: x => x.BreweryId,
                        principalTable: "Breweries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Stocks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WholesalerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BeerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stocks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Stocks_Beers_BeerId",
                        column: x => x.BeerId,
                        principalTable: "Beers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Stocks_Wholesalers_WholesalerId",
                        column: x => x.WholesalerId,
                        principalTable: "Wholesalers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Breweries",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("64d3ad32-d588-41c9-af1e-6b79fc92592c"), "Heineken" },
                    { new Guid("844ee1af-f882-4b35-85b9-6916f288432c"), "Gunniess" },
                    { new Guid("f1b76026-c4a7-4cb7-9b71-dda1e1df49f5"), "Abbaye de Leffe" }
                });

            migrationBuilder.InsertData(
                table: "Discounts",
                columns: new[] { "id", "DiscountPercentage", "TierFrom" },
                values: new object[,]
                {
                    { new Guid("0babea85-7d1f-4d57-aa17-06906215d6ed"), 10m, 11 },
                    { new Guid("a56c5ce6-1892-4c4a-92ea-b4c705fcb737"), 20m, 21 }
                });

            migrationBuilder.InsertData(
                table: "Wholesalers",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("214d5662-1051-40c3-95af-926fc66a3032"), "Forever Beer" },
                    { new Guid("ce22239c-17af-4330-833d-3040f13773a9"), "GeneDrinks" },
                    { new Guid("f148a4c7-76c4-40dd-b829-b6ee3442756e"), "AllBeerSales" }
                });

            migrationBuilder.InsertData(
                table: "Beers",
                columns: new[] { "Id", "AlcoholContent", "BreweryId", "Currency", "Name", "Price" },
                values: new object[] { new Guid("5669de71-bd93-41ec-9ce6-d25846120ec9"), 4.5m, new Guid("64d3ad32-d588-41c9-af1e-6b79fc92592c"), "EUR", "Heineken Silver", 1.5m });

            migrationBuilder.InsertData(
                table: "Beers",
                columns: new[] { "Id", "AlcoholContent", "BreweryId", "Currency", "Name", "Price" },
                values: new object[] { new Guid("7f395fd9-0ca9-4ec9-9c93-87a491f139de"), 6.6m, new Guid("f1b76026-c4a7-4cb7-9b71-dda1e1df49f5"), "EUR", "Leffe Blonde", 2.3m });

            migrationBuilder.InsertData(
                table: "Beers",
                columns: new[] { "Id", "AlcoholContent", "BreweryId", "Currency", "Name", "Price" },
                values: new object[] { new Guid("f42e81f1-5129-4dde-a333-de52ebacfbc1"), 5.6m, new Guid("844ee1af-f882-4b35-85b9-6916f288432c"), "EUR", "Guinness Draught", 2.6m });

            migrationBuilder.InsertData(
                table: "Stocks",
                columns: new[] { "Id", "BeerId", "Quantity", "WholesalerId" },
                values: new object[,]
                {
                    { new Guid("1e4e36b4-6e0e-436b-bfa9-bddacd7e00de"), new Guid("5669de71-bd93-41ec-9ce6-d25846120ec9"), 200, new Guid("f148a4c7-76c4-40dd-b829-b6ee3442756e") },
                    { new Guid("2afdf137-86ff-4ea3-a138-52a12b3d4fea"), new Guid("f42e81f1-5129-4dde-a333-de52ebacfbc1"), 70, new Guid("f148a4c7-76c4-40dd-b829-b6ee3442756e") },
                    { new Guid("46b9645d-d44f-4797-afeb-411c1eeaf933"), new Guid("f42e81f1-5129-4dde-a333-de52ebacfbc1"), 40, new Guid("214d5662-1051-40c3-95af-926fc66a3032") },
                    { new Guid("5c1a5bea-7eda-4a98-8c9e-43b8d6c80b8f"), new Guid("5669de71-bd93-41ec-9ce6-d25846120ec9"), 20, new Guid("214d5662-1051-40c3-95af-926fc66a3032") },
                    { new Guid("a58c6e9c-7328-47fc-91c2-b5019d10e9fe"), new Guid("7f395fd9-0ca9-4ec9-9c93-87a491f139de"), 30, new Guid("f148a4c7-76c4-40dd-b829-b6ee3442756e") },
                    { new Guid("b9b7c9f5-43ed-42b7-8d62-9e6abf73405a"), new Guid("5669de71-bd93-41ec-9ce6-d25846120ec9"), 50, new Guid("ce22239c-17af-4330-833d-3040f13773a9") },
                    { new Guid("d7ce00e3-5225-41c2-854a-1ce71a02999b"), new Guid("7f395fd9-0ca9-4ec9-9c93-87a491f139de"), 300, new Guid("214d5662-1051-40c3-95af-926fc66a3032") },
                    { new Guid("d8ebd9e9-c83a-4c24-b264-22fa580ea1a0"), new Guid("7f395fd9-0ca9-4ec9-9c93-87a491f139de"), 100, new Guid("ce22239c-17af-4330-833d-3040f13773a9") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Beers_BreweryId",
                table: "Beers",
                column: "BreweryId");

            migrationBuilder.CreateIndex(
                name: "IX_Stocks_BeerId",
                table: "Stocks",
                column: "BeerId");

            migrationBuilder.CreateIndex(
                name: "IX_Stocks_WholesalerId",
                table: "Stocks",
                column: "WholesalerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Discounts");

            migrationBuilder.DropTable(
                name: "Stocks");

            migrationBuilder.DropTable(
                name: "Beers");

            migrationBuilder.DropTable(
                name: "Wholesalers");

            migrationBuilder.DropTable(
                name: "Breweries");
        }
    }
}
