using Cassandra;
using Cassandra.Mapping;
using HighScale.Domain.Configurations.Settings;
using HighScale.Infrastructure.Configurations.EntitiesMapping;
using Microsoft.Extensions.Caching.StackExchangeRedis;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using ZiggyCreatures.Caching.Fusion;
using ZiggyCreatures.Caching.Fusion.Backplane.StackExchangeRedis;
using ZiggyCreatures.Caching.Fusion.Serialization.SystemTextJson;
using HighScale.Infrastructure.Migrations.MigrationRunner;

namespace HighScale.Infrastructure.Configurations;

public static class ServiceCollectionExtensions 
{
    public static IServiceCollection AddScyllaDbServices(this IServiceCollection services, IConfiguration configuration)
    {
        var scyllaDbSettings = configuration.GetSection(nameof(ScyllaDbSettings)).Get<ScyllaDbSettings>()
            ?? throw new ArgumentException($"Missing configuration section: {nameof(ScyllaDbSettings)}");
        
        //It's recommended to register the Cluster outside the Session
        services.AddSingleton<ICluster>(_ =>
            Cluster.Builder()
                .AddContactPoint(scyllaDbSettings.ContractPoint)
                .WithCredentials(scyllaDbSettings.Username, scyllaDbSettings.Password)
                .Build());

        services.AddSingleton<ISession>(provider => provider.GetRequiredService<ICluster>().Connect());

         //Apply migrations
         services.ApplyMigrations();

         //Apply mapping
         AddScyllaDbMappings();
         
         return services;
    }

    public static IFusionCacheBuilder AddDragonFlyDbServices(this IServiceCollection services, IConfiguration configuration)
    {
        var dragonflyDbSettings = configuration.GetSection(nameof(DragonFlyDbSettings)).Get<DragonFlyDbSettings>()
                                  ?? throw new ArgumentException($"Missing configuration section: {nameof(DragonFlyDbSettings)}");

        return services.AddFusionCache()
            .WithOptions(options => { options.DistributedCacheCircuitBreakerDuration = TimeSpan.FromSeconds(2); })
            .WithDefaultEntryOptions(new FusionCacheEntryOptions
            {
                Duration = TimeSpan.FromMinutes(5),

                IsFailSafeEnabled = true,
                FailSafeMaxDuration = TimeSpan.FromHours(2),
                FailSafeThrottleDuration = TimeSpan.FromSeconds(30),

                FactorySoftTimeout = TimeSpan.FromMilliseconds(100),
                FactoryHardTimeout = TimeSpan.FromMilliseconds(1500),

                JitterMaxDuration = TimeSpan.FromSeconds(2)
            })
            .WithSerializer(
                new FusionCacheSystemTextJsonSerializer())
            .WithDistributedCache(
                new RedisCache(new RedisCacheOptions { Configuration = dragonflyDbSettings.ConnectionString }))
            .WithBackplane(
                new RedisBackplane(new RedisBackplaneOptions { Configuration = dragonflyDbSettings.ConnectionString }));
    }

    private static void ApplyMigrations(this IServiceCollection services)
    {
        var runner = new MigrationRunner();
        runner.RunMigrations(services).Wait();
    }
    
    private static void AddScyllaDbMappings()
    {
        MappingConfiguration.Global.Define<MigrationsMapping>();
    }
}