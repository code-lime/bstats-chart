using System.Text.Json.Serialization;

namespace DotNet.GitHubAction.QuickCharts.Options;

public class ChartTime
{
    [JsonPropertyName("parser")]
    public required string Parser { get; set; }
}