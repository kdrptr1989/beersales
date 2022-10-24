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
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
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
                    DiscountPercentage = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
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
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
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
                    Currency = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
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
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
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
                columns: new[] { "Id", "CreatedDate", "ModifiedDate", "Name" },
                values: new object[,]
                {
                    { new Guid("2d066bf1-8cea-4933-8562-1ddf8bc77131"), new DateTime(2022, 10, 24, 16, 10, 0, 153, DateTimeKind.Utc).AddTicks(1266), null, "Gunniess" },
                    { new Guid("8e489942-f9ca-47a9-9781-df6c08a962cb"), new DateTime(2022, 10, 24, 16, 10, 0, 153, DateTimeKind.Utc).AddTicks(1260), null, "Abbaye de Leffe" },
                    { new Guid("ea11c974-a835-4161-9fd4-b4d40b6af996"), new DateTime(2022, 10, 24, 16, 10, 0, 153, DateTimeKind.Utc).AddTicks(1265), null, "Heineken" }
                });

            migrationBuilder.InsertData(
                table: "Discounts",
                columns: new[] { "id", "CreatedDate", "DiscountPercentage", "ModifiedDate", "TierFrom" },
                values: new object[,]
                {
                    { new Guid("28652824-28ba-4dde-8812-240d453b6fe8"), new DateTime(2022, 10, 24, 16, 10, 0, 153, DateTimeKind.Utc).AddTicks(2741), 10m, null, 11 },
                    { new Guid("a094d126-fb1e-4192-ae3e-b9c48bbf3602"), new DateTime(2022, 10, 24, 16, 10, 0, 153, DateTimeKind.Utc).AddTicks(2744), 20m, null, 21 }
                });

            migrationBuilder.InsertData(
                table: "Wholesalers",
                columns: new[] { "Id", "CreatedDate", "ModifiedDate", "Name" },
                values: new object[,]
                {
                    { new Guid("3b2b2649-0a7b-459b-9195-bdf04dc33cd5"), new DateTime(2022, 10, 24, 16, 10, 0, 153, DateTimeKind.Utc).AddTicks(2082), null, "GeneDrinks" },
                    { new Guid("3be8866e-da37-4cbb-9da8-f29317c53454"), new DateTime(2022, 10, 24, 16, 10, 0, 153, DateTimeKind.Utc).AddTicks(2083), null, "AllBeerSales" },
                    { new Guid("f401ed08-3908-4dc9-8354-2b616d713fec"), new DateTime(2022, 10, 24, 16, 10, 0, 153, DateTimeKind.Utc).AddTicks(2083), null, "Forever Beer" }
                });

            migrationBuilder.InsertData(
                table: "Beers",
                columns: new[] { "Id", "AlcoholContent", "BreweryId", "CreatedDate", "Currency", "ModifiedDate", "Name", "Price" },
                values: new object[] { new Guid("961fd882-f6e3-4444-ad5b-d80dc7aa80fa"), 5.6m, new Guid("2d066bf1-8cea-4933-8562-1ddf8bc77131"), new DateTime(2022, 10, 24, 16, 10, 0, 153, DateTimeKind.Utc).AddTicks(1767), "EUR", null, "Guinness Draught", 2.6m });

            migrationBuilder.InsertData(
                table: "Beers",
                columns: new[] { "Id", "AlcoholContent", "BreweryId", "CreatedDate", "Currency", "ModifiedDate", "Name", "Price" },
                values: new object[] { new Guid("9a458ff1-068c-42e4-8ecd-673d46a07c2f"), 6.6m, new Guid("8e489942-f9ca-47a9-9781-df6c08a962cb"), new DateTime(2022, 10, 24, 16, 10, 0, 153, DateTimeKind.Utc).AddTicks(1765), "EUR", null, "Leffe Blonde", 2.3m });

            migrationBuilder.InsertData(
                table: "Beers",
                columns: new[] { "Id", "AlcoholContent", "BreweryId", "CreatedDate", "Currency", "ModifiedDate", "Name", "Price" },
                values: new object[] { new Guid("b5f51183-6eaa-4533-9a97-992d0323e9bb"), 4.5m, new Guid("ea11c974-a835-4161-9fd4-b4d40b6af996"), new DateTime(2022, 10, 24, 16, 10, 0, 153, DateTimeKind.Utc).AddTicks(1766), "EUR", null, "Heineken Silver", 1.5m });

            migrationBuilder.InsertData(
                table: "Stocks",
                columns: new[] { "Id", "BeerId", "CreatedDate", "ModifiedDate", "Quantity", "WholesalerId" },
                values: new object[,]
                {
                    { new Guid("09fcb33a-8345-4e26-bd78-e63c4d152a3a"), new Guid("b5f51183-6eaa-4533-9a97-992d0323e9bb"), new DateTime(2022, 10, 24, 16, 10, 0, 153, DateTimeKind.Utc).AddTicks(2416), null, 200, new Guid("3be8866e-da37-4cbb-9da8-f29317c53454") },
                    { new Guid("435e9e7e-abd4-4d4a-8c29-208f81b386ec"), new Guid("9a458ff1-068c-42e4-8ecd-673d46a07c2f"), new DateTime(2022, 10, 24, 16, 10, 0, 153, DateTimeKind.Utc).AddTicks(2418), null, 300, new Guid("f401ed08-3908-4dc9-8354-2b616d713fec") },
                    { new Guid("49e67567-e1ed-4e9a-b085-693fdcff990b"), new Guid("961fd882-f6e3-4444-ad5b-d80dc7aa80fa"), new DateTime(2022, 10, 24, 16, 10, 0, 153, DateTimeKind.Utc).AddTicks(2423), null, 40, new Guid("f401ed08-3908-4dc9-8354-2b616d713fec") },
                    { new Guid("58d77151-46f5-4773-9740-8af1e15887e1"), new Guid("9a458ff1-068c-42e4-8ecd-673d46a07c2f"), new DateTime(2022, 10, 24, 16, 10, 0, 153, DateTimeKind.Utc).AddTicks(2412), null, 100, new Guid("3b2b2649-0a7b-459b-9195-bdf04dc33cd5") },
                    { new Guid("7e3d4de5-e9d0-4a0b-93cb-3d91c4319632"), new Guid("9a458ff1-068c-42e4-8ecd-673d46a07c2f"), new DateTime(2022, 10, 24, 16, 10, 0, 153, DateTimeKind.Utc).AddTicks(2415), null, 30, new Guid("3be8866e-da37-4cbb-9da8-f29317c53454") },
                    { new Guid("8ce844af-9eb9-4686-8591-25cfafe7eadf"), new Guid("b5f51183-6eaa-4533-9a97-992d0323e9bb"), new DateTime(2022, 10, 24, 16, 10, 0, 153, DateTimeKind.Utc).AddTicks(2414), null, 50, new Guid("3b2b2649-0a7b-459b-9195-bdf04dc33cd5") },
                    { new Guid("9bfe7874-e106-42b0-8301-6d09bd6f5d81"), new Guid("961fd882-f6e3-4444-ad5b-d80dc7aa80fa"), new DateTime(2022, 10, 24, 16, 10, 0, 153, DateTimeKind.Utc).AddTicks(2417), null, 70, new Guid("3be8866e-da37-4cbb-9da8-f29317c53454") },
                    { new Guid("e0ae1c8e-dc03-491a-b344-a55b8a44241a"), new Guid("b5f51183-6eaa-4533-9a97-992d0323e9bb"), new DateTime(2022, 10, 24, 16, 10, 0, 153, DateTimeKind.Utc).AddTicks(2422), null, 20, new Guid("f401ed08-3908-4dc9-8354-2b616d713fec") }
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
