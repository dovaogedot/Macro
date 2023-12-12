
using Macro.Repository;

namespace Macro;

internal class Macro(IHistogramRepository _histogramRepository) : IMacro {
    private static readonly IEnumerable<string> _formats = "jpg,jpeg,webp,png,bmp,gif,pbm,tiff,tga".Split(',').Select(x => '.' + x);

    public IEnumerable<IEnumerable<string>> GetAllDuplicates(string directory, float threshold) {
        throw new NotImplementedException();
    }

    public IEnumerable<string> GetDuplicatesOf(string filepath, float threshold) {
        return GetDuplicatesOf(filepath, Path.GetDirectoryName(filepath) ?? throw new ArgumentException($"""Cannot get directory name from "{filepath}"."""), threshold);
    }

    public IEnumerable<string> GetDuplicatesOf(string filepath, string directory, float threshold) {
        var histogram = _histogramRepository.Get(filepath);
        var files = Directory.GetFiles(directory).Where(file => _formats.Any(f => file.EndsWith(f)));

        foreach (var file in files) {
            var h = _histogramRepository.Get(file);
            if (Histogram.Compare(h, histogram) > threshold) {
                yield return file;
            }
        }
    }
}
