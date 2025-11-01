using System.Net.Http.Json;
using System.Text.Json.Nodes;

namespace DotNet.GitHubAction;

public class BStatsChartClient
{
    private readonly string api = "https://bstats.org/api/v1/plugins/";
    private readonly int elementsPerDay = 24 * 2;

    public required string PluginId { get; init; }
    public required string ChartKey { get; init; }

    private string ChartUrl => $"{api}{Uri.EscapeDataString(PluginId)}/charts/{Uri.EscapeDataString(ChartKey)}";

    public async ValueTask<string> GetNameAsync(CancellationToken cancellationToken)
    {
        using HttpClient client = new();
        JsonObject? raw = await client.GetFromJsonAsync<JsonObject>(ChartUrl, cancellationToken);
        return raw?["data"]?["lineName"]?.GetValue<string>() ?? raw?["title"]?.GetValue<string>() ?? ChartKey;
    }
    public async Task<Dictionary<TKey, double>> GetTimedDataAsync<TKey>(
        int days,
        Func<DateTime, TKey> keySelector,
        CancellationToken cancellationToken)
        where TKey : notnull
    {
        int elements = days * elementsPerDay;

        using var client = new HttpClient();
        double[][] raw = (await client.GetFromJsonAsync<double[][]>($"{ChartUrl}/data?maxElements={elements}", cancellationToken))!;
        return raw
            .Select(v => new
            {
                Date = keySelector(DateTimeOffset.FromUnixTimeMilliseconds((long)v[0]).UtcDateTime),
                Value = v[1],
            })
            .GroupBy(v => v.Date)
            .ToDictionary(v => v.Key, v => v.Max(v => v.Value)) ?? [];
    }
}
