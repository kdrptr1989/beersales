namespace BeerSales.Domain
{
    public record BaseEntity(DateTime? ModifiedDate)
    {
        public DateTime CreatedDate { get; init; } = DateTime.UtcNow;                
    }
}
