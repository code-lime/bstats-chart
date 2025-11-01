using System.Text.Json.Serialization;

namespace DotNet.GitHubAction.QuickCharts.Options;

public class ChartTitle
{
    [JsonPropertyName("display")]
    public bool Display { get; set; } = true;
    [JsonPropertyName("text")]
    public required string Text { get; set; }
}
