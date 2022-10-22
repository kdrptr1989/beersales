using BeerSales.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("BeerSaleDbContext");
builder.Services.AddSqlServer<BeerSaleDbContext>(connectionString);

var app = builder.Build();

await EnsureDbAsync(app.Services);

app.MapGet("/", () => "BeerSales Api");

app.Run();

static async Task EnsureDbAsync(IServiceProvider sp)
{
    await using var db = sp.CreateScope().ServiceProvider.GetRequiredService<BeerSaleDbContext>();
    await db.Database.MigrateAsync();
}
