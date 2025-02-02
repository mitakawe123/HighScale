namespace HighScale.Domain.Models;

public class Logs
{
    public long Id { get; init; }
    
    public required string Level { get; init; }
    
    public required string Message { get; init; }
    
    public DateTimeOffset CreatedAt { get; init; } = DateTimeOffset.UtcNow;
}