using Dapper;
using Microsoft.Data.Sqlite;
using System.Text.Json;


public class DocumentRepository
{
    private readonly string _connectionString = "Data Source=semantic.db";

    public async Task AddDocumentAsync(Document doc)
    {
        using var connection = new SqliteConnection(_connectionString);
        var embeddingJson = JsonSerializer.Serialize(doc.Embedding);
        await connection.ExecuteAsync("INSERT INTO Documents (Title, Content, Embedding) VALUES (@Title, @Content, @Embedding)",
            new { doc.Title, doc.Content, Embedding = embeddingJson });
    }

    public async Task<IEnumerable<Document>> SearchAsync(float[] queryVector)
    {
        using var connection = new SqliteConnection(_connectionString);
        var docs = await connection.QueryAsync<(int Id, string Title, string Content, string Embedding)>("SELECT * FROM Documents");

        return docs
            .Select(d =>
            {
                var embedding = JsonSerializer.Deserialize<float[]>(d.Embedding);
                var score = CosineSimilarity(queryVector, embedding!);
                return new Document { Id = d.Id, Title = d.Title, Content = d.Content, Embedding = embedding! };
            })
            .OrderByDescending(d => CosineSimilarity(queryVector, d.Embedding))
            .Take(5);
    }

    private static float CosineSimilarity(float[] a, float[] b)
    {
        float dot = 0, normA = 0, normB = 0;
        for (int i = 0; i < a.Length; i++)
        {
            dot += a[i] * b[i];
            normA += a[i] * a[i];
            normB += b[i] * b[i];
        }
        return dot / (float)(Math.Sqrt(normA) * Math.Sqrt(normB));
    }
}
