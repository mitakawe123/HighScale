using Cassandra.Mapping;
using HighScale.Domain.Constants;
using HighScale.Domain.Models;

namespace HighScale.Infrastructure.Configurations.EntitiesMapping;

public class OrdersMappings : Mappings
{
    public OrdersMappings()
    {
        For<Orders>()
            .TableName(Constants.TableNames.Orders)
            .KeyspaceName(Constants.Database.HighScaleKeySpace)
            .PartitionKey(x => x.UserId)
            .ClusteringKey(x => x.OrderDate, SortOrder.Descending)
            .Column(x => x.UserId, map => map.WithName("user_id"))
            .Column(x => x.Quantity, map => map.WithName("quantity"))
            .Column(x => x.ProductId, map => map.WithName("product_id"))
            .Column(x => x.TotalPrice, map => map.WithName("total_price"))
            .Column(x => x.OrderDate, map => map.WithName("order_date"));
    }
}