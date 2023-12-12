using AutoMapper;
using Macro.Sqlite;

namespace Macro.Repository;
internal class SqliteHistogramRepository(MacroDbContext _macroDbContext, IMapper _mapper, IHistogramProvider _histogramProvider) : IHistogramRepository
{
    public void Add(string filename, Histogram histogram)
    {
        Sqlite.Histogram hist = new();
        hist.Red = string.Join(",", histogram.Red);
        hist.Green = string.Join(",", histogram.Green);
        hist.Blue = string.Join(",", histogram.Blue);

        Image image = new();
        image.Path = filename;
        image.Histogram = hist;

        _macroDbContext.Images.Add(image);
        _macroDbContext.SaveChanges();
    }

    public Histogram Get(string filename)
    {
        var hist = _macroDbContext.Histograms.Where(h => h.Image.Path == filename).FirstOrDefault();

        if (hist != null)
        {
            return _mapper.Map<Histogram>(hist);
        }

        var newHist = _histogramProvider.GetHistogram(filename);
        var newImg = new Image();
        newImg.Path = filename;
        newImg.Histogram = _mapper.Map<Sqlite.Histogram>(newHist);
        _macroDbContext.SaveChanges();
        return newHist;
    }

    public void Remove(string filename)
    {
        var hist = _macroDbContext.Histograms.Where(h => h.Image.Path == filename).FirstOrDefault();
        if (hist == null)
        {
            throw new ArgumentException("No such file in database.");
        }
        _macroDbContext.Histograms.Remove(hist);
        _macroDbContext.SaveChanges();
    }

    public void Update(string filename, Histogram histogram)
    {
        var img = _macroDbContext.Images.Where(i => i.Path == filename).FirstOrDefault();
        if (img == null)
        {
            throw new ArgumentException("No such file in database.");
        }
        var hist = _mapper.Map<Sqlite.Histogram>(histogram);
        img.Histogram = hist;
        _macroDbContext.SaveChanges();
    }
}
