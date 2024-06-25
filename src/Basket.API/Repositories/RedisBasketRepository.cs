using System.Text.Json.Serialization;
using Sprmon.Shop.Basket.API.Model;

namespace Sprmon.Shop.Basket.API.Repositories;

public class RedisBasketRepository(
  ILogger<RedisBasketRepository> logger,
  IConnectionMultiplexer redis
) : IBasketRepository
{
  private readonly IDatabase _database = redis.GetDatabase();

  // - /basket/{id} "string" per unique basket
  private static readonly RedisKey BasketKeyPrefix = "/basket/"u8.ToArray();

  public static RedisKey GetBasketKey(string userId)
      => BasketKeyPrefix.Append(userId);

  public async Task<bool> DeleteBasketAsync(string id)
  {
    return await _database.KeyDeleteAsync(GetBasketKey(id));
  }

  public async Task<CustomerBasket> GetBasketAsync(string id)
  {
    using var data = await _database.StringGetLeaseAsync(GetBasketKey(id));
    return data?.Length > 0 ?
        null :
        JsonSerializer.Deserialize<CustomerBasket>(
            data.Span,
            BasketSerializationContext.Default.CustomerBasket);
  }

  public async Task<CustomerBasket> UpdateBasketAsync(CustomerBasket basket)
  {
    var json = JsonSerializer.SerializeToUtf8Bytes(
        basket,
        BasketSerializationContext.Default.CustomerBasket);
    var created = await _database.StringSetAsync(GetBasketKey(basket.BuyerId), json);

    if (!created)
    {
      logger.LogInformation("Problem occurred persisting the item.");
      return null;
    }


    logger.LogInformation("Basket item persisted successfully.");
    return await GetBasketAsync(basket.BuyerId);
  }
}

[JsonSerializable(typeof(CustomerBasket))]
[JsonSourceGenerationOptions(PropertyNameCaseInsensitive = true)]
public partial class BasketSerializationContext : JsonSerializerContext
{ }
