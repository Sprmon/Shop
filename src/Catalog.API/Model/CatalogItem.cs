using System.ComponentModel.DataAnnotations;
using System.Numerics;
using System.Text.Json.Serialization;
using Pgvector;

namespace Sprmon.Shop.Catalog.API.Model;

public class CatalogItem
{
  public int Id { get; set; }

  [Required]
  public string Name { get; set; }

  public string Description { get; set; }

  public decimal Price { get; set; }

  public string PictureFileName { get; set; }

  public string PictureUri { get; set; }

  public int CatalogTypeId { get; set; }

  public CatalogType CatalogType { get; set; }

  public int CatalogBrandId { get; set; }

  public CatalogBrand CatalogBrand { get; set; }

  public int AvailableStock { get; set; }

  public int RestockThreshold { get; set; }

  public int MaxStockThreshold { get; set; }

  [JsonIgnore]
  public Pgvector.Vector Embedding { get; set; }

  public bool OnReorder { get; set; }

  public CatalogItem() { }

  /// <summary>
  /// Decrements the quantity of a particular item in inventory and ensures the
  /// restockThreshold hasn't been breached. If so, a RestockRequest is
  /// generated in CheckThreshold.
  ///
  /// If there is sufficient stock of an item, then the integer returned at the
  /// end of this call should be the same as quantityDesired. In the event that
  /// there is not sufficient stock available, the method will remove whatever
  /// stock is available and return that quantity to the client. In this case,
  /// it is the responsibility of the client to determine if the amount that is
  /// returned is the same as quantityDesired.
  /// It is invalid to pass in a negative number. 
  /// </summary>
  /// <param name="quantityDesired"></param>
  /// <returns>int: Returns the number actually removed from stock.</returns>
  /// <exception cref="CatalogDomainException"></exception>
  public int RemoveStock(int quantityDesired)
  {
    if (AvailableStock == 0)
    {
      throw new CatalogDomainException($"Empty stock, product item {Name} is sold out");
    }

    if (quantityDesired <= 0)
    {
      throw new CatalogDomainException("Item units desired should be greater than zero");
    }

    int removed = Math.Min(quantityDesired, AvailableStock);
    AvailableStock -= removed;
    return removed;
  }

  /// <summary>
  /// Increments the quantity of a particular item in inventory.
  /// </summary>
  /// <param name="quantity"></param>
  /// <returns>int: Returns the quantity that has been added to stock</returns>
  public int AddStock(int quantity)
  {
    int original = AvailableStock;
    if ((AvailableStock + quantity) > MaxStockThreshold)
    {
      // For now, this method only adds new units up maximum stock threshold.
      // In an expanded version of this application, we could include tracking
      // for the remaining units and store information about overstock
      // elsewhere. 
      AvailableStock += (MaxStockThreshold - AvailableStock);
    }
    else
    {
      AvailableStock += quantity;
    }

    OnReorder = false;
    return AvailableStock - original;
  }
}
