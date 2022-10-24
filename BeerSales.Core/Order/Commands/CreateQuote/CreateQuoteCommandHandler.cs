using BeerSale.Infrastructure;
using BeerSales.Core.Order.Dto;
using BeerSales.Infrastructure.Interfaces;
using BeerSales.Infrastructure.Repository.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BeerSales.Core.Order.Commands.CreateQuote
{
    public class CreateQuoteCommandHandler : IRequestHandler<CreateQuoteCommand, CreateQuoteResponse>
    {
        private readonly IBeerSalesDbContext _dbContext;
        private readonly IWholesalerRepository _wholesalerRepository;
        private readonly IStockRepository _stockRepository;
        private readonly IDiscountRepository _discountRepository;
        private readonly ILogger<CreateQuoteCommandHandler> _logger;

        public CreateQuoteCommandHandler(
            IBeerSalesDbContext context,
            IWholesalerRepository wholesalerRepository,
            IStockRepository stockRepository,
            IDiscountRepository discountRepository,
            ILogger<CreateQuoteCommandHandler> logger)
        {
            Ensure.ArgumentNotNull(context, nameof(context));
            Ensure.ArgumentNotNull(logger, nameof(logger));
            Ensure.ArgumentNotNull(wholesalerRepository, nameof(wholesalerRepository));
            Ensure.ArgumentNotNull(stockRepository, nameof(stockRepository));
            Ensure.ArgumentNotNull(discountRepository, nameof(discountRepository));

            _dbContext = context;
            _logger = logger;
            _wholesalerRepository = wholesalerRepository;
            _stockRepository = stockRepository;
            _discountRepository = discountRepository;
        }

        public async Task<CreateQuoteResponse> Handle(CreateQuoteCommand request, CancellationToken cancellationToken)
        {
            try
            {
                Ensure.ArgumentNotNull(request, nameof(request));

                _logger.Log(LogLevel.Information, $"{nameof(CreateQuoteCommand)} is called");

                ValidateRequest(request, cancellationToken);
                            
                var orderSummaryList = new List<OrderSummaryDto>();

                foreach (var order in request.OrdersList)
                {
                    var stock = _stockRepository
                        .GetAll
                        .Include(x => x.Wholesaler)
                        .Include(x => x.Beer)
                        .FirstOrDefault(x => x.WholesalerId == order.WholesalerId && x.BeerId == order.BeerId);

                    if (stock is null)
                    {
                        throw new Exception("Stock not found");
                    }

                    var orderDetail = new OrderSummaryDto(
                            stock.WholesalerId,
                            stock.Wholesaler.Name,
                            stock.BeerId,
                            stock.Beer.Name,
                            order.Quantity,
                            stock.Beer.Price,
                            stock.Beer.Currency,
                            order.Quantity * stock.Beer.Price);

                    if (orderDetail != null)
                    {
                       orderSummaryList.Add(orderDetail);
                    }
                }

                var totalQuantity = orderSummaryList.Sum(x => x.Quantity);
                var totalPrice = orderSummaryList.Sum(x => x.SubTotalPrice);
                var discountValue = GetDiscount(totalQuantity);
                var totalPriceWithdrawDiscount = discountValue != null ? totalPrice * (1 - discountValue / 100) : default;

                return new CreateQuoteResponse
                {
                    Success = true,
                    OrderSummaryDtos = orderSummaryList,
                    DiscountPercentageValue = discountValue != null ? $"{discountValue.ToString()} %" : string.Empty,
                    ReducedTotalPriceWithDiscount = totalPriceWithdrawDiscount,
                    TotalPrice = totalPrice
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Problem during {nameof(CreateQuoteCommand)} quote creation.");

                return new CreateQuoteResponse
                {
                    Success = false,
                    ErrorMessage = $"Exception message: " + ex.Message
                };
            }
        }

        #region Private methods

        private decimal? GetDiscount(int totalQuantity)
        {
            var discount = _discountRepository
                .GetAll
                .OrderByDescending(x => x.TierFrom)
                .FirstOrDefault(d => d.TierFrom <= totalQuantity);                

            return discount != null ? discount.DiscountPercentage : null;
        }

        private void ValidateRequest(CreateQuoteCommand request, CancellationToken cancellationToken)
        {          
            // Wholesaler exists check
            var wholesalerListIds = request.OrdersList.Select(x => x.WholesalerId).Distinct();
            if (wholesalerListIds.Any())
            {
                var wholesalers = _wholesalerRepository
                    .GetAll
                    .Select(x => x.Id)
                    .ToList();

                if (!wholesalerListIds.All(x => wholesalers.Contains(x)))
                {
                    throw new Exception("The wholesaler must be exits.");
                }
            }

            // Duplicate check in the orders
            var duplicates = request
                .OrdersList
                .GroupBy(x => new { x.WholesalerId, x.BeerId })
                .Select(g => g.Count() > 1)
                .ToList();

            if (duplicates.Contains(true))
            {
                throw new Exception("There can't be any duplicate in the order.");
            }

            // Wholesaler's stock check
            foreach (var order in request.OrdersList)
            {
                var stockByWholesaler = _stockRepository
                    .GetAll
                    .Where(x => x.WholesalerId == order.WholesalerId);
                   
                var stockByBeer = stockByWholesaler.FirstOrDefault(x => x.BeerId == order.BeerId);
                if (stockByBeer is null || stockByBeer.Quantity == 0)
                {
                    throw new Exception("The beer must be sold by the wholesaler.");
                }

                if (stockByBeer.Quantity < order.Quantity)
                {
                    throw new Exception("The number of beers ordered cannot be greater than the wholesaler's stock.");
                }
            }
        }

        #endregion
    }
}
