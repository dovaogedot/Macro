using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Macro.DI;
public record Options {
    public Storage Storage { get; set; } = Storage.InMemory;
    public string DbFilePath { get; set; } = "macro.db";
}
