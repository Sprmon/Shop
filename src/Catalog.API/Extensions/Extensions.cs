using Sprmon.Shop.Catalog.API.Services;
using Microsoft.SemanticKernel;

public static class Extensions
{
  public static void AddApplicationServices(this IHostApplicationBuilder builder)
  {
    builder.AddNpgsqlDbContext<CatalogContext>(
      "CatalogDB",
      configureDbContextOptions: dbContextOptionsBuilder =>
      {
        dbContextOptionsBuilder.UseNpgsql(builder =>
        {
          builder.UseVector();
        });
      });

    builder.Services.AddMigration<CatalogContext, CatalogContextSeed>();

    // Add the integration services that consume the DbContext
    builder.Services.AddTransient<
      IIntegrationEventLogService, IntegrationEventLogService<CatalogContext>>();

    builder.Services.AddTransient<
      ICatalogIntegrationEventService, CatalogIntegrationEventService>();

    builder.AddRabbitMQEventBus("eventbus")
           .AddSubscription<OrderStatusChangedToAwaitingValidationIntegrationEvent,
                            OrderStatusChangedToAwaitingValidationIntegrationEventHandler>()
           .AddSubscription<OrderStatusChangedToPaidIntegrationEvent,
                            OrderStatusChangedToPaidIntegrationEventHandler>();

    builder.Services.AddOptions<CatalogOptions>()
        .BindConfiguration(nameof(CatalogOptions));

    if (builder.Configuration["AI:Onnx:EmbeddingModelPath"] is string modelPath &&
        builder.Configuration["AI:Onnx:EmbeddingVocabPath"] is string vocabPath)
    {
      builder.Services.AddBertOnnxTextEmbeddingGeneration(modelPath, vocabPath);
    }
    else if (!string.IsNullOrWhiteSpace(builder.Configuration.GetConnectionString("openai")))
    {
      builder.AddAzureOpenAIClient("openai");
      builder.Services.AddOpenAITextEmbeddingGeneration(builder.Configuration["AIOptions:OpenAI:EmbeddingName"] ?? "text-embedding-3-small");
    }

    builder.Services.AddSingleton<ICatalogAI, CatalogAI>();
  }
}
