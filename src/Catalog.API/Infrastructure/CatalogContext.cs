namespace Sprmon.Shop.Catalog.API.Infrastructure;

public class CatalogContext : DbContext
{
  public CatalogContext(DbContextOptions<CatalogContext> options) : base(options) { }

  public DbSet<CatalogBrand> CatalogBrands { get; set; }
  public DbSet<CatalogType> CatalogTypes { get; set; }
  public DbSet<CatalogItem> CatalogItems { get; set; }

  protected override void OnModelCreating(ModelBuilder builder)
  {
    builder.HasPostgresExtension("vector");
    builder.ApplyConfiguration(new CatalogBrandEntityTypeConfiguration());
    builder.ApplyConfiguration(new CatalogTypeEntityTypeConfiguration());
    builder.ApplyConfiguration(new CatalogItemEntityTypeConfiguration());

    // Add the outbox table to this context
    builder.UseIntegrationEventLogs();
  }
}
