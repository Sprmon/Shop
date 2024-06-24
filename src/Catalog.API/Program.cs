using Asp.Versioning.Builder;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
builder.AddApplicationServices();
builder.Services.AddProblemDetails();

var withApiVersioning = builder.Services.AddApiVersioning();

builder.AddDefaultOpenApi(withApiVersioning);

var app = builder.Build();
app.MapDefaultEndpoints();
app.NewVersionedApi("Catalog").MapCatalogApiV1();
app.UseDefaultOpenApi();

using (var scope = app.Services.CreateScope())
{
  var services = scope.ServiceProvider;
  var context = services.GetRequiredService<CatalogContext>();
  if (context.Database.GetPendingMigrations().Any())
  {
    context.Database.Migrate();
  }
}

app.Run();
