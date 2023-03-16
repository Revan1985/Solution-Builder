
using Spectre.Console;

namespace SolutionBuilder.Console.Shared;

internal static class ColorExtensions
{
    public static Color GetInvertedColor(this Color color) =>
        GetLuminance(color) < 140 ? Color.White : Color.Black;

    public static float GetLuminance(this Color color) =>
        (float)((0.2126 * color.R) + (0.7152 * color.G) + (0.0722 * color.B));
}
