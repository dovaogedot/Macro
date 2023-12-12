namespace Macro;

internal interface IHistogramProvider {
    Histogram GetHistogram(string filename);
}