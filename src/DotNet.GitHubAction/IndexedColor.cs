using SixLabors.ImageSharp.ColorSpaces;
using SixLabors.ImageSharp.ColorSpaces.Conversion;
using SixLabors.ImageSharp.PixelFormats;
using System.Numerics;

namespace DotNet.GitHubAction;

public static class IndexedColor
{
    private const float HUE_OFFSET = 204.2f;

    public static Rgba32 GetColor(int index, int total, float alpha = 1)
    {
        float hue = index * 360f + HUE_OFFSET;
        var hsv = new Hsv(hue / total % 360f, 0.65f, 0.95f);
        return new Rgba32(new Vector4(ColorSpaceConverter.ToRgb(hsv).ToVector3(), alpha));
    }
}
