using System.Diagnostics.CodeAnalysis;
using Sprmon.Shop.Basket.API.Repositories;
using Sprmon.Shop.Basket.API.Extensions;
using Sprmon.Shop.Basket.API.Model;

namespace Sprmon.Shop.Basket.API.Grpc;

public class BasketService(
    IBasketRepository repository,
    ILogger<BasketService> logger) : Basket.BasketBase
{
  [AllowAnonymous]
  public override async Task<CustomerBasketResponse> GetBasket(
      GetBasketRequest request, ServerCallContext context)
  {
    var id = context.GetUserIdentity();
    if (string.IsNullOrEmpty(id))
    {
      return new();
    }

    if (logger.IsEnabled(LogLevel.Debug))
    {
      logger.LogDebug("Getting basket for user {id}", id);
    }

    var data = await repository.GetBasketAsync(id);
    if (data is not null)
    {
      return MapToCustomerBasketResponse(data);
    }
    return new();
  }

  public override async Task<CustomerBasketResponse> UpdateBasket(
      UpdateBasketRequest request, ServerCallContext context)
  {
    var id = context.GetUserIdentity();
    if (string.IsNullOrEmpty(id))
    {
      ThrowNotAuthenticated();
    }

    if (logger.IsEnabled(LogLevel.Debug))
    {
      logger.LogDebug("Updating basket for user {id}", id);
    }

    var basket = MapToCustomerBasket(id, request);
    var response = await repository.UpdateBasketAsync(basket);
    if (response is null)
    {
      ThrowBasketDoesNotExist(id);
    }

    return MapToCustomerBasketResponse(response);
  }

  public override async Task<DeleteBasketResponse> DeleteBasket(
      DeleteBasketRequest request, ServerCallContext context)
  {
    var id = context.GetUserIdentity();
    if (string.IsNullOrEmpty(id))
    {
      ThrowNotAuthenticated();
    }

    if (logger.IsEnabled(LogLevel.Debug))
    {
      logger.LogDebug("Deleting basket for user {id}", id);
    }

    await repository.DeleteBasketAsync(id);
    return new();
  }

  [DoesNotReturn]
  private static void ThrowNotAuthenticated()
      => throw new RpcException(
          new Status(
              StatusCode.Unauthenticated, "The caller is not authenticated."));

  [DoesNotReturn]
  private static void ThrowBasketDoesNotExist(string id)
      => throw new RpcException(
          new Status(
              StatusCode.NotFound,  $"Basket with buyer id {id} does not exist"));

  private static CustomerBasketResponse MapToCustomerBasketResponse(CustomerBasket basket)
  {
    var response = new CustomerBasketResponse();
    foreach (var item in response.Items)
    {
      response.Items.Add(new BasketItem()
      {
        ProductId = item.ProductId,
        Quantity = item.Quantity,
      });
    }
    return response;
  }

  private static CustomerBasket MapToCustomerBasket(string id, UpdateBasketRequest request)
  {
    var response = new CustomerBasket
    {
      BuyerId = id,
    };

    foreach (var item in request.Items)
    {
      response.Items.Add(new()
      {
        ProductId = item.ProductId,
        Quantity = item.Quantity,
      });
    }
    return response;
  }
}
