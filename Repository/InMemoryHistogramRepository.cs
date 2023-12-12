using System;
using System.Collections.Generic;

namespace Macro.Repository;
internal class InMemoryHistogramRepository(IHistogramProvider _histogramProvider) : IHistogramRepository
{
    private readonly Dictionary<string, Histogram> _histograms = [];

    public void Add(string filename, Histogram histogram)
    {
        _histograms.Add(filename, histogram);
    }

    public Histogram Get(string filename)
    {
        if (_histograms.ContainsKey(filename))
        {
            return _histograms[filename];
        }

        var hist = _histogramProvider.GetHistogram(filename);
        _histograms.Add(filename, hist);
        return hist;
    }

    public void Remove(string filename)
    {
        _histograms.Remove(filename);
    }

    public void Update(string filename, Histogram histogram)
    {
        _histograms.Add(filename, histogram);
    }
}
