using SixLabors.ImageSharp.ColorSpaces;
using SixLabors.ImageSharp.ColorSpaces.Conversion;
using SixLabors.ImageSharp.PixelFormats;
using System.Numerics;

namespace DotNet.GitHubAction;

public static class IndexedColor
{
    public static Rgba32 GetColor(int index, int total, float alpha = 1)
    {
        var hsv = new Hsv((index * 360f / total) % 360f, 0.65f, 0.95f);
        return new Rgba32(new Vector4(ColorSpaceConverter.ToRgb(hsv).ToVector3(), alpha));
    }
}
