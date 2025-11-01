using CommandLine;

namespace DotNet.GitHubAction;

public class ActionInputs
{
    [Option("plugin-id",
        Required = true,
        HelpText = "Plugin id (number) from bstats.org")]
    public string PluginId { get; set; } = null!;

    [Option("chart-keys",
        Required = true,
        HelpText = "Chart keys separated by ','. E.g. 'servers,players'",
        Separator = ',')]
    public IEnumerable<string> ChartKeys { get; set; } = [];

    [Option("days",
        Required = true,
        HelpText = "Total days on chart")]
    public int Days { get; set; } = 100;

    [Option("width",
        Required = true,
        HelpText = "Output image width")]
    public int Width { get; set; } = 800;

    [Option("height",
        Required = true,
        HelpText = "Output image height")]
    public int Height { get; set; } = 200;

    [Option("output-file",
        Required = true,
        HelpText = "Created PNG image path")]
    public string OutputFile { get; set; } = null!;
}
