using Pgvector;

namespace Sprmon.Shop.Catalog.API.Services;

public interface ICatalogAI
{
  bool IsEnabled { get; }

  ValueTask<Vector> GetEmbeddingAsync(string text);

  ValueTask<Vector> GetEmbeddingAsync(CatalogItem item);

  ValueTask<IReadOnlyList<Vector>> GetEmbeddingsAsync(
      IEnumerable<CatalogItem> item);
}
