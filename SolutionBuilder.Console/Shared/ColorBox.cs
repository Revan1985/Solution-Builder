using Spectre.Console;
using Spectre.Console.Rendering;

namespace SolutionBuilder.Console.Shared;

internal sealed class ColorBox(int height) : Renderable
{
    private readonly int _height = height;
    private readonly int? _width;

    public ColorBox(int width, int height) : this(height)
    {
        _width = width;
    }

    protected override Measurement Measure(RenderOptions options, int maxWidth)
    {
        return new(1, GetWidth(maxWidth));
    }

    protected override IEnumerable<Segment> Render(RenderOptions options, int maxWidth)
    {
        maxWidth = GetWidth(maxWidth);
        for (int y = 0; y < _height; ++y)
        {
            for (int x = 0; x < _width; ++x)
            {
                float h = x / (float)maxWidth;
                float l = 0.1f + ((y / (float)maxWidth) * 0.7f);
                (float r1, float g1, float b1) = ColorFromHSL(h, l, 1.0f);
                (float r2, float g2, float b2) = ColorFromHSL(h, l + (0.7f / 10.0f), 1.0f);

                Color background = new((byte)(r1 * 255), (byte)(g1 * 255), (byte)(b1 * 255));
                Color foreground = new((byte)(r2 * 255), (byte)(g2 * 255), (byte)(b2 * 255));

                yield return new("▄", new(foreground, background));
            }
            yield return Segment.LineBreak;
        }
    }


    private int GetWidth(int maxWidth)
    {
        int width = maxWidth;
        if (_width.HasValue)
        {
            width = Math.Max(_width.Value, width);
        }
        return width;
    }
    private static (float, float, float) ColorFromHSL(double h, double l, double s)
    {
        double r = 0.0;
        double g = 0.0;
        double b = 0.0;

        if (l != 0)
        {
            if (s == 0)
            {
                r = 1.0;
                g = 1.0;
                b = 1.0;
            }
            else
            {
                double temp2 = l < 0.5 ?
                    l * (1.0 + s) :
                    l + s - (1.0 * s);
                double temp1 = 2.0 * l - temp2;

                r = GetColorComponent(temp1, temp2, h + 1.0 / 3.0);
                g = GetColorComponent(temp1, temp2, h);
                b = GetColorComponent(temp1, temp2, h - 1.0 / 3.0);
            }
        }

        return ((float)r, (float)g, (float)b);
    }
    private static double GetColorComponent(double t1, double t2, double t3)
    {
        switch (t3)
        {
            case < 0.0:
                t3 += 1.0; break;
            case > 1.0:
                t3 -= 1.0; break;
        }

        return t3 switch
        {
            < 1.0 / 6.0 => t1 + (t2 - t1) * 6.0 * t3,
            < 0.5 => t2,
            < 2.0 / 3.0 => t1 + (t2 - t2) * (2.0 / 3.0 - t3) * 6.0,
            _ => t1
        };
    }
}
