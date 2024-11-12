using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Moongazing.ElasticSearch;
using Moongazing.ElasticSearch.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<ElasticSearchConfig>(builder.Configuration.GetSection("ElasticSearchConfig"));
builder.Services.AddSingleton(resolver => resolver.GetRequiredService<IOptions<ElasticSearchConfig>>().Value);
builder.Services.AddSingleton<IElasticSearchService, ElasticSearchService>();

var app = builder.Build();

// Index listeleme
app.MapGet("/api/indexes", async ([FromServices] IElasticSearchService elasticSearchService) =>
{
    return Results.Ok(await Task.Run(() => elasticSearchService.GetIndexList()));
});

// Yeni index oluşturma
app.MapPost("/api/indexes", async ([FromServices] IElasticSearchService elasticSearchService, [FromBody] IndexModel model) =>
{
    var result = await elasticSearchService.CreateNewIndexAsync(model);
    return Results.Ok(result);
});

// Birçok belge ekleme (bulk insert)
app.MapPost("/api/documents/bulk", async ([FromServices] IElasticSearchService elasticSearchService, [FromQuery] string indexName, [FromBody] object[] items) =>
{
    var result = await elasticSearchService.InsertManyAsync(indexName, items);
    return Results.Ok(result);
});

// Belge silme
app.MapDelete("/api/documents/{indexName}/{elasticId}", async ([FromServices] IElasticSearchService elasticSearchService, string indexName, string elasticId) =>
{
    var model = new ElasticSearchModel { IndexName = indexName, ElasticId = elasticId };
    var result = await elasticSearchService.DeleteByElasticIdAsync(model);
    return Results.Ok(result);
});

// Belge arama
app.MapGet("/api/documents/search", async ([FromServices] IElasticSearchService elasticSearchService, [FromBody] SearchParameters parameters) =>
{
    var result = await elasticSearchService.GetAllSearch<object>(parameters);
    return Results.Ok(result);
});

// Alana göre arama
app.MapGet("/api/documents/searchByField", async ([FromServices] IElasticSearchService elasticSearchService, [FromBody] SearchByFieldParameters fieldParameters) =>
{
    var result = await elasticSearchService.GetSearchByField<object>(fieldParameters);
    return Results.Ok(result);
});

// Sorgu ile arama
app.MapGet("/api/documents/searchByQuery", async ([FromServices] IElasticSearchService elasticSearchService, [FromBody] SearchByQueryParameters queryParameters) =>
{
    var result = await elasticSearchService.GetSearchBySimpleQueryString<object>(queryParameters);
    return Results.Ok(result);
});

// Tek bir belge ekleme
app.MapPost("/api/documents", async ([FromServices] IElasticSearchService elasticSearchService, [FromBody] ElasticSearchInsertUpdateModel model) =>
{
    var result = await elasticSearchService.InsertAsync(model);
    return Results.Ok(result);
});

// Belge güncelleme
app.MapPut("/api/documents/{indexName}/{elasticId}", async ([FromServices] IElasticSearchService elasticSearchService, string indexName, string elasticId, [FromBody] object item) =>
{
    var model = new ElasticSearchInsertUpdateModel { IndexName = indexName, ElasticId = elasticId, Item = item };
    var result = await elasticSearchService.UpdateByElasticIdAsync(model);
    return Results.Ok(result);
});

app.Run();
