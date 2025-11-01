using System.Text.Json.Serialization;

namespace DotNet.GitHubAction.QuickCharts.Options;

public class ChartScales
{
    [JsonPropertyName("xAxes")]
    public required List<ChartAxis> XAxes { get; set; }
    [JsonPropertyName("yAxes")]
    public required List<ChartAxis> YAxes { get; set; }
}
