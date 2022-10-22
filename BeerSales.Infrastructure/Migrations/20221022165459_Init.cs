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
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TierFrom = table.Column<int>(type: "int", nullable: false),
                    TierTo = table.Column<int>(type: "int", nullable: false),
                    DiscountValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DiscountType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Discounts", x => x.Id);
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
                    { new Guid("14806c4b-8571-4f76-a47e-564ab3b2c3df"), "Gunniess" },
                    { new Guid("af8b997f-d49c-475d-8aaa-ca46ad922892"), "Heineken" },
                    { new Guid("e8a16727-5972-44f9-a812-0498ef378c2e"), "Abbaye de Leffe" }
                });

            migrationBuilder.InsertData(
                table: "Discounts",
                columns: new[] { "Id", "DiscountType", "DiscountValue", "TierFrom", "TierTo" },
                values: new object[,]
                {
                    { new Guid("4bb7d642-632b-4c31-8c42-3efdb0829d54"), 0, 10m, 11, 20 },
                    { new Guid("bdabd67a-ab3f-4158-bb88-e6ead30e76ad"), 0, 20m, 21, 29 }
                });

            migrationBuilder.InsertData(
                table: "Wholesalers",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("0de53695-0ce5-4b2f-ad8c-e7b60c9d8b28"), "AllBeerSales" },
                    { new Guid("6512a761-b061-48b0-a576-0f12f93b6f03"), "GeneDrinks" },
                    { new Guid("c8d5de6a-4c8a-4050-8850-aa14ca162712"), "Forever Beer" }
                });

            migrationBuilder.InsertData(
                table: "Beers",
                columns: new[] { "Id", "AlcoholContent", "BreweryId", "Currency", "Name", "Price" },
                values: new object[] { new Guid("40ea0b13-9729-4f40-a54f-25c77c9b550a"), 5.6m, new Guid("14806c4b-8571-4f76-a47e-564ab3b2c3df"), "EUR", "Guinness Draught", 2.6m });

            migrationBuilder.InsertData(
                table: "Beers",
                columns: new[] { "Id", "AlcoholContent", "BreweryId", "Currency", "Name", "Price" },
                values: new object[] { new Guid("87d8fa10-335b-4cd1-b9fb-fa601f35d478"), 4.5m, new Guid("af8b997f-d49c-475d-8aaa-ca46ad922892"), "EUR", "Heineken Silver", 1.5m });

            migrationBuilder.InsertData(
                table: "Beers",
                columns: new[] { "Id", "AlcoholContent", "BreweryId", "Currency", "Name", "Price" },
                values: new object[] { new Guid("d45a1fb6-571c-49a8-ab84-295673486c30"), 6.6m, new Guid("e8a16727-5972-44f9-a812-0498ef378c2e"), "EUR", "Leffe Blonde", 2.3m });

            migrationBuilder.InsertData(
                table: "Stocks",
                columns: new[] { "Id", "BeerId", "Quantity", "WholesalerId" },
                values: new object[,]
                {
                    { new Guid("464d6963-9a40-41cd-a22c-0a59daf94b9b"), new Guid("d45a1fb6-571c-49a8-ab84-295673486c30"), 100, new Guid("6512a761-b061-48b0-a576-0f12f93b6f03") },
                    { new Guid("4d358bdd-d88d-4913-8e00-290a0e476f5f"), new Guid("d45a1fb6-571c-49a8-ab84-295673486c30"), 300, new Guid("c8d5de6a-4c8a-4050-8850-aa14ca162712") },
                    { new Guid("4d974e72-c2f3-4df0-b1aa-ee275517cbec"), new Guid("87d8fa10-335b-4cd1-b9fb-fa601f35d478"), 20, new Guid("c8d5de6a-4c8a-4050-8850-aa14ca162712") },
                    { new Guid("57a016ee-4ca4-411a-8874-f9e2daf8b9b8"), new Guid("40ea0b13-9729-4f40-a54f-25c77c9b550a"), 70, new Guid("0de53695-0ce5-4b2f-ad8c-e7b60c9d8b28") },
                    { new Guid("66429f25-89cd-45cc-b5e1-b33c1953e2e5"), new Guid("40ea0b13-9729-4f40-a54f-25c77c9b550a"), 40, new Guid("c8d5de6a-4c8a-4050-8850-aa14ca162712") },
                    { new Guid("805cc87b-1842-46e0-89a4-9478d650acd5"), new Guid("87d8fa10-335b-4cd1-b9fb-fa601f35d478"), 200, new Guid("0de53695-0ce5-4b2f-ad8c-e7b60c9d8b28") },
                    { new Guid("834420f9-5176-489b-a3b7-825e79ad7b56"), new Guid("87d8fa10-335b-4cd1-b9fb-fa601f35d478"), 50, new Guid("6512a761-b061-48b0-a576-0f12f93b6f03") },
                    { new Guid("f0934390-38fe-45fc-bdfd-1b1cb8d15c71"), new Guid("d45a1fb6-571c-49a8-ab84-295673486c30"), 30, new Guid("0de53695-0ce5-4b2f-ad8c-e7b60c9d8b28") }
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
