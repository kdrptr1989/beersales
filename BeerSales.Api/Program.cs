using BeerSales.Api;
using BeerSales.Core.Beers.Queries.GetAllBreweriesWithBeers;
using BeerSales.Infrastructure.Data;
using BeerSales.Infrastructure.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMediatR(typeof(GetAllBreweriesWithBeersQueryHandler).GetTypeInfo().Assembly);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.CustomSchemaIds(type => type.ToString());
});

var connectionString = builder.Configuration.GetConnectionString("BeerSaleDbContext");

builder.Services.AddDbContext<BeerSaleDbContext>(options =>
              options.UseSqlServer(connectionString,
                  builder => builder.MigrationsAssembly(typeof(BeerSaleDbContext).Assembly.FullName)));

builder.Services.AddScoped<IBeerSalesDbContext>(provider => provider.GetRequiredService<BeerSaleDbContext>());

var app = builder.Build();
app.UseWeb(builder.Configuration);
app.UseHttpsRedirection();

await EnsureDbAsync(app.Services);

app.Run();

static async Task EnsureDbAsync(IServiceProvider sp)
{
    await using var db = sp.CreateScope().ServiceProvider.GetRequiredService<BeerSaleDbContext>();
    await db.Database.MigrateAsync();
}
