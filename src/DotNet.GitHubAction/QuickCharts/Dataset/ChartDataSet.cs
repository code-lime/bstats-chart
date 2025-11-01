using DotNet.GitHubAction.Converters;
using SixLabors.ImageSharp.PixelFormats;
using System.Text.Json.Serialization;

namespace DotNet.GitHubAction.QuickCharts.Dataset;

public class ChartDataSet
{
    [JsonPropertyName("label")]
    public required string Label { get; set; }

    [JsonPropertyName("backgroundColor")]
    [JsonConverter(typeof(JsonRgba32Converter))]
    public required Rgba32 BackgroundColor { get; set; }

    [JsonPropertyName("borderColor")]
    [JsonConverter(typeof(JsonRgba32Converter))]
    public required Rgba32 BorderColor { get; set; }

    [JsonPropertyName("pointRadius")]
    public required int PointRadius { get; set; }

    [JsonPropertyName("borderWidth")]
    public required int BorderWidth { get; set; }

    [JsonPropertyName("data")]
    public required List<ChartPoint> Points { get; set; }

    [JsonPropertyName("fill")]
    public required bool Fill { get; set; }

    [JsonPropertyName("yAxisID")]
    public string? YAxisID { get; set; }
}
