using Microsoft.Extensions.DependencyInjection;

namespace HighScale.Application.Configurations;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAppServices(this IServiceCollection services)
    {
        return services;
    }
}