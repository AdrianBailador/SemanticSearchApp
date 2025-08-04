var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

var embeddingService = new EmbeddingService("API_KEY");
var repo = new DocumentRepository();

app.MapPost("/documents", async (Document doc) =>
{
    doc.Embedding = await embeddingService.GenerateEmbeddingAsync(doc.Content);
    await repo.AddDocumentAsync(doc);
    return Results.Ok();
});

app.MapGet("/search", async (string q) =>
{
    var embedding = await embeddingService.GenerateEmbeddingAsync(q);
    var results = await repo.SearchAsync(embedding);
    return Results.Ok(results);
});

app.Run();
