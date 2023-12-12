using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Macro.Sqlite;
internal class Histogram {
    public int Id { get; set; }

    // FK
    public int ImageId { get; set; }

    // Nav
    public Image Image { get; set; }

    // Column
    public string Red { get; set; }
    public string Green { get; set; }
    public string Blue { get; set; }
}
