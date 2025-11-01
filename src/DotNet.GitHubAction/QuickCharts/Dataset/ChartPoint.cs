using System.Text.Json.Serialization;

namespace DotNet.GitHubAction.QuickCharts.Dataset;

public class ChartPoint
{
    [JsonPropertyName("x")]
    public required string Label { get; set; }

    [JsonPropertyName("y")]
    public required double Value { get; set; }
}


/*

        ["type"] = "line",
        ["data"] = new JsonObject()
        {
            ["datasets"] = new JsonArray
            {
                (JsonNode)new JsonObject()
                {
                    ["label"] = "Players",
                    ["backgroundColor"] = "rgb(255, 99, 132)",
                    ["borderColor"] = "rgb(255, 99, 132)",
                    ["pointRadius"] = 0,
                    ["borderWidth"] = 2,
                    ["data"] = JsonSerializer.SerializeToNode(playersValues, JsonContexts.Default.Int64Array),
                    ["fill"] = false,
                    ["yAxisID"] = "Y2",
                },
                (JsonNode) new JsonObject()
                {
                ["label"] = "Servers",
                    ["backgroundColor"] = "#ff0000ff",
                    ["borderColor"] = "#ff0000ff",
                    ["pointRadius"] = 0,
                    ["borderWidth"] = 1,
                    ["data"] = serversPoints,
                    ["fill"] = false,
                },
            },
        },
        ["options"] = new JsonObject()
        {
            ["title"] = new JsonObject()
            {
                ["display"] = true,
                ["text"] = "bStats.org",
            },
            ["legend"] = new JsonObject()
            {
                ["display"] = true,
                ["position"] = "bottom",
            },
            ["scales"] = new JsonObject()
            {
                ["xAxes"] = new JsonArray
                {
                    (JsonNode)new JsonObject()
                    {
                        ["type"] = "time",
                        ["time"] = new JsonObject()
                        {
                            ["parser"] = "MM/DD/YYYY HH:mm",
                        },
                    },
                },
                ["yAxes"] = new JsonArray
                {
                    (JsonNode)new JsonObject()
                    {
                        ["id"] = "Y1",
                        ["position"] = "right",
                        ["display"] = true,
                    },
                },
            },
        },
*/