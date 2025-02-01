namespace HighScale.Domain.Configurations.Settings;

public class ScyllaDbSettings
{
    public required string ContractPoint { get; init; }
    
    public required string Username { get; init; }
    
    public required string Password { get; init; }
}