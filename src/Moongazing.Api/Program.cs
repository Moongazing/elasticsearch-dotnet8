using Moongazing.ElasticSearch;
using Moongazing.ElasticSearch.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<IElasticSearchService, ElasticSearchService>();
var app = builder.Build();

app.MapGet("/api/indexes", async (IElasticSearchService elasticSearchService) =>
{
    return Results.Ok(await Task.Run(() => elasticSearchService.GetIndexList()));
});

app.MapPost("/api/indexes", async (IElasticSearchService elasticSearchService, IndexModel model) =>
{
    var result = await elasticSearchService.CreateNewIndexAsync(model);
    return Results.Ok(result);
});

app.MapPost("/api/documents/bulk", async (IElasticSearchService elasticSearchService, string indexName, object[] items) =>
{
    var result = await elasticSearchService.InsertManyAsync(indexName, items);
    return Results.Ok(result);
});

app.MapDelete("/api/documents/{indexName}/{elasticId}", async (IElasticSearchService elasticSearchService, string indexName, string elasticId) =>
{
    var model = new ElasticSearchModel { IndexName = indexName, ElasticId = elasticId };
    var result = await elasticSearchService.DeleteByElasticIdAsync(model);
    return Results.Ok(result);
});

app.MapGet("/api/documents/search", async (IElasticSearchService elasticSearchService, SearchParameters parameters) =>
{
    var result = await elasticSearchService.GetAllSearch<object>(parameters);
    return Results.Ok(result);
});

app.MapGet("/api/documents/searchByField", async (IElasticSearchService elasticSearchService, SearchByFieldParameters fieldParameters) =>
{
    var result = await elasticSearchService.GetSearchByField<object>(fieldParameters);
    return Results.Ok(result);
});

app.MapGet("/api/documents/searchByQuery", async (IElasticSearchService elasticSearchService, SearchByQueryParameters queryParameters) =>
{
    var result = await elasticSearchService.GetSearchBySimpleQueryString<object>(queryParameters);
    return Results.Ok(result);
});

app.MapPost("/api/documents", async (IElasticSearchService elasticSearchService, ElasticSearchInsertUpdateModel model) =>
{
    var result = await elasticSearchService.InsertAsync(model);
    return Results.Ok(result);
});

app.MapPut("/api/documents/{indexName}/{elasticId}", async (IElasticSearchService elasticSearchService, string indexName, string elasticId, object item) =>
{
    var model = new ElasticSearchInsertUpdateModel { IndexName = indexName, ElasticId = elasticId, Item = item };
    var result = await elasticSearchService.UpdateByElasticIdAsync(model);
    return Results.Ok(result);
});

app.Run();
