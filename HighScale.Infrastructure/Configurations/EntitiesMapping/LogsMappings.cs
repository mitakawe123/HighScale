using Cassandra.Mapping;
using HighScale.Domain.Constants;
using HighScale.Domain.Models;

namespace HighScale.Infrastructure.Configurations.EntitiesMapping;

public class LogsMappings : Mappings
{
    public LogsMappings()
    {
        For<Logs>()
            .TableName(Constants.TableNames.Logs)
            .KeyspaceName(Constants.Database.HighScaleKeySpace)
            .PartitionKey(x => x.Level)
            .ClusteringKey(x => x.CreatedAt, SortOrder.Descending)
            .Column(x => x.Level, map => map.WithName("level"))
            .Column(x => x.Message, map => map.WithName("message"))
            .Column(x => x.CreatedAt, map => map.WithName("created_at"));
    }
}