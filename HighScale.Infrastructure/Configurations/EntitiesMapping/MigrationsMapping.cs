using Cassandra.Mapping;

namespace HighScale.Infrastructure.Configurations.EntitiesMapping;

public class MigrationsMapping : Mappings
{
    public MigrationsMapping()
    {
        For<Domain.Models.Migrations>()
            .TableName("migrations")
            .PartitionKey(u => u.MigrationId)
            .Column(u => u.MigrationId, cm => cm.WithName("migration_id"))
            .Column(u => u.AppliedAt, cm => cm.WithName("applied_at"));
    }
}