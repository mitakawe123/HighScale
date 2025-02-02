using Cassandra.Mapping;
using HighScale.Domain.Constants;
using HighScale.Domain.Models;

namespace HighScale.Infrastructure.Configurations.EntitiesMapping;

public class UserMapping : Mappings
{
    public UserMapping()
    {
        For<User>()
            .TableName(Constants.TableNames.Users)
            .KeyspaceName(Constants.Database.HighScaleKeySpace)
            .PartitionKey(x => x.Id)
            .Column(x => x.Name, map => map.WithName("name"))
            .Column(x => x.Age, map => map.WithName("age"))
            .Column(x => x.Address, map => map.WithName("address"))
            .Column(x => x.CreatedAt, map => map.WithName("created_at"))
            .Column(x => x.UpdatedAt, map => map.WithName("updated_at"));
    }
}