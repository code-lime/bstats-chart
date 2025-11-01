using CommandLine;
using CommandLine.Text;
using DotNet.GitHubAction;
using DotNet.GitHubAction.Extensions;
using DotNet.GitHubAction.QuickCharts;
using DotNet.GitHubAction.QuickCharts.Dataset;
using DotNet.GitHubAction.QuickCharts.Options;
using Serilog;
using SixLabors.ImageSharp;

CancellationTokenSource cancellationTokenSource = new();
Console.CancelKeyPress += (_, e) =>
{
    e.Cancel = true;
    cancellationTokenSource.Cancel();
};
CancellationToken cancellationToken = cancellationTokenSource.Token;

Log.Logger = new LoggerConfiguration()
        .WriteTo.Console(outputTemplate: "{Message:lj}{NewLine}{Exception}")
        .CreateLogger();

try
{
    var result = new Parser(v =>
    {
        v.CaseInsensitiveEnumValues = true;
    })
    .ParseArguments<ActionInputs>(() => new(), args.ModifyEnviromentArgs());

    await result.WithNotParsed(_ =>
    {
        var help = HelpText.AutoBuild(result).ToString();
        Log.Logger.Information(help);
        Environment.Exit(2);
    })
    .WithParsedAsync(async options =>
    {
        var keys = options.ChartKeys.ToHashSet();
        int index = -1;
        int totalKeys = keys.Count;
        List<ChartDataSet> dataSets = [];
        foreach (var chartKey in options.ChartKeys)
        {
            index++;
            var color = IndexedColor.GetColor(index, totalKeys);
            BStatsChartClient bStats = new BStatsChartClient
            {
                PluginId = options.PluginId,
                ChartKey = chartKey,
            };
            string chartName = await bStats.GetNameAsync(cancellationToken);
            Log.Information($"[{bStats.PluginId}/{bStats.ChartKey}] Chart name: {chartName}");
            var data = await bStats.GetTimedDataAsync(options.Days, DateOnly.FromDateTime, cancellationToken);

            dataSets.Add(new ChartDataSet
            {
                Label = chartName,
                BackgroundColor = color,
                BorderColor = color,
                PointRadius = 0,
                BorderWidth = 2,
                Points = data
                    .Select(v => new ChartPoint
                    {
                        Label = v.Key.ToString("MM/dd/yyyy"),
                        Value = v.Value,
                    })
                    .ToList(),
                Fill = false,
            });
        }

        QuickChartClient quickChart = new QuickChartClient
        {
            Width = options.Width,
            Height = options.Height,
            Content = new ChartContent
            {
                Type = "line",
                Data = new ChartData
                {
                    DataSets = dataSets,
                },
                Options = new ChartOptions
                {
                    Title = new ChartTitle
                    {
                        Display = true,
                        Text = "bStats.org",
                    },
                    Legend = new ChartLegend
                    {
                        Display = true,
                        Position = "bottom",
                    },
                    Scales = new ChartScales
                    {
                        XAxes =
                        [
                            new ChartAxis
                            {
                                Type = "time",
                                Time = new ChartTime
                                {
                                    Parser = "MM/DD/YYYY",
                                },
                            },
                        ],
                        YAxes =
                        [
                            new ChartAxis
                            {
                                Id = "Y1",
                                Position = "right",
                                Display = true,
                            },
                        ],
                    },
                },
            },
        };

        await quickChart.SaveImageAsync(options.OutputFile, cancellationToken);
    });
}
catch (TaskCanceledException){}
catch (Exception e)
{
    Log.Error(e, "Error");
    Environment.Exit(2);
}
