namespace HighScale.Domain.Models;

public class Products
{
    public long Id { get; init; }
    
    public required string Name { get; init; }
    
    public decimal Price { get; init; }
    
    public int Stock { get; init; }
    
    public required string Category { get; init; }

    public DateTimeOffset CreateAt { get; init; } = DateTimeOffset.UtcNow;
}