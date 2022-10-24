using BeerSales.Core.Order.Dto;

namespace BeerSales.Core.Order.Commands.CreateQuote
{
    public record CreateQuoteResponse : BaseResponse
    {
        public List<OrderSummaryDto> OrderSummaryDtos { get; init; }

        public decimal TotalPrice { get; init; }
        
        public decimal? ReducedTotalPriceWithDiscount { get; init; }
        
        public string DiscountPercentageValue { get; init; }
    }
}
