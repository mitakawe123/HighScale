namespace HighScale.Domain.Models;

public class Migrations 
{
    public required string MigrationId { get; init; }
    
    public required DateTimeOffset AppliedAt { get; init; }
}