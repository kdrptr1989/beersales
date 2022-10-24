namespace BeerSales.Core
{
    public record BaseResponse
    {
        public bool Success { get; set; }

        public string ErrorMessage { get; set; }
    }
}
