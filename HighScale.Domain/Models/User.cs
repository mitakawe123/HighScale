namespace HighScale.Domain.Models;

public class User
{
    public long Id { get; init; }
    
    public required string Name { get; init; }
    
    public ushort Age { get; init; }
    
    public required string Address { get; init; }
    
    public DateTimeOffset CreatedAt { get; init; } = DateTimeOffset.UtcNow;
    
    public DateTimeOffset UpdatedAt { get; init; } = DateTimeOffset.UtcNow;
}