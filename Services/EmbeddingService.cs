using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;


public class EmbeddingService
{
    private readonly HttpClient _http;
    private readonly string _apiKey;

    public EmbeddingService(string apiKey)
    {
        _apiKey = apiKey;
        _http = new HttpClient();
        _http.DefaultRequestHeaders.Add("Authorization", $"Bearer {_apiKey}");
    }

   public async Task<float[]> GenerateEmbeddingAsync(string input)
{
    var content = new
    {
        input,
        model = "text-embedding-ada-002"
    };

    var response = await _http.PostAsJsonAsync("https://api.openai.com/v1/embeddings", content);
    var responseContent = await response.Content.ReadAsStringAsync();

    if (!response.IsSuccessStatusCode)
    {
        throw new Exception($"OpenAI API error: {response.StatusCode}\nResponse content: {responseContent}");
    }

    var json = JsonDocument.Parse(responseContent).RootElement;

    var vector = json.GetProperty("data")[0].GetProperty("embedding").EnumerateArray().Select(x => x.GetSingle()).ToArray();
    return vector;
}

}
