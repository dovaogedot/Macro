namespace Macro.Repository;

internal interface IHistogramRepository
{
    void Add(string filename, Histogram histogram);
    Histogram Get(string filename);
    void Remove(string filename);
    void Update(string filename, Histogram histogram);
}