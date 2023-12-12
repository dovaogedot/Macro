using Macro.Repository;
using Macro.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Macro.DI;
public static class DependencyInjection {
    public static void AddMacro(this IServiceCollection services, Options options) {
        services.AddSingleton<IHistogramProvider, ImageProcessor>();
        services.AddSingleton(p => new MacroDbContext(new DbContextOptionsBuilder().UseSqlite(options.DbFilePath).Options));
        services.AddSingleton<IMacro, Macro>();
        services.AddAutoMapper(typeof(MacroAutoMapperProfile));

        switch (options.Storage) {
            case Storage.InMemory:
                services.AddSingleton<IHistogramRepository, InMemoryHistogramRepository>();
                break;
            case Storage.Sqlite:
                services.AddSingleton<IHistogramRepository, SqliteHistogramRepository>();
                break;
            default:
                services.AddSingleton<IHistogramRepository, InMemoryHistogramRepository>();
                break;
        }
    }

    public static void AddMacro(this IServiceCollection services) {
        services.AddMacro(new Options { Storage = Storage.InMemory, DbFilePath = "macro.db"});
    }
}
