using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Macro.DI;
internal class MacroAutoMapperProfile : Profile {
    public MacroAutoMapperProfile() {
        CreateMap<Sqlite.Histogram, Histogram>()
            .ForMember(h => h.Red, cfg => cfg.MapFrom(h => h.Red.Split(',', StringSplitOptions.None)));
        CreateMap<Sqlite.Histogram, Histogram>()
            .ForMember(h => h.Green, cfg => cfg.MapFrom(h => h.Green.Split(',', StringSplitOptions.None)));
        CreateMap<Sqlite.Histogram, Histogram>()
            .ForMember(h => h.Blue, cfg => cfg.MapFrom(h => h.Blue.Split(',', StringSplitOptions.None)));


        CreateMap<Histogram, Sqlite.Histogram>()
            .ForMember(h => h.Red, cfg => cfg.MapFrom(h => string.Join(',', h.Red)));
        CreateMap<Histogram, Sqlite.Histogram>()
            .ForMember(h => h.Green, cfg => cfg.MapFrom(h => string.Join(',', h.Green)));
        CreateMap<Histogram, Sqlite.Histogram>()
            .ForMember(h => h.Blue, cfg => cfg.MapFrom(h => string.Join(',', h.Blue)));
    }
}
