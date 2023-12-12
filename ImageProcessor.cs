using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

namespace Macro;
internal class ImageProcessor : IHistogramProvider {
    public Histogram GetHistogram(string filename) {
        using Image<Rgba32> image = Image.Load<Rgba32>(filename);

        var l = new object();
        var hist = new Histogram();

        image.Mutate(c => c
            .ProcessPixelRowsAsVector4(row => {
                var lhist = new Histogram();

                foreach (var pixel in row) {
                    lhist.Red[(int) (pixel.X * 7)]++;
                    lhist.Green[(int) (pixel.Y * 7)]++;
                    lhist.Blue[(int) (pixel.Z * 7)]++;
                }

                lock (l) {
                    for (int i = 0; i < 8; i++) {
                        hist.Red[i] += lhist.Red[i];
                        hist.Green[i] += lhist.Green[i];
                        hist.Blue[i] += lhist.Blue[i];
                    }
                }
            })
        );

        var totalPixels = image.Width * image.Height * 3;
        hist.Red = hist.Red.Select(x => x / totalPixels).ToArray();
        hist.Green = hist.Green.Select(x => x / totalPixels).ToArray();
        hist.Blue = hist.Blue.Select(x => x / totalPixels).ToArray();


        //var diff = image.Height - _histograms.Count;
        //if (diff > 0) {
        //    _histograms.AddRange(Enumerable.Range(0, diff).Select(x => new Histogram()));
        //}

        //var iterator = new IndexedRowIterator(i => {
        //    var pixelRow = image.DangerousGetPixelRowMemory(i).Span;

        //    var rowHist = _histograms[i];
        //    for (int x = 0; x < 8; x++) {
        //        rowHist.Red[x] = 0;
        //        rowHist.Green[x] = 0;
        //        rowHist.Blue[x] = 0;
        //    }

        //    foreach (var pixel in pixelRow) {
        //        rowHist.Red[pixel.R / 32]++;
        //        rowHist.Green[pixel.G / 32]++;
        //        rowHist.Blue[pixel.B / 32]++;
        //    }
        //});

        //var interest = image.Bounds;
        //ParallelRowIterator.IterateRows(
        //    Configuration.Default,
        //    interest,
        //    in iterator);

        //var hist = new Histogram();
        //for (int i = 0; i < _histograms.Count; i++) {
        //    var tempHist = _histograms[i];
        //    for (int j = 0; j < 8; j++) {
        //        hist.Red[j] += tempHist.Red[j];
        //        hist.Green[j] += tempHist.Green[j];
        //        hist.Blue[j] += tempHist.Blue[j];
        //    }
        //}

        //var totalPixels = image.Width * image.Height * 3;
        //hist.Red = hist.Red.Select(x => x / totalPixels).ToArray();
        //hist.Green = hist.Green.Select(x => x / totalPixels).ToArray();
        //hist.Blue = hist.Blue.Select(x => x / totalPixels).ToArray();

        return hist;
    }
}
