using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Macro.Sqlite;
internal class Image {
    public int Id { get; set; }

    // FK
    public int HistogramId { get; set; }

    // Nav
    public Histogram Histogram { get; set; }

    // Column
    public string Path { get; set; }
}
