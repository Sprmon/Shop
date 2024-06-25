namespace Sprmon.Shop.Basket.API.Model;

public class BasketItem : IValidatableObject
{
  public string Id { get; set; }
  public int ProductId { get; set; }
  public string ProductName { get; set; }
  public decimal UnitPrice { get; set; }
  public decimal OldUnitPrice { get; set; }
  public int Quantity { get; set; }
  public string PictureUrl { get; set; }

  public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
  {
    var results = new List<ValidationResult>();
    if (Quantity <= 0)
    {
      results.Add(new ValidationResult(
          "Quantity must be greater than 0",
          [nameof(Quantity)]
      ));
    }
    return results;
  }
}
