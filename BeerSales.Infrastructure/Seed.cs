using BeerSales.Domain.Entities;

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
            new (abbayeDeLeffeBreweryId , "Abbaye de Leffe", default),
            new (heinekenBreweryId , "Heineken", default),
            new (guinessBreweryId , "Gunniess", default),
        };

        #endregion

        #region Beers

        static readonly Guid leffeBlondeId = Guid.NewGuid();
        static readonly Guid heinekenSilverId = Guid.NewGuid();
        static readonly Guid guinessDraughtId = Guid.NewGuid();

        internal static Beer[] Beers =
       {
            new (leffeBlondeId, abbayeDeLeffeBreweryId ,"Leffe Blonde", 2.3m, 6.6m, "EUR", default),
            new (heinekenSilverId, heinekenBreweryId, "Heineken Silver", 1.5m, 4.5m, "EUR", default),
            new (guinessDraughtId, guinessBreweryId, "Guinness Draught", 2.6m, 5.6m, "EUR", default)
        };

        #endregion

        #region Wholesalers

        static readonly Guid geneDrinksId = Guid.NewGuid();
        static readonly Guid allBeerSalesId = Guid.NewGuid();
        static readonly Guid foreverBeerId = Guid.NewGuid();

        internal static Wholesaler[] Wholesalers =
        {
            new (geneDrinksId, "GeneDrinks", default),
            new (allBeerSalesId, "AllBeerSales", default),
            new ( foreverBeerId, "Forever Beer", default)
        };

        #endregion

        #region Stocks

        internal static Stock[] Stocks =
        {
            new ( Guid.NewGuid(), geneDrinksId, leffeBlondeId, 100, default),
            new ( Guid.NewGuid(), geneDrinksId, heinekenSilverId, 50, default),

            new ( Guid.NewGuid(), allBeerSalesId, leffeBlondeId, 30, default),
            new ( Guid.NewGuid(), allBeerSalesId, heinekenSilverId, 200, default),
            new ( Guid.NewGuid(), allBeerSalesId, guinessDraughtId, 70, default),

            new ( Guid.NewGuid(), foreverBeerId, leffeBlondeId, 300, default),
            new ( Guid.NewGuid(), foreverBeerId, heinekenSilverId, 20, default),
            new ( Guid.NewGuid(), foreverBeerId, guinessDraughtId, 40, default)
        };

        #endregion

        #region Discounts

        internal static Discount[] Discounts =
        {
            new ( Guid.NewGuid(), 11, 10, default),
            new ( Guid.NewGuid(), 21, 20, default)
        };

        #endregion

    }
}
