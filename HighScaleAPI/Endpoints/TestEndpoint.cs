using FastEndpoints;

namespace HighScaleAPI.Endpoints;

public class TestEndpoint : EndpointWithoutRequest<string>
{
    public override void Configure()
    {
        Get("/test");   
    }

    public override Task<string> ExecuteAsync(CancellationToken ct)
    {
        return Task.FromResult("Hello there");
    }
}