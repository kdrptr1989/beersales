using BeerSales.Core.Order.Commands.CreateQuote;
using FluentAssertions;
using NSubstitute;
using BeerSales.Infrastructure.Interfaces;
using Microsoft.Extensions.Logging;
using BeerSales.Core.Order.Dto;
using Microsoft.EntityFrameworkCore;
using BeerSales.Infrastructure.Data;
using Microsoft.EntityFrameworkCore.Diagnostics;
using BeerSales.Infrastructure.Repository.Interface;
using System.Diagnostics;
using BeerSales.Domain.Entities;

namespace BeerSales.UnitTests.Core.Order
{
    public class CreateQuoteCommandHandlerTests
    {
        private readonly DbContextOptions<BeerSaleDbContext> _contextOptions;
        private readonly BeerSaleDbContext _dbContext;

        #region Constructor

        [Fact]
        [Trait("Order.Command.CreteQuote.CreateQuoteCommandHandler", "Constructor")]
        public void Constructor_GivenNullParameters_ThrowsArgumentNullException()
        {
            // Arrange
            Action act = () => new CreateQuoteCommandHandler(null, null, null, null, null);

            act.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        [Trait("Order.Command.CreteQuote.CreateQuoteCommandHandler", "Constructor")]
        public void Constructor_GivenValidParameters_CreateNewInstance()
        {
            // Arrange
            var dbContext = Substitute.For<IBeerSalesDbContext>();
            var stockRepository = Substitute.For<IStockRepository>();
            var discountRepository = Substitute.For<IDiscountRepository>();
            var wholesalerRepositor = Substitute.For<IWholesalerRepository>();
            var logger = Substitute.For<ILogger<CreateQuoteCommandHandler>>();

            // Act
            var result = new CreateQuoteCommandHandler(dbContext, wholesalerRepositor, stockRepository, discountRepository, logger);

            // Assert
            result.Should().NotBeNull();
        }

        #endregion

        #region Handle

        [Fact]
        [Trait("Order.Command.CreteQuote.CreateQuoteCommandHandler", "Handle")]
        public void Handle_GivenNullParameters_ThrowsArgumentNullException()
        {
            // Arrange
            var dbContext = Substitute.For<IBeerSalesDbContext>();
            var stockRepository = Substitute.For<IStockRepository>();
            var discountRepository = Substitute.For<IDiscountRepository>();
            var wholesalerRepositor = Substitute.For<IWholesalerRepository>();
            var logger = Substitute.For<ILogger<CreateQuoteCommandHandler>>();

            // Act
            var handler = new CreateQuoteCommandHandler(dbContext, wholesalerRepositor, stockRepository, discountRepository, logger);

            var token = new CancellationToken();

            Func<Task> act = async () => { _ = await handler.Handle(null, token); };

            //Assert
            act.Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact]
        [Trait("Order.Command.CreteQuote.CreateQuoteCommandHandler", "Handle")]
        public async void Handle_GivenValidRequest_SuccessResult()
        {
            // Arrange
            var dbContext = Substitute.For<IBeerSalesDbContext>();
            var stockRepository = Substitute.For<IStockRepository>();
            var discountRepository = Substitute.For<IDiscountRepository>();
            var wholesalerRepository = Substitute.For<IWholesalerRepository>();
            var logger = Substitute.For<ILogger<CreateQuoteCommandHandler>>();

            // Act
            var handler = new CreateQuoteCommandHandler(dbContext, wholesalerRepository, stockRepository, discountRepository, logger);

            var wholesalerId = Guid.NewGuid();
            var beerIdFirst = Guid.NewGuid();
            var beerIdSecond = Guid.NewGuid();

            
            stockRepository
               .GetAll
               .ReturnsForAnyArgs(x =>
                   new List<Stock>
                   {
                       {
                           new Stock(Guid.NewGuid(), wholesalerId, beerIdFirst, 66, DateTime.Now)
                           {
                                 Beer = new Beer(beerIdFirst, Guid.NewGuid(), "Beer First", 2, 3.4m, "EUR", DateTime.Now),
                                 Wholesaler = new Wholesaler(wholesalerId, "WholesalerTest", DateTime.Now)
                           }
                       },
                       { 
                            new Stock(Guid.NewGuid(), wholesalerId, beerIdSecond, 40, DateTime.Now)
                            {
                                 Beer = new Beer(beerIdSecond, Guid.NewGuid(), "Beer Second", 3, 3.3m, "EUR", DateTime.Now),
                                 Wholesaler = new Wholesaler(wholesalerId, "WholesalerTest", DateTime.Now)
                            }
                       }
                   }.AsQueryable()
               );

            discountRepository
                .GetAll
                .ReturnsForAnyArgs(x =>
                    new List<Discount>
                    {
                        { new Discount(Guid.NewGuid(), 11, 10, DateTime.Now)},
                        { new Discount(Guid.NewGuid(), 21, 20, DateTime.Now)}
                    }.AsQueryable()
                );

            wholesalerRepository
                .GetAll
                .ReturnsForAnyArgs(x => new List<Wholesaler> 
                    {
                        { new Wholesaler(wholesalerId, "WholesalerTest", DateTime.Now) }
                    }.AsQueryable()
                 );

            var orderList = new List<OrderDto>()
            {
                { new OrderDto(wholesalerId, beerIdFirst, 5) },
                { new OrderDto(wholesalerId, beerIdSecond, 4) },
            };

            var request = new CreateQuoteCommand(orderList);
            var cancellationToken = new CancellationToken();

            var result = await handler.Handle(request, cancellationToken);

            //Assert
            result.Should().NotBeNull();
            result.Success.Should().BeTrue();
            result.TotalPrice.Should().Be(22);
            result.DiscountPercentageValue.Should().BeEmpty();
            result.ReducedTotalPriceWithDiscount.Should().BeNull();
        }


        [Fact]
        [Trait("Order.Command.CreteQuote.CreateQuoteCommandHandler", "Handle")]
        public async void Handle_GivenValidRequest_Failed_With_Ordered_Quantity_Grather_Than_Stock_Quantity()
        {
            // Arrange
            var dbContext = Substitute.For<IBeerSalesDbContext>();
            var stockRepository = Substitute.For<IStockRepository>();
            var discountRepository = Substitute.For<IDiscountRepository>();
            var wholesalerRepository = Substitute.For<IWholesalerRepository>();
            var logger = Substitute.For<ILogger<CreateQuoteCommandHandler>>();

            // Act
            var handler = new CreateQuoteCommandHandler(dbContext, wholesalerRepository, stockRepository, discountRepository, logger);

            var wholesalerId = Guid.NewGuid();
            var beerIdFirst = Guid.NewGuid();
            var beerIdSecond = Guid.NewGuid();


            stockRepository
               .GetAll
               .ReturnsForAnyArgs(x =>
                   new List<Stock>
                   {
                       {
                           new Stock(Guid.NewGuid(), wholesalerId, beerIdFirst, 10, DateTime.Now)
                           {
                                 Beer = new Beer(beerIdFirst, Guid.NewGuid(), "Beer First", 2, 3.4m, "EUR", DateTime.Now),
                                 Wholesaler = new Wholesaler(wholesalerId, "WholesalerTest", DateTime.Now)
                           }
                       },
                       {
                            new Stock(Guid.NewGuid(), wholesalerId, beerIdSecond, 60, DateTime.Now)
                            {
                                 Beer = new Beer(beerIdSecond, Guid.NewGuid(), "Beer Second", 3, 3.3m, "EUR", DateTime.Now),
                                 Wholesaler = new Wholesaler(wholesalerId, "WholesalerTest", DateTime.Now)
                            }
                       }
                   }.AsQueryable()
               );

            discountRepository
                .GetAll
                .ReturnsForAnyArgs(x =>
                    new List<Discount>
                    {
                        { new Discount(Guid.NewGuid(), 11, 10, DateTime.Now)},
                        { new Discount(Guid.NewGuid(), 21, 20, DateTime.Now)}
                    }.AsQueryable()
                );

            wholesalerRepository
                .GetAll
                .ReturnsForAnyArgs(x => new List<Wholesaler>
                    {
                        { new Wholesaler(wholesalerId, "WholesalerTest", DateTime.Now) }
                    }.AsQueryable()
                 );

            var orderList = new List<OrderDto>()
            {
                { new OrderDto(wholesalerId, beerIdFirst, 20) },
                { new OrderDto(wholesalerId, beerIdSecond, 50) },
            };

            var request = new CreateQuoteCommand(orderList);
            var cancellationToken = new CancellationToken();

            var result = await handler.Handle(request, cancellationToken);

            //Assert
            result.Should().NotBeNull();
            result.Success.Should().BeFalse();
            result.ErrorMessage.Should().Be("Exception message: The number of beers ordered cannot be greater than the wholesaler's stock.");
        }

        [Fact]
        [Trait("Order.Command.CreteQuote.CreateQuoteCommandHandler", "Handle")]
        public async void Handle_GivenValidRequest_Failed_With_Duplicated_Order_Items()
        {
            // Arrange
            var dbContext = Substitute.For<IBeerSalesDbContext>();
            var stockRepository = Substitute.For<IStockRepository>();
            var discountRepository = Substitute.For<IDiscountRepository>();
            var wholesalerRepository = Substitute.For<IWholesalerRepository>();
            var logger = Substitute.For<ILogger<CreateQuoteCommandHandler>>();

            // Act
            var handler = new CreateQuoteCommandHandler(dbContext, wholesalerRepository, stockRepository, discountRepository, logger);

            var wholesalerId = Guid.NewGuid();
            var beerIdFirst = Guid.NewGuid();
            var beerIdSecond = Guid.NewGuid();


            stockRepository
               .GetAll
               .ReturnsForAnyArgs(x =>
                   new List<Stock>
                   {
                       {
                           new Stock(Guid.NewGuid(), wholesalerId, beerIdFirst, 10, DateTime.Now)
                           {
                                 Beer = new Beer(beerIdFirst, Guid.NewGuid(), "Beer First", 2, 3.4m, "EUR", DateTime.Now),
                                 Wholesaler = new Wholesaler(wholesalerId, "WholesalerTest", DateTime.Now)
                           }
                       },
                       {
                            new Stock(Guid.NewGuid(), wholesalerId, beerIdSecond, 60, DateTime.Now)
                            {
                                 Beer = new Beer(beerIdSecond, Guid.NewGuid(), "Beer Second", 3, 3.3m, "EUR", DateTime.Now),
                                 Wholesaler = new Wholesaler(wholesalerId, "WholesalerTest", DateTime.Now)
                            }
                       }
                   }.AsQueryable()
               );

            discountRepository
                .GetAll
                .ReturnsForAnyArgs(x =>
                    new List<Discount>
                    {
                        { new Discount(Guid.NewGuid(), 11, 10, DateTime.Now)},
                        { new Discount(Guid.NewGuid(), 21, 20, DateTime.Now)}
                    }.AsQueryable()
                );

            wholesalerRepository
                .GetAll
                .ReturnsForAnyArgs(x => new List<Wholesaler>
                    {
                        { new Wholesaler(wholesalerId, "WholesalerTest", DateTime.Now) }
                    }.AsQueryable()
                 );

            var orderList = new List<OrderDto>()
            {
                { new OrderDto(wholesalerId, beerIdFirst, 20) },
                { new OrderDto(wholesalerId, beerIdFirst, 50) },
            };

            var request = new CreateQuoteCommand(orderList);
            var cancellationToken = new CancellationToken();

            var result = await handler.Handle(request, cancellationToken);

            //Assert
            result.Should().NotBeNull();
            result.Success.Should().BeFalse();
            result.ErrorMessage.Should().Be("Exception message: There can't be any duplicate in the order.");
        }

        #endregion

    }
}