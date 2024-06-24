using System.Diagnostics;
using Microsoft.SemanticKernel.Embeddings;
using Pgvector;

namespace Sprmon.Shop.Catalog.API.Services;

public sealed class CatalogAI : ICatalogAI
{
  private const int EmbeddingDimensions = 384;
  private readonly ITextEmbeddingGenerationService _embeddingGenerator;
  private readonly IWebHostEnvironment _environment;
  private readonly ILogger<CatalogAI> _logger;

  public CatalogAI(
      IWebHostEnvironment environment,
      ILogger<CatalogAI> logger,
      ITextEmbeddingGenerationService embeddingGenerator = null
  )
  {
    _environment = environment;
    _logger = logger;
    _embeddingGenerator = embeddingGenerator;
  }

  public bool IsEnabled => _embeddingGenerator is not null;

  public ValueTask<Vector> GetEmbeddingAsync(CatalogItem item) =>
    IsEnabled?
        GetEmbeddingAsync(CatalogItemToString(item)) :
        ValueTask.FromResult<Vector>(null);

  public async ValueTask<IReadOnlyList<Vector>> GetEmbeddingsAsync(
      IEnumerable<CatalogItem> items)
  {
    if (IsEnabled)
    {
      long timestamp = Stopwatch.GetTimestamp();

      IList<ReadOnlyMemory<float>> embeddings = (
          await _embeddingGenerator.GenerateEmbeddingsAsync(
              items.Select(CatalogItemToString).ToList()
          ));
      var results = embeddings.Select(
          m => new Vector(m[..EmbeddingDimensions])).ToList();

      if (_logger.IsEnabled(LogLevel.Trace))
      {
        _logger.LogTrace(
            "Generated {EmbeddingsCount} embeddings in {ElapsedMilliseconds}ms",
            results.Count,
            (Stopwatch.GetTimestamp() - timestamp) * 1000 / Stopwatch.Frequency
        );
      }

      return results;
    }
    return null;
  }

  public async ValueTask<Vector> GetEmbeddingAsync(string text)
  {
    if (IsEnabled)
    {
      long timestamp = Stopwatch.GetTimestamp();

      ReadOnlyMemory<float> embedding = (
          await _embeddingGenerator.GenerateEmbeddingAsync(text));
      embedding = embedding[0..EmbeddingDimensions];

      if (_logger.IsEnabled(LogLevel.Trace))
      {
        _logger.LogTrace(
            "Generated embedding in {ElapsedMilliseconds}ms: '{Text}'",
            (Stopwatch.GetTimestamp() - timestamp) * 1000 / Stopwatch.Frequency,
            text
        );
      }

      return new Vector(embedding);
    }

    return null;
  }

  private static string CatalogItemToString(CatalogItem item) => (
      $"{item.Name} {item.Description}");
}
