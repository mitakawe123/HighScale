namespace HighScale.Domain.Models;

public class Orders
{
    public long Id { get; init; }
    
    public long UserId { get; init; }
    
    public long ProductId { get; init; }
    
    public int Quantity { get; init; }
    
    public decimal TotalPrice { get; init; }

    public DateTimeOffset OrderDate { get; init; } = DateTimeOffset.UtcNow;
}