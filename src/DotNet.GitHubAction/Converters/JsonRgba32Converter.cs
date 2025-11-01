using SixLabors.ImageSharp.PixelFormats;
using System.Text.Json;

namespace DotNet.GitHubAction.Converters;

public class JsonRgba32Converter : StringJsonConverter<Rgba32>
{
    public override Rgba32 Parse(string value)
        => value.StartsWith('#') || !Rgba32.TryParseHex(value, out Rgba32 color)
        ? throw new JsonException($"'{value}' is not hexable with prefix '#'")
        : color;
    public override string ToString(Rgba32 value)
        => $"#{value.ToHex()}";
}
