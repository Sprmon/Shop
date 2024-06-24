using System.ComponentModel.DataAnnotations;

namespace Sprmon.Shop.Catalog.API.Model;

public class CatalogBrand
{
  public int Id { get; set; }

  [Required]
  public string Brand { get; set; }
}
