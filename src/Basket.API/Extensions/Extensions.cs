using System.Text.Json.Serialization;
using Sprmon.Shop.Basket.API.Repositories;
using Sprmon.Shop.Basket.API.IntegrationEvents.Events;
using Sprmon.Shop.Basket.API.IntegrationEvents.EventHandling;

namespace Sprmon.Shop.Basket.API.Extensions;

public static class Extensions
{
  public static void AddApplicationServices(this IHostApplicationBuilder builder)
  {
    builder.AddDefaultAuthentication();
    builder.AddRedisClient("redis");
    builder.Services.AddSingleton<IBasketRepository, RedisBasketRepository>();
    builder.AddRabbitMQEventBus("eventbus")
      .AddSubscription<OrderStartedIntegrationEvent, OrderStartedIntegrationEventHandler>()
      .ConfigureJsonOptions(options =>
      {
        options.TypeInfoResolverChain.Add(IntegrationEventContext.Default);
      });
  }
}

[JsonSerializable(typeof(OrderStartedIntegrationEvent))]
partial class IntegrationEventContext : JsonSerializerContext
{

}
