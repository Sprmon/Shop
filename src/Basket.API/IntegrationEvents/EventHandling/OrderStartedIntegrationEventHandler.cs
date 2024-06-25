using Sprmon.Shop.Basket.API.Repositories;
using Sprmon.Shop.Basket.API.IntegrationEvents.Events;

namespace Sprmon.Shop.Basket.API.IntegrationEvents.EventHandling;

public class OrderStartedIntegrationEventHandler(
    IBasketRepository repository,
    ILogger<OrderStartedIntegrationEventHandler> logger
) : IIntegrationEventHandler<OrderStartedIntegrationEvent>
{
  public async Task Handle(OrderStartedIntegrationEvent @event)
  {
    logger.LogInformation(
        "Received order started event for order {id} - ({@IntegrationEvent})",
        @event.Id, @event
    );
    await repository.DeleteBasketAsync(@event.UserId);
  }
}
