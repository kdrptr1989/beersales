using BeerSales.Domain.Models;

namespace BeerSales.Infrastructure
{
    internal static class Seed
    {
        #region Breweries

        static readonly Guid abbayeDeLeffeBreweryId = Guid.NewGuid();
        static readonly Guid heinekenBreweryId = Guid.NewGuid();
        static readonly Guid guinessBreweryId = Guid.NewGuid();

        internal static Brewery[] Breweries =
        {
            new (abbayeDeLeffeBreweryId , "Abbaye de Leffe"),
            new (heinekenBreweryId , "Heineken"),
            new (guinessBreweryId , "Gunniess")
        };

        #endregion

        #region Beers

        static readonly Guid leffeBlondeId = Guid.NewGuid();
        static readonly Guid heinekenSilverId = Guid.NewGuid();
        static readonly Guid guinessDraughtId = Guid.NewGuid();

        internal static Beer[] Beers =
       {
            new (leffeBlondeId, abbayeDeLeffeBreweryId ,"Leffe Blonde", 2.3m, 6.6m, "EUR"),
            new (heinekenSilverId, heinekenBreweryId, "Heineken Silver", 1.5m, 4.5m, "EUR"),
            new (guinessDraughtId, guinessBreweryId, "Guinness Draught", 2.6m, 5.6m, "EUR"),
        };

        #endregion

        #region Wholesalers

        static readonly Guid geneDrinksId = Guid.NewGuid();
        static readonly Guid allBeerSalesId = Guid.NewGuid();
        static readonly Guid foreverBeerId = Guid.NewGuid();

        internal static Wholesaler[] Wholesalers =
        {
            new (geneDrinksId, "GeneDrinks"),
            new (allBeerSalesId, "AllBeerSales"),
            new ( foreverBeerId, "Forever Beer")
        };

        #endregion

        #region Stocks

        internal static Stock[] Stocks =
        {
            new ( Guid.NewGuid(), geneDrinksId, leffeBlondeId, 100),
            new ( Guid.NewGuid(), geneDrinksId, heinekenSilverId, 50),

            new ( Guid.NewGuid(), allBeerSalesId, leffeBlondeId, 30),
            new ( Guid.NewGuid(), allBeerSalesId, heinekenSilverId, 200),
            new ( Guid.NewGuid(), allBeerSalesId, guinessDraughtId, 70),

            new ( Guid.NewGuid(), foreverBeerId, leffeBlondeId, 300),
            new ( Guid.NewGuid(), foreverBeerId, heinekenSilverId, 20),
            new ( Guid.NewGuid(), foreverBeerId, guinessDraughtId, 40)
        };

        #endregion

        #region Discounts

        internal static Discount[] Discounts =
        {
            new ( Guid.NewGuid(), 11, 20, 10, 0 ),
            new ( Guid.NewGuid(), 21, 29, 20, 0 )
        };

        #endregion

    }
}
