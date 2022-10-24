namespace BeerSales.Core.Beer.Commands
{
    public record AddBeerResponse : BaseResponse
    {
        public Guid Id { get; init; }
    }
}
