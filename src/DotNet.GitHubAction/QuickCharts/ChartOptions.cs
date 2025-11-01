using DotNet.GitHubAction.QuickCharts.Options;
using System.Text.Json.Serialization;

namespace DotNet.GitHubAction.QuickCharts;

public class ChartOptions
{
    [JsonPropertyName("title")]
    public ChartTitle? Title { get; set; }
    [JsonPropertyName("legend")]
    public ChartLegend? Legend { get; set; }
    [JsonPropertyName("scales")]
    public ChartScales? Scales { get; set; }
}
