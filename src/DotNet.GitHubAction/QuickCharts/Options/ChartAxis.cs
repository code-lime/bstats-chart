using System.Text.Json.Serialization;

namespace DotNet.GitHubAction.QuickCharts.Options;

public class ChartAxis
{
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    [JsonPropertyName("type")]
    public string Type { get; set; } = "linear";

    [JsonPropertyName("position")]
    public string? Position { get; set; }

    [JsonPropertyName("display")]
    public bool Display { get; set; } = true;

    [JsonPropertyName("time")]
    public ChartTime? Time { get; set; }
}
