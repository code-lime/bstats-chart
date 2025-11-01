using DotNet.GitHubAction.QuickCharts.Dataset;
using System.Text.Json.Serialization;

namespace DotNet.GitHubAction.QuickCharts;

public class ChartData
{
    [JsonPropertyName("datasets")]
    public required List<ChartDataSet> DataSets { get; set; }
}
