using System.Text.Json.Serialization;

namespace DotNet.GitHubAction.QuickCharts.Options;

public class ChartLegend
{
    [JsonPropertyName("display")]
    public bool Display { get; set; } = true;
    [JsonPropertyName("position")]
    public required string Position { get; set; }
}
