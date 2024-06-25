using Sprmon.Shop.Basket.API.Model;

namespace Sprmon.Shop.Basket.API.Repositories;

public interface IBasketRepository
{
  Task<CustomerBasket> GetBasketAsync(string customerId);
  Task<CustomerBasket> UpdateBasketAsync(CustomerBasket basket);
  Task<bool> DeleteBasketAsync(string customerId);
}
