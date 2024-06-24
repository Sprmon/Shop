using System.Reflection;
using Microsoft.AspNetCore.Mvc.Testing;

namespace Sprmon.Shop.Catalog.FunctionalTests;

public sealed class CatalogApiFixture : WebApplicationFactory<Program>, IAsyncLifetime
{
  private readonly IHost _app;

  public IResourceBuilder<PostgresServerResource> Postgres { get; private set; }
  private string _postgresConnectionString;

  public CatalogApiFixture()
  {
    var options = new DistributedApplicationOptions
    {
      AssemblyName = typeof(CatalogApiFixture).Assembly.FullName,
      DisableDashboard = true,
    };
    var builder = DistributedApplication.CreateBuilder(options);
    Postgres = builder.AddPostgres("CatalogDB")
        .WithImage("ankane/pgvector")
        .WithImageTag("latest");
    _app = builder.Build();
  }

  protected override IHost CreateHost(IHostBuilder builder)
  {
    builder.ConfigureHostConfiguration(config =>
    {
      config.AddInMemoryCollection(new Dictionary<string, string>
      {
        {
          $"ConnectionStrings:{Postgres.Resource.Name}",
          _postgresConnectionString
        },
      });
    });
    return base.CreateHost(builder);
  }

  public new async Task DisposeAsync()
  {
    await base.DisposeAsync();
    await _app.StopAsync();
    if (_app is IAsyncDisposable asyncDisposable)
    {
      await asyncDisposable.DisposeAsync().ConfigureAwait(false);
    }
    else
    {
      _app.Dispose();
    }
  }

  public async Task  ()
  {
    await _app.StartAsync();
    _postgresConnectionString = await Postgres.Resource.GetConnectionStringAsync();
  }
}
