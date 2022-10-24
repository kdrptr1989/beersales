﻿using BeerSales.Core.Order.Dto;
using BeerSales.Infrastructure.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BeerSales.Core.Order.Commands.CreateOrder
{
    public class CreateQuoteCommandHandler : IRequestHandler<CreateQuoteCommand, CreateQuoteResponse>
    {
        private readonly IBeerSalesDbContext _dbContext;

        public CreateQuoteCommandHandler(IBeerSalesDbContext context)
        {
            _dbContext = context;
        }

        public async Task<CreateQuoteResponse> Handle(CreateQuoteCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var validRequest = await ValidateQuoteRequest(request, cancellationToken);
                            
                var orderSummaryList = new List<OrderSummaryDto>();

                foreach (var order in request.OrdersList)
                {
                    var stock = _dbContext
                        .Stocks
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
                // TODO logging
                return new CreateQuoteResponse
                {
                    Success = false,
                    ErrorMessage = $"Exception message: " + ex.Message + " Inner exception: " + ex.InnerException
                };
            }
        }

        #region Private methods

        private decimal? GetDiscount(int totalQuantity)
        {
            var discount = _dbContext
                .Discounts                
                .OrderByDescending(x=>x.TierFrom)
                .FirstOrDefault(d => d.TierFrom <= totalQuantity);

            return discount != null ? discount.DiscountPercentage : default;
        }

        private async Task<bool> ValidateQuoteRequest(CreateQuoteCommand request, CancellationToken cancellationToken)
        {
            // Order list check
            if (!request.OrdersList.Any())
            {
                throw new Exception("Order can not be empty");
            }

            // Wholesaler exists check
            var wholesalerListIds = request.OrdersList.Select(x => x.WholesalerId).Distinct();
            if (wholesalerListIds.Any())
            {
                var wholesalers = await _dbContext
                    .Wholesalers
                    .Select(x => x.Id)
                    .ToListAsync(cancellationToken);

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
                var stockByWholesaler = await _dbContext
                    .Stocks
                    .Where(x => x.WholesalerId == order.WholesalerId)
                    .ToListAsync(cancellationToken);

                var stockByBeer = stockByWholesaler.FirstOrDefault(x => x.BeerId == order.BeerId);
                if (stockByBeer is null || stockByBeer.Quantity == 0)
                {
                    throw new Exception("The beer must be sold by the wholesaler");
                }

                if (stockByBeer.Quantity < order.Quantity)
                {
                    throw new Exception("The number of beers ordered cannot be greater than the wholesaler's stock");
                }
            }

            return true;
        }

        #endregion
    }
}