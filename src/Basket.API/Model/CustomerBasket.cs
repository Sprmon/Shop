namespace Sprmon.Shop.Basket.API.Model;

public class CustomerBasket
{
  public string BuyerId { get; set; }
  public List<BasketItem> Items { get; set; } = [];

  public CustomerBasket() {}

  public CustomerBasket(string buyerId)
  {
    BuyerId = buyerId;
  }
}
