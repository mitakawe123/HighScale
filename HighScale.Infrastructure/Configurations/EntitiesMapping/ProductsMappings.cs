using Cassandra.Mapping;
using HighScale.Domain.Constants;
using HighScale.Domain.Models;

namespace HighScale.Infrastructure.Configurations.EntitiesMapping;

public class ProductsMappings : Mappings
{
    public ProductsMappings()
    {
        For<Products>()
            .TableName(Constants.TableNames.Products)
            .KeyspaceName(Constants.Database.HighScaleKeySpace)
            .PartitionKey(x => x.Category, x => x.Price )
            .ClusteringKey(x => x.CreateAt, SortOrder.Descending)
            .Column(x => x.Name, map => map.WithName("name"))
            .Column(x => x.Category, map => map.WithName("category"))
            .Column(x => x.Price, map => map.WithName("price"))
            .Column(x => x.Stock, map => map.WithName("stock"))
            .Column(x => x.CreateAt, map => map.WithName("create_at"));
    }   
}