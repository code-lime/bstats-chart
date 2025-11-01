using System.Text.Json.Serialization;

namespace DotNet.GitHubAction.QuickCharts;

public class ChartContent
{
    [JsonPropertyName("type")]
    public required string Type { get; set; }

    [JsonPropertyName("data")]
    public required ChartData Data { get; set; }

    [JsonPropertyName("options")]
    public required ChartOptions Options { get; set; }
}
