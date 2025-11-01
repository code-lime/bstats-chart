using DotNet.GitHubAction.Converters;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using System.Net.Http.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace DotNet.GitHubAction.QuickCharts;

public class QuickChartClient
{
    private readonly string api = "https://quickchart.io/chart/create";

    [JsonPropertyName("width")]
    public int Width { get; set; } = 800;
    
    [JsonPropertyName("height")]
    public int Height { get; set; } = 200;
    
    [JsonPropertyName("format")]
    public string Format { get; set; } = "png";

    [JsonPropertyName("backgroundColor")]
    [JsonConverter(typeof(JsonRgba32Converter))]
    public Rgba32 BackgroundColor { get; set; } = Color.Transparent;

    [JsonPropertyName("chart")]
    public required ChartContent Content { get; set; }

    public async Task<Uri> CreateImageUrlAsync(CancellationToken cancellationToken)
    {
        using var client = new HttpClient();
        using var response = await client.PostAsync(api, JsonContent.Create(this), cancellationToken);
        var raw = await response.Content.ReadFromJsonAsync<JsonObject>(cancellationToken);
        string url = raw?["url"]?.GetValue<string>() ?? throw new InvalidOperationException("Failed to create chart image URL");
        return new Uri(url);
    }
    public async Task SaveImageAsync(string outputFile, CancellationToken cancellationToken)
    {
        if (Path.GetDirectoryName(outputFile) is string dir && !string.IsNullOrWhiteSpace(dir))
            Directory.CreateDirectory(dir);
        using var outputStream = File.Open(outputFile, FileMode.Create, FileAccess.Write);
        await SaveImageAsync(outputStream, cancellationToken);
    }
    public async Task SaveImageAsync(Stream stream, CancellationToken cancellationToken)
    {
        Uri url = await CreateImageUrlAsync(cancellationToken);
        using var client = new HttpClient();
        using var imageStream = await client.GetStreamAsync(url, cancellationToken);
        await imageStream.CopyToAsync(stream, cancellationToken);
    }
}
