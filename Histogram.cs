using System;

namespace Macro;

/// <summary>
/// Contains information about each RGB channel - <see cref="Red"/>, <see cref="Green"/>, <see cref="Blue"/>.
/// Each channel is divided into 8 buckets. 
/// Each bucket represents color's brightness, respectively to bucket's index.
/// In each bucket sits a value representing percentage of this color (at that brightness level) in whole image.
/// </summary>
internal record Histogram {
    public double[] Red { get; set; } = new double[8];
    public double[] Green { get; set; } = new double[8];
    public double[] Blue { get; set; } = new double[8];

    public override string ToString() {
        var r = String.Join("\n", Red.Select((x, i) => $"{i}: {String.Join("", Enumerable.Repeat("E", (int) Math.Floor(x * 10 * 32)))}"));
        var g = String.Join("\n", Green.Select((x, i) => $"{i}: {String.Join("", Enumerable.Repeat("E", (int) Math.Floor(x * 10 * 32)))}"));
        var b = String.Join("\n", Blue.Select((x, i) => $"{i}: {String.Join("", Enumerable.Repeat("E", (int) Math.Floor(x * 10 * 32)))}"));

        return $"Red:\n{r}\n\nGreen:\n{g}\n\nBlue:\n{b}";
    }

    /// <summary>
    /// Makes a Histogram which shows difference between each bucket of each channel of histograms.
    /// </summary>
    public static Histogram Difference(Histogram h1, Histogram h2) {
        var channelDiff = double[] (double[] channel1, double[] channel2) =>
            Enumerable.Range(0, 8).Select(x => Math.Abs(channel1[x] - channel2[x])).ToArray();
        return new Histogram { 
            Red = channelDiff(h1.Red, h2.Red), 
            Green = channelDiff(h1.Green, h2.Green),
            Blue = channelDiff(h1.Blue, h2.Blue),
        };
    }

    /// <summary>
    /// A percentage showing how similar two histograms are.
    /// </summary>
    public static double Compare(Histogram h1, Histogram h2) {
        var diff = Difference(h1, h2);
        return 100 - (diff.Red.Sum() + diff.Green.Sum() + diff.Blue.Sum()) * 100;
    }
}
