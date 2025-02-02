using Cassandra.Mapping;
using HighScale.Domain.Constants;

namespace HighScale.Infrastructure.Configurations.EntitiesMapping;

public class MigrationsMapping : Mappings
{
    public MigrationsMapping()
    {
        For<Domain.Models.Migrations>()
            .TableName(Constants.TableNames.Migrations)
            .KeyspaceName(Constants.Database.HighScaleKeySpace)
            .PartitionKey(u => u.MigrationId)
            .Column(u => u.MigrationId, cm => cm.WithName("migration_id"))
            .Column(u => u.AppliedAt, cm => cm.WithName("applied_at"));
    }
}