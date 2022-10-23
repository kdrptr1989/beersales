namespace BeerSales.Api.Interface
{
    public interface IEndpoint
    {
        public static abstract void DefineEndpoint(IEndpointRouteBuilder builder);
    }
}
