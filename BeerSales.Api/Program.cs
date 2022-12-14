using BeerSales.Api;
using BeerSales.Core.Beers.Queries.GetAllBreweriesWithBeers;
using BeerSales.Infrastructure.Data;
using BeerSales.Infrastructure.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using FluentValidation;
using BeerSales.Infrastructure.Repository;
using BeerSales.Infrastructure.Repository.Interface;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMediatR(typeof(GetAllBreweriesWithBeersQueryHandler).GetTypeInfo().Assembly);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.CustomSchemaIds(type => type.ToString());
});

var connectionString = builder.Configuration.GetConnectionString("BeerSaleDbContext");
builder.Services.AddSqlServer<BeerSaleDbContext>(connectionString);
builder.Services.AddScoped<IBeerSalesDbContext>(provider => provider.GetRequiredService<BeerSaleDbContext>());
builder.Services.AddValidatorsFromAssemblyContaining<Program>();

builder.Services.AddScoped<IBeerRepository, BeerRepository>();
builder.Services.AddScoped<IBreweryRepository, BreweryRepository>();
builder.Services.AddScoped<IDiscountRepository, DiscountRepository>();
builder.Services.AddScoped<IStockRepository, StockRepository>();
builder.Services.AddScoped<IWholesalerRepository, WholesalerRepository>();

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
